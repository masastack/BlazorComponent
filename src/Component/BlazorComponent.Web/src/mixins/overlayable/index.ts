// Utilities
import { addPassiveEventListener, composedPath, getDom, keyCodes } from "../../utils/helper";

var wheelListenerCaches: { [key: string]: any } = {}

export function hideScroll(fullscreen: boolean, overlaySelector: string, content, dialog) {
  if (fullscreen) {
    document.documentElement!.classList.add('overflow-y-hidden')
  } else {
    if (!overlaySelector) return
    const overlay = getDom(overlaySelector);
    const listener = (e) => {
      scrollListener(e, overlay, content, dialog)
    }

    wheelListenerCaches[`window_${overlaySelector}`] = listener;

    addPassiveEventListener(window, 'wheel', listener, { passive: false })
    window.addEventListener('keydown', listener)
  }
}

export function showScroll (overlaySelector) {
  document.documentElement!.classList.remove('overflow-y-hidden')

  var listener = wheelListenerCaches[`window_${overlaySelector}`]
  if (listener) {
    window.removeEventListener('wheel', listener)
    window.removeEventListener('keydown', listener)
  }
}

function scrollListener (e: WheelEvent & KeyboardEvent, overlay, content, dialog) {
  if (e.type === 'keydown') {
    if (
      ['INPUT', 'TEXTAREA', 'SELECT'].includes((e.target as Element).tagName) ||
      // https://github.com/vuetifyjs/vuetify/issues/4715
      (e.target as HTMLElement).isContentEditable
    ) return

    const up:number[] = [keyCodes.up, keyCodes.pageup]
    const down:number[] = [keyCodes.down, keyCodes.pagedown]

    if (up.includes(e.keyCode)) {
      (e as any).deltaY = -1
    } else if (down.includes(e.keyCode)) {
      (e as any).deltaY = 1
    } else {
      return
    }
  }

  if (e.target === overlay ||
    (e.type !== 'keydown' && e.target === document.body) ||
    checkPath(e, content, dialog)) e.preventDefault()
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

function hasScrollbar(el?: Element) {
  if (!el || el.nodeType !== Node.ELEMENT_NODE) return false

  const style = window.getComputedStyle(el)
  return ((['auto', 'scroll'].includes(style.overflowY!) || el.tagName === 'SELECT') && el.scrollHeight > el.clientHeight) ||
    ((['auto', 'scroll'].includes(style.overflowX!)) && el.scrollWidth > el.clientWidth)
}
