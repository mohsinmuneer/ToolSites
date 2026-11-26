import assert from 'node:assert/strict';
import { readFileSync } from 'node:fs';
import vm from 'node:vm';

const cases = [
  [
    "gpa-calculator",
    [
      [
        "grade1",
        "number",
        "4"
      ],
      [
        "credits1",
        "number",
        "3"
      ],
      [
        "grade2",
        "number",
        "3"
      ],
      [
        "credits2",
        "number",
        "4"
      ],
      [
        "grade3",
        "number",
        "3.7"
      ],
      [
        "credits3",
        "number",
        "3"
      ]
    ],
    "GPA: 3.51"
  ],
  [
    "discount-calculator",
    [
      [
        "price",
        "number",
        "120"
      ],
      [
        "first",
        "number",
        "25"
      ],
      [
        "second",
        "number",
        "10"
      ]
    ],
    "Final price: $81.00"
  ],
  [
    "cm-to-feet-inches",
    [
      [
        "cm",
        "number",
        "170"
      ]
    ],
    "5 ft 6.93 in"
  ],
  [
    "hex-to-rgb",
    [
      [
        "hex",
        "text",
        "#0a7d6e"
      ]
    ],
    "rgb(10, 125, 110)"
  ],
  [
    "unix-timestamp-converter",
    [
      [
        "stamp",
        "number",
        "1704067200"
      ],
      [
        "unit",
        "select",
        "Seconds"
      ]
    ],
    "UTC: 2024-01-01T00:00:00.000Z"
  ],
  [
    "url-encoder-decoder",
    [
      [
        "source",
        "textarea",
        "cats & dogs"
      ],
      [
        "operation",
        "select",
        "Encode"
      ]
    ],
    "cats%20%26%20dogs"
  ],
  [
    "random-number-generator",
    [
      [
        "low",
        "number",
        "1"
      ],
      [
        "high",
        "number",
        "100"
      ]
    ],
    "1"
  ],
  [
    "qr-code-generator",
    [
      [
        "source",
        "textarea",
        "https://example.com"
      ]
    ],
    "QR code ready."
  ],
  [
    "days-until-calculator",
    [
      [
        "event",
        "date",
        ""
      ]
    ],
    "7 days until the event"
  ],
  [
    "reverse-text",
    [
      [
        "source",
        "textarea",
        "Hello world!"
      ],
      [
        "operation",
        "select",
        "Characters"
      ]
    ],
    "!dlrow olleH"
  ]
];
const read = name => readFileSync(new URL('../' + name, import.meta.url), 'utf8');

for (const [slug, fields, expected] of cases) {
  const name = slug.split('-').map(part => part[0].toUpperCase() + part.slice(1)).join('');
  const ui = read('Views/ToolUi/' + name + '.cshtml');
  const script = ui.match(/<script>([\s\S]*?)<\/script>/)[1];
  const elements = new Map(fields.map(([id, type, value]) => [id, {
    value,
    defaultValue: value,
    validity: { valid: true },
    labels: [{ textContent: id }],
    get valueAsNumber() { return type === 'date' ? Date.parse(this.value) : Number(this.value); }
  }]));
  const events = new Map();
  const output = [];
  const preview = [];
  let href;
  const canvas = {
    getContext: () => ({ fillRect() {} }),
    toDataURL: () => 'data:image/png;base64,test'
  };
  elements.set('out', { appendChild: el => output.push(el.textContent), set textContent(_) { output.length = 0; } });
  elements.set('tool-inputs', { addEventListener: (event, callback) => events.set(event, callback) });
  elements.set('qr-preview', { replaceChildren: (...children) => preview.splice(0, preview.length, ...children) });
  elements.set('qr-download', { set href(value) { href = value; }, removeAttribute() { href = null; } });
  const box = vm.createContext({
    document: { getElementById: id => elements.get(id), createElement: tag => tag === 'canvas' ? canvas : { style: {} } },
    crypto: { getRandomValues: array => { array[0] = 0; } },
    TextEncoder
  });
  vm.runInContext(read('wwwroot/js/t.js'), box);
  if (slug === 'qr-code-generator') vm.runInContext(read('wwwroot/js/lib/qrcode-generator-1.4.4.js'), box);
  vm.runInContext(script, box);
  const submit = () => events.get('submit')({ preventDefault() {} });
  submit();
  assert.equal(output[0], expected, slug);
  if (slug === 'qr-code-generator') {
    assert.ok(canvas.width >= 174);
    assert.equal(preview[0], canvas);
    assert.match(href, /^data:image\/png/);
  }
  if (slug === 'discount-calculator') assert.equal(output[1], 'You save: $39.00');
  if (slug === 'hex-to-rgb') {
    elements.get('hex').value = '#f09';
    submit();
    assert.equal(output[0], 'rgb(255, 0, 153)');
    elements.get('hex').value = '#zz9';
    submit();
    assert.match(output[0], /Enter a 3- or 6-digit/);
  }
  if (slug === 'url-encoder-decoder') {
    elements.get('operation').value = 'Decode';
    elements.get('source').value = '%ZZ';
    submit();
    assert.match(output[0], /hexadecimal digits/);
  }
  events.get('reset')();
  assert.equal(output.length, 0);
  console.log('PASS ' + slug + ' — ' + expected);
}
