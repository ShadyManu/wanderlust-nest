export const stripHTML = (input: string, maxLetters: number = 0) => {
  const div = document.createElement('div');
  div.innerHTML = input;
  const text = div.textContent ?? div.innerText ?? '';
  if (maxLetters === 0 || text.length <= maxLetters) {
    return text;
  }

  return text.slice(0, maxLetters) + '...';
};
