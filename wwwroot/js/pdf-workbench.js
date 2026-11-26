(() => {
  const form = $('pdf-form'), input = $('pdf-file'), button = $('pdf-submit');
  const mode = form.dataset.pdfMode;
  const isSlides = mode === 'slides';
  const maxPages = isSlides ? 20 : 200, maxMb = isSlides ? 30 : 50;
  let selectedFile;
  const picker = input.closest('label');

  function selectFile(file) {
    selectedFile = file;
    $('pdf-selection').textContent = file ? file.name : 'No file selected.';
    $('out').textContent = '';
    clearDownloadLinks('links');
  }
  input.addEventListener('change', () => selectFile(input.files[0]));
  picker.addEventListener('dragover', event => event.preventDefault());
  picker.addEventListener('drop', event => {
    event.preventDefault();
    if (button.disabled) return;
    const files = event.dataTransfer.files;
    if (files.length !== 1) { show('out', 'Drop one PDF at a time.', false); return; }
    selectFile(files[0]);
  });
  form.addEventListener('reset', () => selectFile());

  function selectedPages(text, total, allWhenEmpty) {
    if (!text.trim()) {
      if (allWhenEmpty) return Array.from({ length: total }, (_, i) => i);
      throw new Error('Enter a page number or range, such as 2,5-7.');
    }
    const pages = new Set();
    for (const item of text.split(',')) {
      const match = item.trim().match(/^(\d+)(?:\s*-\s*(\d+))?$/);
      const first = Number(match?.[1]), last = Number(match?.[2] ?? first);
      if (!match || first < 1 || last < first || last > total) {
        throw new Error('Use ascending page ranges between 1 and ' + total + ', such as 2,5-7.');
      }
      for (let position = first; position <= last; position++) pages.add(position - 1);
    }
    return [...pages].sort((a, b) => a - b);
  }

  function drawLabel(page, font, label, position, opacity) {
    const { x: left, y: bottom, width, height } = page.getCropBox();
    const rotation = ((page.getRotation().angle % 360) + 360) % 360;
    const visualWidth = rotation === 90 || rotation === 270 ? height : width;
    const visualHeight = rotation === 90 || rotation === 270 ? width : height;
    const watermark = position === 'middle';
    const units = font.widthOfTextAtSize(label, 1);
    const margin = Math.min(24, visualWidth / 10, visualHeight / 10);
    const size = Math.min(watermark ? 54 : 12, (visualWidth - 2 * margin) / units, visualHeight / 4);
    const textWidth = units * size;
    const x = position === 'right' ? visualWidth - margin - textWidth : (visualWidth - textWidth) / 2;
    const y = watermark ? (visualHeight - size) / 2 : margin;
    const coordinate = {
      0: [x, y],
      90: [width - y, x],
      180: [width - x, height - y],
      270: [y, height - x]
    }[rotation];
    page.drawText(label, { x: left + coordinate[0], y: bottom + coordinate[1], font, size,
      rotate: PDFLib.degrees(rotation), opacity, color: PDFLib.rgb(0.18, 0.22, 0.25) });
  }

  async function editPdf(file) {
    let pdf;
    try {
      pdf = await PDFLib.PDFDocument.load(await readBytes(file), { updateMetadata: false });
    } catch {
      throw new Error('Could not open this PDF. Check that it is a readable file without a password.');
    }
    const total = pdf.getPageCount();
    if (!total || total > maxPages) throw new Error('Choose a PDF with 1 to ' + maxPages + ' pages.');
    const pages = pdf.getPages();
    if (mode === 'rotate' || mode === 'remove') {
      const targets = selectedPages($('pdf-pages').value, total, mode === 'rotate');
      if (mode === 'rotate') {
        const turn = Number($('pdf-turn').value);
        for (const index of targets) pages[index].setRotation(PDFLib.degrees((pages[index].getRotation().angle + turn) % 360));
      } else {
        if (targets.length === total) throw new Error('Keep at least one page in the PDF.');
        const remove = new Set(targets);
        const keep = pages.map((_, index) => index).filter(index => !remove.has(index));
        const result = await PDFLib.PDFDocument.create();
        for (const page of await result.copyPages(pdf, keep)) result.addPage(page);
        return new Blob([await result.save()], { type: 'application/pdf' });
      }
    } else {
      const font = await pdf.embedFont(PDFLib.StandardFonts.Helvetica);
      if (mode === 'number') {
        const first = $('pdf-first').valueAsNumber, start = $('pdf-start').valueAsNumber;
        if (!Number.isInteger(first) || first < 1 || first > total ||
            !Number.isInteger(start) || start < 1 || start > 9999) {
          throw new Error('Choose a first page between 1 and ' + total + ' and a starting number between 1 and 9,999.');
        }
        for (let index = first - 1; index < total; index++) {
          drawLabel(pages[index], font, String(start + index - first + 1), $('pdf-footer').value, 1);
        }
      } else {
        const label = $('pdf-text').value.trim(), opacity = $('pdf-opacity').valueAsNumber;
        if (!label || label.length > 80) throw new Error('Enter a watermark of 1 to 80 characters.');
        if (!Number.isInteger(opacity) || opacity < 5 || opacity > 100) throw new Error('Enter an opacity from 5 to 100 percent.');
        try { font.encodeText(label); }
        catch { throw new Error('Use a short label with Latin letters, numbers or common punctuation.'); }
        for (const index of selectedPages($('pdf-pages').value, total, true)) drawLabel(pages[index], font, label, 'middle', opacity / 100);
      }
    }
    return new Blob([await pdf.save()], { type: 'application/pdf' });
  }

  async function makeSlides(file) {
    pdfjsLib.GlobalWorkerOptions.workerSrc = '/js/lib/pdf.worker.min.js';
    const task = pdfjsLib.getDocument({ data: await readBytes(file), isEvalSupported: false });
    try {
      let pdf;
      try { pdf = await task.promise; }
      catch { throw new Error('Could not open this PDF. Check that it is a readable file without a password.'); }
      if (!pdf.numPages || pdf.numPages > maxPages) throw new Error('Choose a PDF with 1 to ' + maxPages + ' pages.');
      const deck = new PptxGenJS();
      deck.layout = 'LAYOUT_WIDE';
      const slideWidth = 40 / 3, slideHeight = 7.5, padding = 0.25;
      for (let n = 1; n <= pdf.numPages; n++) {
        show('out', 'Rendering page ' + n + ' of ' + pdf.numPages + '...', false);
        const page = await pdf.getPage(n), base = page.getViewport({ scale: 1 });
        const viewport = page.getViewport({ scale: Math.min(1800 / base.width, 1400 / base.height) });
        const canvas = document.createElement('canvas');
        canvas.width = Math.max(1, Math.round(viewport.width));
        canvas.height = Math.max(1, Math.round(viewport.height));
        await page.render({ canvasContext: canvas.getContext('2d'), viewport, background: '#fff' }).promise;
        const scale = Math.min((slideWidth - 2 * padding) / canvas.width, (slideHeight - 2 * padding) / canvas.height);
        const w = canvas.width * scale, h = canvas.height * scale;
        deck.addSlide().addImage({ data: canvas.toDataURL('image/jpeg', 0.9), x: (slideWidth - w) / 2, y: (slideHeight - h) / 2, w, h });
        canvas.width = canvas.height = 0;
        page.cleanup();
      }
      show('out', 'Writing the PowerPoint file...', false);
      return new Blob([await deck.write({ outputType: 'arraybuffer' })], {
        type: 'application/vnd.openxmlformats-officedocument.presentationml.presentation'
      });
    } finally {
      await task.destroy().catch(() => {});
    }
  }

  const suffix = { slides: 'slides.pptx', rotate: 'rotated.pdf', remove: 'pages-removed.pdf',
    number: 'numbered.pdf', watermark: 'watermarked.pdf' }[mode];
  form.addEventListener('submit', async event => {
    event.preventDefault();
    if (button.disabled) return;
    const file = selectedFile;
    if (!file) { show('out', 'Choose a PDF first.', false); return; }
    if (file.size > maxMb * 1048576) { show('out', 'Choose a file of ' + maxMb + ' MB or less.', false); return; }
    const controls = [...form.elements];
    controls.forEach(control => { control.disabled = true; });
    clearDownloadLinks('links');
    show('out', 'Opening PDF...', false);
    try {
      await loadToolLibraries();
      const output = await (isSlides ? makeSlides(file) : editPdf(file));
      const name = (file.name.replace(/\.pdf$/i, '') || 'document') + '-' + suffix;
      addLink('links', output, name, isSlides ? 'Download PowerPoint' : 'Download PDF');
      show('out', 'Your file is ready.', false);
    } catch (error) {
      show('out', 'Could not finish. ' + (error instanceof Error ? error.message : 'Try another PDF.'), false);
    } finally {
      controls.forEach(control => { control.disabled = false; });
    }
  });
})();
