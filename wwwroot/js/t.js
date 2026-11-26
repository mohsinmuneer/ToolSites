// Shared helpers for client-side tools
const $ = (id) => document.getElementById(id);
const num = (id) => { const v = parseFloat($(id).value); return Number.isFinite(v) ? v : 0; };
const usd = (n) => n.toLocaleString('en-US', { style: 'currency', currency: 'USD' });
const fix = (n, d = 2) => n.toLocaleString('en-US', { maximumFractionDigits: d });
// show lines of text (safe: textContent only). first line bold.
function show(id, lines, bold = true) {
  const box = $(id); box.textContent = '';
  (Array.isArray(lines) ? lines : [lines]).forEach((t, i) => {
    const p = document.createElement('p');
    if (bold && i === 0) p.style.fontWeight = '700';
    else if (i > 0) p.className = 'muted';
    p.textContent = t; box.appendChild(p);
  });
}
function download(blob, name) {
  const a = document.createElement('a');
  a.href = URL.createObjectURL(blob); a.download = name;
  document.body.appendChild(a); a.click(); a.remove();
  setTimeout(() => URL.revokeObjectURL(a.href), 10000);
}
function addLink(listId, blob, name, label) {
  const li = document.createElement('li'); const a = document.createElement('a');
  a.href = URL.createObjectURL(blob); a.download = name; a.textContent = (label || name) + ' (' + kb(blob.size) + ')';
  li.appendChild(a); $(listId).appendChild(li);
}
const kb = (b) => b >= 1048576 ? (b / 1048576).toFixed(2) + ' MB' : Math.max(1, Math.round(b / 1024)) + ' KB';
function loadImage(file) {
  return new Promise((res, rej) => {
    const u = URL.createObjectURL(file), i = new Image();
    i.onload = () => res(i); i.onerror = () => rej(new Error('Could not read this image.'));
    i.src = u;
  });
}
const canvasBlob = (c, type, q) => new Promise((r) => c.toBlob(r, type, q));
const readBytes = (file) => file.arrayBuffer().then((b) => new Uint8Array(b));
function copyText(text, btn) {
  navigator.clipboard.writeText(text).then(() => { const o = btn.textContent; btn.textContent = 'Copied'; setTimeout(() => btn.textContent = o, 1200); });
}
function randInt(n) {
  const lim = Math.floor(0x100000000 / n) * n, a = new Uint32Array(1);
  do { crypto.getRandomValues(a); } while (a[0] >= lim);
  return a[0] % n;
}
function ageParts(b, t) {
  let y = t.getFullYear() - b.getFullYear(), m = t.getMonth() - b.getMonth(), d = t.getDate() - b.getDate();
  if (d < 0) { m--; d += new Date(t.getFullYear(), t.getMonth(), 0).getDate(); }
  if (m < 0) { y--; m += 12; }
  return [y, m, d];
}
const parseDate = (v) => v ? new Date(v + 'T00:00:00') : null;

let toolLibraryLoad;
function loadToolLibraries() {
  if (toolLibraryLoad) return toolLibraryLoad;
  const libraries = $('tool-card').dataset.toolLibraries.split(',').filter(Boolean);
  toolLibraryLoad = Promise.all(libraries.map(name => new Promise((resolve, reject) => {
    const script = document.createElement('script');
    script.src = '/js/lib/' + name;
    script.onload = resolve;
    script.onerror = () => {
      script.remove();
      reject(new Error('Could not load the PDF tools. Please try again.'));
    };
    document.head.appendChild(script);
  }))).catch(error => {
    toolLibraryLoad = null;
    throw error;
  });
  return toolLibraryLoad;
}


function fitsCanvas(width, height) {
  return width > 0 && height > 0 && width <= 8192 && height <= 8192 && width * height <= 16000000;
}
function clearDownloadLinks(id) {
  const list = $(id);
  for (const link of list.querySelectorAll('a[href^="blob:"]')) URL.revokeObjectURL(link.href);
  list.textContent = '';
}
function showFileError(filename, message) {
  const item = document.createElement('li');
  item.textContent = filename + ': ' + message;
  $('links').appendChild(item);
}


async function copyFromEditor(editorId, statusId) {
  const editor = $(editorId);
  if (!editor.value.length) { show(statusId, 'Enter some text before copying.', false); return; }
  try {
    await navigator.clipboard.writeText(editor.value);
    show(statusId, 'Copied.', false);
  } catch {
    editor.focus();
    editor.select();
    show(statusId, 'Text selected. Use your keyboard or menu to copy it.', false);
  }
}

