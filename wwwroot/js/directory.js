const search = document.getElementById('tool-search');
const filters = Array.from(document.querySelectorAll('.tool-filters button'));
const groups = Array.from(document.querySelectorAll('[data-tool-group]'), group => ({
  element: group,
  cards: Array.from(group.querySelectorAll('[data-tool-card]'), card => ({
    element: card,
    text: (card.textContent + ' ' + card.pathname.replaceAll('-', ' ')).toLowerCase()
  }))
}));
let category = '';

function filterTools() {
  const words = search.value.trim().toLowerCase().split(/\s+/);
  let total = 0;
  for (const group of groups) {
    const inCategory = !category || group.element.dataset.toolGroup === category;
    let count = 0;
    for (const card of group.cards) {
      const matches = inCategory && words.every(word => card.text.includes(word));
      card.element.hidden = !matches;
      if (matches) count++;
    }
    group.element.hidden = !count;
    total += count;
  }
  document.getElementById('tool-count').textContent = total === 1 ? '1 tool' : total + ' tools';
  document.getElementById('tool-empty').hidden = total > 0;
}

search.addEventListener('input', filterTools);
for (const filter of filters) {
  filter.addEventListener('click', () => {
    category = filter.dataset.category;
    for (const button of filters) button.setAttribute('aria-pressed', String(button === filter));
    filterTools();
  });
}
