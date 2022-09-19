const calculate = (e, el) => {
  let localX = 0;
  let localY = 0;

  const offset = el.getBoundingClientRect();
  const target = e;

  localX = target.clientX - offset.left;
  localY = target.clientY - offset.top;

  let radius = 0;
  let scale = 0.3;

  radius = Math.sqrt(el.clientWidth ** 2 + el.clientHeight ** 2) / 2;

  const centerX = `${(el.clientWidth - radius * 2) / 2}px`;
  const centerY = `${(el.clientHeight - radius * 2) / 2}px`;

  const x = `${localX - radius}px`;
  const y = `${localY - radius}px`;

  return { radius, scale, x, y, centerX, centerY };
};

function transform(el, value) {
  el.style.transform = value;
  el.style.webkitTransform = value;
}

function opacity(el, value) {
  el.style.opacity = value.toString();
}

const ripples = {
  /* eslint-disable max-statements */
  show(e, el) {
    const container = document.createElement("span");
    const animation = document.createElement("span");

    container.appendChild(animation);
    container.className = "m-ripple__container";

    const { radius, scale, x, y, centerX, centerY } = calculate(e, el);

    const size = `${radius * 2}px`;
    animation.className = "m-ripple__animation";
    animation.style.width = size;
    animation.style.height = size;

    el.appendChild(container);

    const computed = window.getComputedStyle(el);
    if (computed && computed.position === "static") {
      el.style.position = "relative";
      el.dataset.previousPosition = "static";
    }

    animation.classList.add("m-ripple__animation--enter");
    animation.classList.add("m-ripple__animation--visible");
    transform(
      animation,
      `translate(${x}, ${y}) scale3d(${scale},${scale},${scale})`
    );
    opacity(animation, 0);
    animation.dataset.activated = String(performance.now());

    setTimeout(() => {
      animation.classList.remove("m-ripple__animation--enter");
      animation.classList.add("m-ripple__animation--in");
      transform(animation, `translate(${centerX}, ${centerY}) scale3d(1,1,1)`);
      opacity(animation, 0.25);
    }, 0);
  },

  hide(el) {
    const ripples = el.getElementsByClassName("m-ripple__animation");

    if (ripples.length === 0) return;
    const animation = ripples[ripples.length - 1];

    if (animation.dataset.isHiding) return;
    else animation.dataset.isHiding = "true";

    const diff = performance.now() - Number(animation.dataset.activated);
    const delay = Math.max(250 - diff, 0);

    setTimeout(() => {
      animation.classList.remove("m-ripple__animation--in");
      animation.classList.add("m-ripple__animation--out");
      opacity(animation, 0);

      setTimeout(() => {
        const ripples = el.getElementsByClassName("m-ripple__animation");
        if (ripples.length === 1 && el.dataset.previousPosition) {
          el.style.position = el.dataset.previousPosition;
          delete el.dataset.previousPosition;
        }

        animation.parentNode && el.removeChild(animation.parentNode);
      }, 300);
    }, delay);
  },
};

export function rippleShow(e) {
  ripples.show(e, e.currentTarget);
}

export function rippleHide(e) {
  ripples.hide(e.currentTarget);
}
