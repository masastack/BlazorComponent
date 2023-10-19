import { getScrollParents, hasScrollbar } from "utils/getScrollParent";
import { convertToUnit } from "utils/helper";

export interface StrategyProps {
  scrollStrategy: "none" | "block" // | "close" | "reposition"
  contained: boolean | undefined
}

class ScrollStrategies {
  scrollElements: HTMLElement[];
  scrollableParent: Element;
  scrollbarWidth: string;
  root: HTMLElement;

  constructor(root: HTMLElement, contentEl: HTMLElement, props: StrategyProps) {
    if (!root) {
      return
    }

    this.root = root;
    const offsetParent = root.offsetParent

    this.scrollElements = [...new Set([
      ...getScrollParents(contentEl, props.contained ? offsetParent : undefined),
    ])].filter(el => !el.classList.contains('overflow-y-hidden'))

    this.scrollableParent = (el => hasScrollbar(el) && el)(offsetParent || document.documentElement)
    const scrollbarWidth = window.innerWidth - document.documentElement.offsetWidth
    this.scrollbarWidth = convertToUnit(scrollbarWidth);
  }

  hideScroll() {
    if (!this.scrollElements) {
      return
    }

    this.scrollElements.forEach((el, i) => {
      // review: less of the logic in Vuetify3 about offsetLeft and offsetTop

      el.classList.add('overflow-y-hidden')
      el.style.setProperty("padding-inline-end", this.scrollbarWidth)
    })

    if (this.scrollableParent) {
      this.root.classList.add('v-overlay--scroll-blocked')
      this.root.style.setProperty("padding-inline-end", this.scrollbarWidth)
    }
  }

  showScroll() {
    if (!this.scrollElements) {
      return
    }

    this.scrollElements.forEach((el) => {
      // review: less of the logic in Vuetify3 about offsetLeft and offsetTop

      el.classList.remove('overflow-y-hidden')
      el.style.removeProperty("padding-inline-end")
    })

    if (this.scrollableParent) {
      this.root.classList.remove('v-overlay--scroll-blocked')
      this.root.style.removeProperty("padding-inline-end")
    }
  }
}

function init(root: HTMLElement, contentEl: HTMLElement, props: StrategyProps) {
  return new ScrollStrategies(root, contentEl, props);
}

export { init };