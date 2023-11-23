interface RippleOptions {
  class?: string;
  center?: boolean;
  circle?: boolean;
}

type RippleEvent = (MouseEvent | TouchEvent | KeyboardEvent) & {
  rippleStop?: boolean;
};

const DELAY_RIPPLE = 80

function isTouchEvent(e: RippleEvent): e is TouchEvent {
  return e.constructor.name === "TouchEvent";
}

function isKeyboardEvent(e: RippleEvent): e is KeyboardEvent {
  return e.constructor.name === "KeyboardEvent";
}

const calculate = (e: RippleEvent, el: HTMLElement, options: RippleOptions) => {
  let localX = 0;
  let localY = 0;

  if (!isKeyboardEvent(e)) {
    const offset = el.getBoundingClientRect();
    const target = isTouchEvent(e) ? e.touches[e.touches.length - 1] : e;

    localX = target.clientX - offset.left;
    localY = target.clientY - offset.top;
  }

  let radius = 0;
  let scale = 0.3;

  if (el._ripple && el._ripple.circle) {
    scale = 0.15;
    radius = el.clientWidth / 2;
    radius = options.center
      ? radius
      : radius + Math.sqrt((localX - radius) ** 2 + (localY - radius) ** 2) / 4;
  } else {
    radius = Math.sqrt(el.clientWidth ** 2 + el.clientHeight ** 2) / 2;
  }

  const centerX = `${(el.clientWidth - radius * 2) / 2}px`;
  const centerY = `${(el.clientHeight - radius * 2) / 2}px`;

  const x = options.center ? centerX : `${localX - radius}px`;
  const y = options.center ? centerY : `${localY - radius}px`;

  return { radius, scale, x, y, centerX, centerY };
};

function transform(el, value) {
  el.style.transform = value;
  el.style.webkitTransform = value;
}

export const ripples = {
  show(e: RippleEvent, el: HTMLElement, options: RippleOptions = {}) {
    if (!el._ripple || !el._ripple.enabled) {
      return;
    }

    const container = document.createElement("span");
    const animation = document.createElement("span");

    container.appendChild(animation);
    container.className = "m-ripple__container";

    if (options.class) {
      container.className += ` ${options.class}`;
    }

    const { radius, scale, x, y, centerX, centerY } = calculate(e, el, options);

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
    animation.dataset.activated = String(performance.now());

    setTimeout(() => {
      animation.classList.remove("m-ripple__animation--enter");
      animation.classList.add("m-ripple__animation--in");
      transform(animation, `translate(${centerX}, ${centerY}) scale3d(1,1,1)`);
    }, 0);
  },

  hide (el: HTMLElement | null) {
    if (!el || !el._ripple || !el._ripple.enabled) return

    const ripples = el.getElementsByClassName('m-ripple__animation')

    if (ripples.length === 0) return
    const animation = ripples[ripples.length - 1]

    if (animation.dataset.isHiding) return
    else animation.dataset.isHiding = 'true'

    const diff = performance.now() - Number(animation.dataset.activated)
    const delay = Math.max(250 - diff, 0)

    setTimeout(() => {
      animation.classList.remove('m-ripple__animation--in')
      animation.classList.add('m-ripple__animation--out')

      setTimeout(() => {
        const ripples = el.getElementsByClassName('m-ripple__animation')
        if (ripples.length === 1 && el.dataset.previousPosition) {
          el.style.position = el.dataset.previousPosition
          delete el.dataset.previousPosition
        }

        if (animation.parentNode?.parentNode === el) el.removeChild(animation.parentNode)
      }, 300)
    }, delay)
  },
};

export function rippleShow(e: RippleEvent) {
  const value: RippleOptions = {}
  const element = e.currentTarget as HTMLElement

  if (!element || !element._ripple || element._ripple.touched || e.rippleStop) return

  // Don't allow the event to trigger ripples on any other elements
  e.rippleStop = true

  if (isTouchEvent(e)) {
    element._ripple.touched = true
    element._ripple.isTouch = true
  } else {
    // It's possible for touch events to fire
    // as mouse events on Android/iOS, this
    // will skip the event call if it has
    // already been registered as touch
    if (element._ripple.isTouch) return
  }
  value.center = element._ripple.centered || isKeyboardEvent(e)
  if (element._ripple.class) {
    value.class = element._ripple.class
  }

  if (isTouchEvent(e)) {
    // already queued that shows or hides the ripple
    if (element._ripple.showTimerCommit) return

    element._ripple.showTimerCommit = () => {
      ripples.show(e, element, value)
    }
    element._ripple.showTimer = window.setTimeout(() => {
      if (element && element._ripple && element._ripple.showTimerCommit) {
        element._ripple.showTimerCommit()
        element._ripple.showTimerCommit = null
      }
    }, DELAY_RIPPLE)
  } else {
    ripples.show(e, element, value)
  }
}

export function rippleHide(e: Event) {
  const element = e.currentTarget as HTMLElement | null
  if (!element || !element._ripple) return

  window.clearTimeout(element._ripple.showTimer)

  // The touch interaction occurs before the show timer is triggered.
  // We still want to show ripple effect.
  if (e.type === 'touchend' && element._ripple.showTimerCommit) {
    element._ripple.showTimerCommit()
    element._ripple.showTimerCommit = null

    // re-queue ripple hiding
    element._ripple.showTimer = Number(setTimeout(() => {
      rippleHide(e)
    }))
    return
  }

  window.setTimeout(() => {
    if (element._ripple) {
      element._ripple.touched = false
    }
  })

  ripples.hide(element)
}
