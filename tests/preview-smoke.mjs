import assert from 'node:assert/strict';
const base = 'http://127.0.0.1:5187';
const response = await fetch(base);
assert.equal(response.status, 200);
const home = await response.text();
const routes = [...home.matchAll(/class="directory-card" href="([^"]+)"/g)].map(match => match[1]);
assert.equal(new Set(routes).size, 55);
assert.match(home, /name="robots" content="noindex"/);
for (const route of routes) {
  const page = await fetch(base + route);
  assert.equal(page.status, 200, route);
  assert.equal(((await page.text()).match(/<h1\b/g) || []).length, 1, route);
}
console.log('PASS 55 unique links and 55 HTTP 200 tool pages with one H1 each');

const tabs = await (await fetch('http://127.0.0.1:9223/json/list')).json();
const ws = new WebSocket(tabs.find(tab => tab.type === 'page').webSocketDebuggerUrl);
await new Promise((resolve, reject) => { ws.onopen = resolve; ws.onerror = reject; });
let sequence = 0;
const pending = new Map();
ws.onmessage = event => {
  const message = JSON.parse(event.data);
  const call = pending.get(message.id);
  if (call) {
    pending.delete(message.id);
    if (message.error) call.reject(message.error);
    else call.resolve(message.result);
  }
};
function send(method, params = {}) {
  const id = ++sequence;
  const result = new Promise((resolve, reject) => pending.set(id, { resolve, reject }));
  ws.send(JSON.stringify({ id, method, params }));
  return result;
}
async function evaluate(expression) {
  const result = await send('Runtime.evaluate', { expression, returnByValue: true });
  assert.equal(result.exceptionDetails, undefined, expression);
  return result.result.value;
}
async function navigate(path, ready) {
  await send('Page.navigate', { url: base + path });
  for (let attempt = 0; attempt < 60; attempt++) {
    try {
      if (await evaluate('location.pathname === ' + JSON.stringify(path) + ' && (' + ready + ')')) return;
    } catch {}
    await new Promise(resolve => setTimeout(resolve, 100));
  }
  throw new Error('Browser did not load ' + path);
}
try {
  await send('Emulation.setDeviceMetricsOverride', { width: 360, height: 800, deviceScaleFactor: 1, mobile: true });
  await navigate('/', 'document.readyState === "complete" && !!document.querySelector("#tool-search")');
  assert.equal(await evaluate('document.documentElement.scrollWidth'), 360);
  assert.equal(await evaluate('document.querySelectorAll("[data-tool-card]:not([hidden])").length'), 55);
  assert.equal(await evaluate("document.querySelector('[data-tool-card]').pathname"), '/merge-pdf');
  assert.equal(await evaluate("document.querySelector('[data-tool-card]').getBoundingClientRect().top < innerHeight"), true);
  assert.equal(await evaluate("getComputedStyle(document.querySelector('#pdf-tools .grid')).gridTemplateColumns.split(' ').length"), 2);
  assert.equal(await evaluate("Array.from(document.querySelectorAll('.tool-filters button, #tool-search')).every(element => element.getBoundingClientRect().height >= 44)"), true);
  assert.equal(await evaluate("document.querySelector('.tool-filters').scrollWidth > document.querySelector('.tool-filters').clientWidth"), true);
  assert.equal(await evaluate("document.querySelectorAll('[data-tool-card] .card-mark svg path').length"), 55);
  assert.equal(await evaluate("new Set(Array.from(document.querySelectorAll('#pdf-tools .card-mark path'), icon => icon.getAttribute('d'))).size"), 10);
  if (process.env.TOOLSITE_MOBILE_SCREENSHOT) {
    const screenshot = await send('Page.captureScreenshot', { format: 'png' });
    const { writeFile } = await import('node:fs/promises');
    await writeFile(process.env.TOOLSITE_MOBILE_SCREENSHOT, Buffer.from(screenshot.data, 'base64'));
  }
  await send('Emulation.setDeviceMetricsOverride', { width: 1440, height: 900, deviceScaleFactor: 1, mobile: false });
  assert.equal(await evaluate("document.querySelector('#pdf-tools .grid').getBoundingClientRect().width > 1100"), true);
  const pdfRow = await evaluate("Array.from(document.querySelectorAll('#pdf-tools [data-tool-card]'), card => card.getBoundingClientRect().top).slice(0, 5)");
  assert.equal(new Set(pdfRow).size, 1);
  if (process.env.TOOLSITE_HOMEPAGE_SCREENSHOT) {
    const screenshot = await send('Page.captureScreenshot', { format: 'png' });
    const { writeFile } = await import('node:fs/promises');
    await writeFile(process.env.TOOLSITE_HOMEPAGE_SCREENSHOT, Buffer.from(screenshot.data, 'base64'));
  }
  await send('Emulation.setDeviceMetricsOverride', { width: 360, height: 800, deviceScaleFactor: 1, mobile: true });
  console.log('PASS PDF-first homepage at 360 px and five-card PDF row at 1440 px');
  assert.equal(await evaluate(`document.querySelector('[data-category="PDF Tools"]').click();
    document.querySelectorAll('[data-tool-card]:not([hidden])').length`), 10);
  assert.equal(await evaluate(`const searchInput = document.querySelector('#tool-search');
    searchInput.value = 'merge'; searchInput.dispatchEvent(new Event('input'));
    document.querySelectorAll('[data-tool-card]:not([hidden])').length`), 1);
  await navigate('/discount-calculator', 'document.readyState === "complete" && !!document.querySelector("[type=submit]")');
  assert.equal(await evaluate('document.documentElement.scrollWidth'), 360);
  assert.match(await evaluate("document.querySelector('[type=submit]').click(); document.querySelector('#out, #o1').textContent"), /Final price: \$81\.00/);
  await navigate('/qr-code-generator', 'document.readyState === "complete" && typeof qrcode === "function"');
  assert.match(await evaluate("document.querySelector('[type=submit]').click(); document.querySelector('#qr-download').href"), /^data:image\/png;base64,/);
  console.log('PASS Chrome at 360 px: home width, PDF category and search filters, discount result and QR PNG');
  const { createRequire } = await import('node:module');
  const require = createRequire(import.meta.url);
  const { PDFDocument } = require('../wwwroot/js/lib/pdf-lib.min.js');
  const source = await PDFDocument.create();
  source.setTitle('Fixture '.repeat(30000));
  for (let i = 0; i < 7; i++) {
    const page = source.addPage([200 + i, 200]);
    page.drawText('Page ' + (i + 1), { x: 20, y: 100 });
  }
  const fixture = await source.save({ useObjectStreams: false });
  const file = Buffer.from(fixture).toString('base64');
  const launchResults = {
    'gpa-calculator': [/GPA: 3\.51/, /Total credits: 10/],
    'tip-calculator': [/Tip: \$10\.80/, /Total: \$70\.80/, /Per person: \$35\.40/],
    'discount-calculator': [/Final price: \$81\.00/, /You save: \$39\.00/],
    'days-until-calculator': [/7 days until the event/],
    'car-loan-calculator': [/Monthly payment: \$534\.63/, /Amount financed: \$27,000\.00/, /Total interest: \$5,077\.94/, /Total repaid: \$32,077\.94/],
    'mortgage-calculator': [/Estimated monthly total: \$2,480\.95/, /Principal and interest: \$2,022\.62/, /taxes and insurance: \$458\.33/],
    'hourly-to-salary-calculator': [/Yearly: \$52,000\.00/, /Monthly average: \$4,333\.33/, /Biweekly average: \$2,000\.00/, /Paid week: \$1,000\.00/],
    'sales-tax-calculator': [/Tax: \$7\.25/, /Total: \$107\.25/],
    'split-pdf': [/3 pages extracted/],
    'compress-pdf': [/% smaller/]
  };
  for (const [slug, expected] of Object.entries(launchResults)) {
    await navigate('/' + slug, 'document.readyState === "complete" && !!document.querySelector("#out, #o1")');
    assert.equal(await evaluate('document.documentElement.scrollWidth'), 360, slug);
    const state = await evaluate(`(() => {
      const article = document.querySelector('#tool-card ~ .card');
      const paragraphs = Array.from(article.querySelectorAll(':scope > p:not(.muted)')).slice(0, 5);
      return {
        words: paragraphs.map(p => p.textContent.trim()).join(' ').split(/\\s+/).length,
        faqCount: article.querySelectorAll('h3').length,
        title: document.title,
        description: document.querySelector('meta[name=description]').content,
        schema: Array.from(document.querySelectorAll('script[type="application/ld+json"]')).flatMap(script => JSON.parse(script.textContent))
      };
    })()`);
    assert.ok(state.words >= 300 && state.words <= 450, slug + ' word count ' + state.words);
    assert.equal(state.faqCount, 6);
    assert.ok(state.title.length <= 60 && state.description.length <= 155);
    assert.deepEqual(state.schema.map(item => item['@type']).sort(), ['BreadcrumbList', 'FAQPage', 'WebApplication']);
    if (slug.endsWith('-pdf')) {
      assert.equal(await evaluate("typeof PDFLib"), 'undefined');
      await evaluate(`(() => {
        if (document.querySelector('#rng')) {
          document.querySelector('#rng').value = '2-3,7';
          window.download = blob => { window.testBlob = blob; };
        }
        const transfer = new DataTransfer();
        transfer.items.add(new File([Uint8Array.from(atob('${file}'), c => c.charCodeAt(0))], 'fixture.pdf', { type: 'application/pdf' }));
        document.querySelector('#f').files = transfer.files;
        document.querySelector('#go').click();
      })()`);
      for (let attempt = 0; attempt < 100; attempt++) {
        if (!await evaluate("document.querySelector('#go').disabled")) break;
        await new Promise(resolve => setTimeout(resolve, 100));
      }
      const result = await send('Runtime.evaluate', {
        expression: `(async () => {
          const blob = window.testBlob || await (await fetch(document.querySelector('#links a').href)).blob();
          const pdf = await PDFLib.PDFDocument.load(await blob.arrayBuffer());
          return { widths: pdf.getPages().map(page => page.getWidth()), size: blob.size };
        })()`,
        awaitPromise: true, returnByValue: true
      });
      assert.equal(result.exceptionDetails, undefined);
      const { widths, size } = result.result.value;
      if (slug === 'split-pdf') {
        assert.deepEqual(widths, [201, 202, 206]);
        assert.equal(await evaluate("JSON.stringify(parseRange('7,3,2,3', 7))"), '[1,2,6]');
        assert.equal(await evaluate("parseRange('8', 7)"), null);
        await evaluate("document.querySelector('#mode').value = 'each'; document.querySelector('#go').click()");
        for (let attempt = 0; attempt < 100; attempt++) {
          if (!await evaluate("document.querySelector('#go').disabled")) break;
          await new Promise(resolve => setTimeout(resolve, 100));
        }
        assert.equal(await evaluate("document.querySelectorAll('#links a').length"), 7);
        assert.match(await evaluate("document.querySelector('#out, #o1').textContent"), /7 files created/);
        console.log('PASS Split PDF selection, sorting, invalid pages and single-page downloads');
        continue;
      } else {
        assert.deepEqual(widths, [200, 201, 202, 203, 204, 205, 206]);
        assert.ok(size < fixture.length);
        assert.match(await evaluate("document.querySelector('#out, #o1').textContent"), new RegExp(Math.round(100 - size / fixture.length * 100) + '% smaller'));
        assert.equal((5 - 2) / 5, 0.6);
      }
    } else {
      await evaluate("document.querySelector('#go, #g1, [type=submit]').click()");
    }
    const output = await evaluate("document.querySelector('#out, #o1').textContent");
    for (const match of expected) assert.match(output, match, slug);
    if (slug === 'car-loan-calculator') {
      const zeroRate = await evaluate(`(() => {
        document.querySelector('#apr').value = '0';
        document.querySelector('#go').click();
        return document.querySelector('#out').textContent;
      })()`);
      assert.match(zeroRate, /Monthly payment: \$450\.00/);
      await evaluate("document.querySelector('#term').value = '24.5'; document.querySelector('#go').click()");
      assert.match(await evaluate("document.querySelector('#out').textContent"), /whole-number term/);
    }
    if (slug === 'hourly-to-salary-calculator') {
      await evaluate("document.querySelector('#g2').click()");
      assert.match(await evaluate("document.querySelector('#o2').textContent"), /Hourly rate: \$25\.00/);
      await evaluate("document.querySelector('#wk').value = '50'; document.querySelector('#g1').click()");
      assert.match(await evaluate("document.querySelector('#o1').textContent"), /Yearly: \$50,000\.00/);
    }
    if (slug === 'sales-tax-calculator') {
      await evaluate("document.querySelector('#g2').click()");
      const reverse = await evaluate("document.querySelector('#o2').textContent");
      assert.match(reverse, /Price before tax: \$100\.00/);
      assert.match(reverse, /Tax included: \$7\.25/);
      await evaluate("document.querySelector('#rate').value = '-1'; document.querySelector('#g1').click()");
      assert.match(await evaluate("document.querySelector('#o1').textContent"), /zero or more/);
    }
    if (slug === 'days-until-calculator') {
      const later = Date.UTC(2026, 11, 3), earlier = Date.UTC(2026, 10, 26);
      assert.equal((later - earlier) / 86400000, 7);
      const past = await evaluate(`(() => {
        const now = new Date();
        document.querySelector('#event').valueAsNumber = Date.UTC(now.getFullYear(), now.getMonth(), now.getDate() - 1);
        document.querySelector('[type=submit]').click();
        return document.querySelector('#out').textContent;
      })()`);
      assert.equal(past, '1 day since the event');
    }
    if (slug === 'tip-calculator') {
      const pennies = await evaluate(`(() => {
        document.querySelector('#bill').value = '10';
        document.querySelector('#ppl').value = '3';
        document.querySelector('#pct').value = '0';
        document.querySelector('#go').click();
        return document.querySelector('#out, #o1').textContent;
      })()`);
      assert.match(pennies, /Per person: \$3\.33/);
      await evaluate("document.querySelector('#ppl').value = '1.5'; document.querySelector('#go').click()");
      assert.match(await evaluate("document.querySelector('#out, #o1').textContent"), /whole number/);
    }
    console.log('PASS ' + slug + ' at 360 px, content, metadata, schema and output');
  }
  const letter = await PDFDocument.create();
  letter.addPage([612, 792]).drawText('Letter fixture', { x: 40, y: 700 });
  const letterBase64 = Buffer.from(await letter.save()).toString('base64');
  const heicFixture = process.env.TOOLSITE_HEIC_FIXTURE;
  const heicBase64 = heicFixture
    ? (await (await import('node:fs/promises')).readFile(heicFixture)).toString('base64')
    : null;
  async function completeConversion() {
    for (let attempt = 0; attempt < 200; attempt++) {
      if (!await evaluate("document.querySelector('#go').disabled")) return;
      await new Promise(resolve => setTimeout(resolve, 100));
    }
    throw new Error('Conversion timed out');
  }
  async function firstImage() {
    const result = await send('Runtime.evaluate', {
      expression: `(async () => {
        const link = document.querySelector('#links a');
        if (!link) throw new Error(document.querySelector('#out').textContent + ' ' + document.querySelector('#links').textContent);
        const response = await fetch(link.href);
        const file = await response.blob();
        const bitmap = await createImageBitmap(file);
        const canvas = document.createElement('canvas');
        canvas.width = bitmap.width;
        canvas.height = bitmap.height;
        const ctx = canvas.getContext('2d');
        ctx.drawImage(bitmap, 0, 0);
        return { type: file.type, width: bitmap.width, height: bitmap.height, name: link.download,
          pixel: [...ctx.getImageData(0, 0, 1, 1).data] };
      })()`,
      awaitPromise: true, returnByValue: true
    });
    assert.equal(result.exceptionDetails, undefined, JSON.stringify(result.exceptionDetails));
    return result.result.value;
  }
  for (const [slug, mime, extension, width, height] of [
    ['pdf-to-jpg', 'application/pdf', 'pdf', 918, 1188],
    ['png-to-jpg', 'image/png', 'png', 1200, 800],
    ['jpg-to-png', 'image/jpeg', 'jpg', 1200, 800],
    ['webp-to-jpg', 'image/webp', 'webp', 1280, 720],
    ['heic-to-jpg', 'image/heic', 'heic', null, null]
  ]) {
    await navigate('/' + slug, 'document.readyState === "complete" && !!document.querySelector("#go")');
    assert.equal(await evaluate('document.documentElement.scrollWidth'), 360, slug);
    const state = await evaluate(`(() => {
      const article = document.querySelector('#tool-card ~ .card');
      const paragraphs = Array.from(article.querySelectorAll(':scope > p:not(.muted)')).slice(0, 5);
      return {
        words: paragraphs.map(p => p.textContent.trim()).join(' ').split(/\\s+/).length,
        faqCount: article.querySelectorAll('h3').length,
        title: document.title,
        description: document.querySelector('meta[name=description]').content,
        schema: Array.from(document.querySelectorAll('script[type="application/ld+json"]')).flatMap(script => JSON.parse(script.textContent))
      };
    })()`);
    assert.ok(state.words >= 300 && state.words <= 450, slug + ' word count ' + state.words);
    assert.equal(state.faqCount, 6);
    assert.ok(state.title.length <= 60 && state.description.length <= 155, slug);
    assert.deepEqual(state.schema.map(item => item['@type']).sort(), ['BreadcrumbList', 'FAQPage', 'WebApplication']);
    await evaluate("document.querySelector('#go').click()");
    assert.match(await evaluate("document.querySelector('#out').textContent"), /Choose/);
    if (slug === 'pdf-to-jpg') assert.equal(await evaluate('typeof pdfjsLib'), 'undefined');
    if (slug === 'heic-to-jpg') {
      assert.equal(await evaluate('typeof heic2any'), 'undefined');
      if (!heicBase64) {
        console.log('SKIP HEIC file conversion: set TOOLSITE_HEIC_FIXTURE to a local HEIC fixture');
        continue;
      }
    }
    const fixtureData = slug === 'pdf-to-jpg' ? letterBase64 : slug === 'heic-to-jpg' ? heicBase64 : null;
    const setup = await send('Runtime.evaluate', {
      expression: `(async () => {
        let bytes;
        if (${JSON.stringify(fixtureData)}) {
          bytes = Uint8Array.from(atob(${JSON.stringify(fixtureData)}), char => char.charCodeAt(0));
        } else {
          const canvas = document.createElement('canvas');
          canvas.width = ${width}; canvas.height = ${height};
          const ctx = canvas.getContext('2d');
          ctx.fillStyle = '#008844'; ctx.fillRect(80, 80, 320, 240);
          bytes = await new Promise(resolve => canvas.toBlob(resolve, '${mime}', 0.95));
          if (bytes.type !== '${mime}') throw new Error('Fixture encoding failed.');
        }
        const transfer = new DataTransfer();
        transfer.items.add(new File([bytes], 'fixture.${extension}', { type: '${mime}' }));
        document.querySelector('#f').files = transfer.files;
        document.querySelector('#go').click();
      })()`,
      awaitPromise: true, returnByValue: true
    });
    assert.equal(setup.exceptionDetails, undefined);
    await completeConversion();
    const image = await firstImage();
    assert.equal(image.type, slug === 'jpg-to-png' ? 'image/png' : 'image/jpeg', slug);
    assert.match(image.name, slug === 'jpg-to-png' ? /\.png$/ : /\.jpg$/);
    if (width) assert.deepEqual([image.width, image.height], [width, height]);
    else assert.ok(image.width > 0 && image.height > 0);
    if (slug === 'png-to-jpg' || slug === 'webp-to-jpg') assert.deepEqual(image.pixel, [255, 255, 255, 255]);
    if (slug === 'pdf-to-jpg') {
      await evaluate("document.querySelector('#q').value = '2.5'; document.querySelector('#go').click()");
      await completeConversion();
      const high = await firstImage();
      assert.deepEqual([high.width, high.height], [1530, 1980]);
    }
    if (slug === 'png-to-jpg') {
      await evaluate(`(() => {
        const transfer = new DataTransfer();
        transfer.items.add(new File(['bad image'], '<img>.png', { type: 'image/png' }));
        document.querySelector('#f').files = transfer.files;
        document.querySelector('#go').click();
      })()`);
      await completeConversion();
      assert.match(await evaluate("document.querySelector('#links').textContent"), /<img>\.png/);
      assert.equal(await evaluate("document.querySelector('#links img')"), null);
    }
    console.log('PASS ' + slug + ' at 360 px, content, schema and real output (' + image.width + ' × ' + image.height + ')');
  }
  for (const slug of ['case-converter', 'json-formatter', 'base64-encoder', 'url-encoder-decoder', 'reverse-text']) {
    await navigate('/' + slug, 'document.readyState === "complete" && !!document.querySelector("#out")');
    assert.equal(await evaluate('document.documentElement.scrollWidth'), 360, slug);
    const state = await evaluate(`(() => {
      const article = document.querySelector('#tool-card ~ .card');
      const paragraphs = Array.from(article.querySelectorAll(':scope > p:not(.muted)')).slice(0, 5);
      return {
        words: paragraphs.map(p => p.textContent.trim()).join(' ').split(/\\s+/).length,
        faqCount: article.querySelectorAll('h3').length,
        title: document.title,
        description: document.querySelector('meta[name=description]').content,
        schema: Array.from(document.querySelectorAll('script[type="application/ld+json"]')).flatMap(script => JSON.parse(script.textContent))
      };
    })()`);
    assert.ok(state.words >= 300 && state.words <= 450, slug + ' word count ' + state.words);
    assert.equal(state.faqCount, 6);
    assert.ok(state.title.length <= 60 && state.description.length <= 155, slug);
    assert.deepEqual(state.schema.map(item => item['@type']).sort(), ['BreadcrumbList', 'FAQPage', 'WebApplication']);

    const click = selector => evaluate('document.querySelector(' + JSON.stringify(selector) + ').click()');
    const set = (id, value) => evaluate('document.getElementById(' + JSON.stringify(id) + ').value = ' + JSON.stringify(value));
    const field = () => evaluate("document.querySelector('#t').value");
    const result = () => evaluate("document.querySelector('#out').textContent");
    if (slug === 'case-converter') {
      const modes = [
        ['UPPERCASE', 'NORTH SHORE TICKETS 2026'], ['camelCase', 'northShoreTickets2026'],
        ['snake_case', 'north_shore_tickets_2026'], ['kebab-case', 'north-shore-tickets-2026']
      ];
      for (const [mode, expected] of modes) {
        await set('t', 'North Shore Tickets 2026');
        await evaluate('Array.from(document.querySelectorAll("#btns button")).find(button => button.textContent === ' + JSON.stringify(mode) + ').click()');
        assert.equal(await field(), expected);
      }
    }
    if (slug === 'json-formatter') {
      const example = '{"items":[1,2],"label":"New York"}';
      await click('#fmt');
      assert.equal(await field(), '{\n  "items": [\n    1,\n    2\n  ],\n  "label": "New York"\n}');
      await click('#min');
      assert.equal(await field(), example);
      const precise = '{"n":9007199254740993,"n":1e400,"text":"\\u0041 \\"B\\"","empty":{}}';
      await set('t', precise);
      await click('#fmt');
      assert.match(await field(), /9007199254740993/);
      await click('#min');
      assert.equal(await field(), precise);
      await set('t', '{"x":1,}');
      await click('#fmt');
      assert.match(await result(), /Invalid JSON/);
      assert.equal(await field(), '{"x":1,}');
      await set('t', '"' + 'x'.repeat(199000) + '"');
      await click('#fmt');
      assert.match(await result(), /Formatted/);
    }
    if (slug === 'base64-encoder') {
      await click('#enc');
      assert.equal(await field(), 'QQ==');
      await set('t', 'é');
      await click('#enc');
      assert.equal(await field(), 'w6k=');
      await set('t', 'w6\nk=');
      await click('#dec');
      assert.equal(await field(), 'é');
      await set('t', '\uFEFFé');
      await click('#enc');
      await click('#dec');
      assert.equal(await field(), '\uFEFFé');
      await set('t', '/w==');
      await click('#dec');
      assert.match(await result(), /UTF-8/);
    }
    if (slug === 'url-encoder-decoder') {
      await click('[type=submit]');
      assert.equal(await result(), 'cats%20%26%20dogs');
      await set('source', 'é');
      await click('[type=submit]');
      assert.equal(await result(), '%C3%A9');
      await set('source', '%20');
      await click('[type=submit]');
      assert.equal(await result(), '%2520');
      await set('operation', 'Decode');
      await set('source', '%2B+%20');
      await click('[type=submit]');
      assert.equal(await result(), '++ ');
      await set('source', '%ZZ');
      await click('[type=submit]');
      assert.match(await result(), /hexadecimal/);
    }
    if (slug === 'reverse-text') {
      await click('[type=submit]');
      assert.equal(await result(), '!dlrow olleH');
      await set('source', 'A👍🏽B');
      await click('[type=submit]');
      assert.equal(await result(), 'B👍🏽A');
      await set('source', 'Hello world!');
      await set('operation', 'Words');
      await click('[type=submit]');
      assert.equal(await result(), 'world! Hello');
    }
    console.log('PASS ' + slug + ' at 360 px, content, schema, examples and edge cases');
  }
  const measures = [
    ['cm-to-feet-inches', null, null, [['167.64', '5 ft 6.00 in'], ['182.87746', '6 ft 0.00 in']]],
    ['inches-to-cm', '60.96', ['60', '23.622'], [['7.5', '19.05'], ['0.5', '1.27'], ['0.00000001', '2.5400e-8']]],
    ['pounds-to-kg', '68.0389', ['1', '2.2046'], [['0.5', '0.2268']]],
    ['miles-to-km', '16.0934', ['5', '3.1069'], [['10', '16.0934']]],
    ['fahrenheit-to-celsius', '20', ['20', '68'], [['350', '176.67'], ['-40', '-40'], ['50', '10']]]
  ];
  for (const [slug, initial, reverse, examples] of measures) {
    await navigate('/' + slug, 'document.readyState === "complete" && !!document.querySelector("#out")');
    assert.equal(await evaluate('document.documentElement.scrollWidth'), 360, slug);
    const state = await evaluate(`(() => {
      const article = document.querySelector('#tool-card ~ .card');
      return {
        words: [...article.querySelectorAll(':scope > p:not(.muted)')].slice(0, 5).map(p => p.textContent.trim()).join(' ').split(/\\s+/).length,
        faqCount: article.querySelectorAll('h3').length,
        title: document.title,
        description: document.querySelector('meta[name=description]').content,
        schema: Array.from(document.querySelectorAll('script[type="application/ld+json"]')).flatMap(script => JSON.parse(script.textContent))
      };
    })()`);
    assert.ok(state.words >= 300 && state.words <= 450, slug + ' word count ' + state.words);
    assert.equal(state.faqCount, 6);
    assert.ok(state.title.length <= 60 && state.description.length <= 155, slug);
    assert.deepEqual(state.schema.map(item => item['@type']).sort(), ['BreadcrumbList', 'FAQPage', 'WebApplication']);
    const editField = (id, value) => evaluate(`(() => {
      const element = document.getElementById('${id}');
      element.value = ${JSON.stringify(value)};
      element.dispatchEvent(new Event('input'));
    })()`);
    if (slug === 'cm-to-feet-inches') {
      await evaluate("document.querySelector('[type=submit]').click()");
      assert.match(await evaluate("document.querySelector('#out').textContent"), /5 ft 6\.93 in.*5\.5774/);
      for (const [cm, expected] of examples) {
        await editField('cm', cm);
        await evaluate("document.querySelector('[type=submit]').click()");
        assert.equal(await evaluate("document.querySelector('#out p').textContent"), expected);
      }
      assert.ok(Math.abs(170 / 2.54 - 66.9291) < 0.0001);
    } else {
      assert.equal(await evaluate("document.querySelector('#b').value"), initial);
      await editField('b', reverse[0]);
      assert.equal(await evaluate("document.querySelector('#a').value"), reverse[1]);
      for (const [value, expected] of examples) {
        await editField('a', value);
        assert.equal(await evaluate("document.querySelector('#b').value"), expected);
      }
      await editField('a', '');
      assert.equal(await evaluate("document.querySelector('#b').value"), '');
      assert.match(await evaluate("document.querySelector('#out').textContent"), /Enter a number/);
    }
    console.log('PASS ' + slug + ' at 360 px, content, schema, worked examples and edge cases');
  }
  assert.equal((5.08 * 7.62).toFixed(4), '38.7096');
  assert.equal((60.96 - 60).toFixed(2), '0.96');
  assert.equal((150 * 0.45359237).toFixed(7), '68.0388555');
  const paceSeconds = 10 * 60 / 1.609344;
  assert.equal((paceSeconds / 60).toFixed(2), '6.21');
  assert.equal(Math.round(paceSeconds % 60), 13);
  assert.equal(18 / 1.8, 10);

  await navigate('/tola-to-gram', 'document.readyState === "complete" && !!document.querySelector("#b")');
  assert.equal(await evaluate("document.querySelector('#b').value"), '11.664');
  await navigate('/usd-to-pkr', 'document.readyState === "complete" && !!document.querySelector("#rate")');
  assert.equal(await evaluate(`(() => {
    const change = (id, value) => {
      const field = document.getElementById(id);
      field.value = value;
      field.dispatchEvent(new Event('input'));
    };
    change('rate', '200');
    change('b', '600');
    change('rate', '300');
    return document.querySelector('#a').value;
  })()`), '2');
  console.log('PASS shared converter regression checks for Tola to Gram and USD to PKR');
  for (const slug of ['pdf-to-powerpoint', 'rotate-pdf', 'remove-pdf-pages', 'add-pdf-page-numbers', 'watermark-pdf']) {
    await navigate('/' + slug, 'document.readyState === "complete" && !!document.querySelector("#pdf-submit")');
    assert.equal(await evaluate('document.documentElement.scrollWidth'), 360, slug);
    const state = await evaluate(`(() => {
      const article = document.querySelector('#tool-card ~ .card');
      return {
        words: [...article.querySelectorAll(':scope > p:not(.muted)')].slice(0, 5).map(p => p.textContent.trim()).join(' ').split(/\\s+/).length,
        faqCount: article.querySelectorAll('h3').length,
        title: document.title,
        description: document.querySelector('meta[name=description]').content,
        schema: [...document.querySelectorAll('script[type="application/ld+json"]')].flatMap(script => JSON.parse(script.textContent))
      };
    })()`);
    assert.ok(state.words >= 300 && state.words <= 450, slug + ' word count ' + state.words);
    assert.equal(state.faqCount, 6);
    assert.ok(state.title.length <= 60 && state.description.length <= 155);
    assert.deepEqual(state.schema.map(item => item['@type']).sort(), ['BreadcrumbList', 'FAQPage', 'WebApplication']);
    assert.equal(await evaluate('typeof PDFLib + " " + typeof pdfjsLib'), 'undefined undefined');
    await evaluate("document.querySelector('#pdf-submit').click()");
    assert.match(await evaluate("document.querySelector('#out').textContent"), /Choose a PDF/);

    const length = slug === 'rotate-pdf' ? 8 : ['pdf-to-powerpoint', 'add-pdf-page-numbers'].includes(slug) ? 6 : 7;
    const fixture = await PDFDocument.create();
    for (let index = 0; index < length; index++) {
      fixture.addPage([300 + index, 400]).drawText('Sheet ' + (index + 1), { x: 32, y: 300 });
    }
    const data = Buffer.from(await fixture.save()).toString('base64');
    const options = {
      'rotate-pdf': { 'pdf-pages': '2-3,8' },
      'remove-pdf-pages': { 'pdf-pages': '1-7' },
      'add-pdf-page-numbers': { 'pdf-first': '3', 'pdf-start': '1' },
      'watermark-pdf': { 'pdf-pages': '2-4', 'pdf-text': 'DRAFT', 'pdf-opacity': '25' }
    }[slug] || {};
    await evaluate(`(() => {
      const transfer = new DataTransfer();
      transfer.items.add(new File([Uint8Array.from(atob('${data}'), c => c.charCodeAt(0))], 'fixture.pdf', { type: 'application/pdf' }));
      const field = document.querySelector('#pdf-file');
      field.files = transfer.files;
      field.dispatchEvent(new Event('change'));
      for (const [id, value] of Object.entries(${JSON.stringify(options)})) document.getElementById(id).value = value;
      document.querySelector('#pdf-submit').click();
    })()`);
    async function waitForOutput() {
      for (let attempt = 0; attempt < 150; attempt++) {
        if (!await evaluate("document.querySelector('#pdf-submit').disabled")) return;
        await new Promise(resolve => setTimeout(resolve, 100));
      }
      throw new Error(slug + ' took too long');
    }
    await waitForOutput();
    if (slug === 'remove-pdf-pages') {
      assert.match(await evaluate("document.querySelector('#out').textContent"), /Keep at least one page/);
      await evaluate("document.querySelector('#pdf-pages').value = '2,5-6'; document.querySelector('#pdf-submit').click()");
      await waitForOutput();
    }
    assert.match(await evaluate("document.querySelector('#out').textContent"), /Your file is ready/);
    const result = await send('Runtime.evaluate', {
      expression: `(async () => {
        const link = document.querySelector('#links a');
        const bytes = await (await fetch(link.href)).arrayBuffer();
        if (link.download.endsWith('.pptx')) {
          const zip = await JSZip.loadAsync(bytes);
          const names = Object.keys(zip.files);
          const cover = await zip.file('ppt/slides/slide1.xml').async('string');
          return { slides: names.filter(name => /^ppt\\/slides\\/slide\\d+\\.xml$/.test(name)).length,
            images: names.filter(name => /^ppt\\/media\\/.+\\.jpe?g$/i.test(name)).length,
            coverContainsImage: cover.includes('<p:pic>') };
        }
        const doc = await PDFLib.PDFDocument.load(bytes);
        if (['add-pdf-page-numbers', 'watermark-pdf'].includes('${slug}')) {
          await new Promise((resolve, reject) => {
            const script = document.createElement('script');
            script.src = '/js/lib/pdf.min.js';
            script.onload = resolve;
            script.onerror = reject;
            document.head.append(script);
          });
          pdfjsLib.GlobalWorkerOptions.workerSrc = '/js/lib/pdf.worker.min.js';
          const task = pdfjsLib.getDocument({ data: bytes, isEvalSupported: false });
          try {
            const rendered = await task.promise;
            const text = [];
            for (let index = 1; index <= rendered.numPages; index++) {
              const contents = await (await rendered.getPage(index)).getTextContent();
              text.push(contents.items.map(item => item.str).filter(Boolean));
            }
            return { text };
          } finally {
            await task.destroy();
          }
        }
        return { widths: doc.getPages().map(page => page.getWidth()), turns: doc.getPages().map(page => page.getRotation().angle) };
      })()`,
      awaitPromise: true, returnByValue: true
    });
    assert.equal(result.exceptionDetails, undefined, JSON.stringify(result.exceptionDetails));
    const output = result.result.value;
    if (slug === 'pdf-to-powerpoint') assert.deepEqual(output, { slides: 6, images: 6, coverContainsImage: true });
    if (slug === 'rotate-pdf') assert.deepEqual(output.turns, [0, 90, 90, 0, 0, 0, 0, 90]);
    if (slug === 'remove-pdf-pages') assert.deepEqual(output.widths, [300, 302, 303, 306]);
    if (slug === 'add-pdf-page-numbers') assert.deepEqual(output.text, [['Sheet 1'], ['Sheet 2'], ['Sheet 3', '1'], ['Sheet 4', '2'], ['Sheet 5', '3'], ['Sheet 6', '4']]);
    if (slug === 'watermark-pdf') assert.deepEqual(output.text.map(page => page.includes('DRAFT')), [false, true, true, true, false, false, false]);
    console.log('PASS ' + slug + ' at 360 px, content, schema and real output');
  }
} finally {
  ws.close();
}
