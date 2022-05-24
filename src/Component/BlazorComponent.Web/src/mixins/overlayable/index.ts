// KeyboardEvent.keyCode aliases
const keyCodes = Object.freeze({
  enter: 13,
  tab: 9,
  delete: 46,
  esc: 27,
  space: 32,
  up: 38,
  down: 40,
  left: 37,
  right: 39,
  end: 35,
  home: 36,
  del: 46,
  backspace: 8,
  insert: 45,
  pageup: 33,
  pagedown: 34,
  shift: 16,
})
var wheelListenerCaches: { [key: string]: any } = {}

export function addWheelEventListener(overlaySelector, content, dialog) {
  if (!overlaySelector) return

  const listener = function (e) {
    if (e.type === 'keydown') {
      if (
        ['INPUT', 'TEXTAREA', 'SELECT'].includes((e.target as Element).tagName) || (e.target as HTMLElement).isContentEditable
      ) return

      const up = [keyCodes.up, keyCodes.pageup]
      const down = [keyCodes.down, keyCodes.pagedown]

      if (up.includes(e.keyCode)) {
        (e as any).deltaY = -1
      } else if (down.includes(e.keyCode)) {
        (e as any).deltaY = 1
      } else {
        return
      }
    }

    var overlay = document.querySelector(overlaySelector)

    if (overlay === e.target ||
      (e.type !== 'keydown' && e.target === document.body) ||
      checkPath(e, getDom(content), getDom(dialog))) {
      e.preventDefault()
    }
  }

  wheelListenerCaches[`window_${overlaySelector}`] = listener;

  window.addEventListener("wheel", listener, { passive: false })
}

export function removeWheelEventListener(overlaySelector) {
  var listener = wheelListenerCaches[`window_${overlaySelector}`]
  if (listener) {
    window.removeEventListener("wheel", listener, ({ passive: false } as any))
  }
}

function checkPath(e: WheelEvent, content, dialog) {
  const path = composedPath(e)

  if (e.type === 'keydown' && path[0] === document.body) {
    // getSelection returns null in firefox in some edge cases, can be ignored
    const selected = window.getSelection()!.anchorNode as Element
    if (dialog && hasScrollbar(dialog) && isInside(selected, dialog)) {
      return !shouldScroll(dialog, e)
    }
    return true
  }

  for (let index = 0; index < path.length; index++) {
    const el = path[index]

    if (el === document) return true
    if (el === document.documentElement) return true
    if (el === content) return true

    if (hasScrollbar(el as Element)) return !shouldScroll(el as Element, e)
  }

  return true
}

function shouldScroll(el: Element, e: WheelEvent): boolean {
  if (el.hasAttribute('data-app')) return false

  const dir = e.shiftKey || e.deltaX ? 'x' : 'y'
  const delta = dir === 'y' ? e.deltaY : e.deltaX || e.deltaY

  let alreadyAtStart: boolean
  let alreadyAtEnd: boolean
  if (dir === 'y') {
    alreadyAtStart = el.scrollTop === 0
    alreadyAtEnd = el.scrollTop + el.clientHeight === el.scrollHeight
  } else {
    alreadyAtStart = el.scrollLeft === 0
    alreadyAtEnd = el.scrollLeft + el.clientWidth === el.scrollWidth
  }

  const scrollingUp = delta < 0
  const scrollingDown = delta > 0

  if (!alreadyAtStart && scrollingUp) return true
  if (!alreadyAtEnd && scrollingDown) return true
  if ((alreadyAtStart || alreadyAtEnd)) {
    return shouldScroll(el.parentNode as Element, e)
  }

  return false
}

function isInside(el: Element, parent: Element): boolean {
  if (el === parent) {
    return true
  } else if (el === null || el === document.body) {
    return false
  } else {
    return isInside(el.parentNode as Element, parent)
  }
}

/**  Polyfill for Event.prototype.composedPath */
function composedPath(e: Event): EventTarget[] {
  if (e.composedPath) return e.composedPath()

  const path = []
  let el = e.target as Element

  while (el) {
    path.push(el)

    if (el.tagName === 'HTML') {
      path.push(document)
      path.push(window)

      return path
    }

    el = el.parentElement!
  }
  return path
}

function hasScrollbar(el?: Element) {
  if (!el || el.nodeType !== Node.ELEMENT_NODE) return false

  const style = window.getComputedStyle(el)
  return ((['auto', 'scroll'].includes(style.overflowY!) || el.tagName === 'SELECT') && el.scrollHeight > el.clientHeight) ||
    ((['auto', 'scroll'].includes(style.overflowX!)) && el.scrollWidth > el.clientWidth)
}

function getDom(element) {
  if (!element) {
    element = document.body;
  } else if (typeof element === 'string') {
    if (element === 'document') {
      return document.documentElement;
    } else if (element.indexOf('.') > 0) {
      let array = element.split('.');
      let el = document.querySelector(array[0]);
      if (!el) {
        return null;
      }

      element = el[array[1]];
    } else {
      element = document.querySelector(element);
    }
  }
  return element;
}
