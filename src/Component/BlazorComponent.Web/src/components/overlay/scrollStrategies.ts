import { getScrollParents, hasScrollbar } from "utils/getScrollParent";
import { convertToUnit } from "utils/helper";

export interface StrategyProps {
  strategy: "none" | "block"; // | "close" | "reposition"
  contained: boolean | undefined;
}

class ScrollStrategies {
  root: HTMLElement;
  contentEl: HTMLElement;
  options: StrategyProps;

  constructor(
    root: HTMLElement,
    contentEl: HTMLElement,
    options: StrategyProps
  ) {
    if (!root) {
      return;
    }

    this.root = root;
    this.contentEl = contentEl;
    this.options = options;
  }

  bind() {
    if (this.options.strategy === "block") {
      this._blockScroll();
    }
  }

  unbind() {
    if (this.options.strategy === "block") {
      this._unblockScroll();
    }
  }

  _prepareBlock() {
    const offsetParent = this.root.offsetParent;
    const scrollElements = [
      ...new Set([
        ...getScrollParents(
          this.contentEl,
          this.options.contained ? offsetParent : undefined
        ),
      ]),
    ];

    const scrollableParent = ((el) => hasScrollbar(el) && el)(
      offsetParent || document.documentElement
    );

    return { scrollableParent, scrollElements };
  }

  _blockScroll() {
    let { scrollableParent, scrollElements } = this._prepareBlock();

    if (scrollableParent) {
      this.root.classList.add("m-overlay--scroll-blocked");
    }

    const scrollbarWidth =
      window.innerWidth - document.documentElement.offsetWidth;

    scrollElements
      .filter((el) => !el.classList.contains("m-overlay-scroll-blocked"))
      .forEach((el, i) => {
        el.style.setProperty(
          "--m-body-scroll-x",
          convertToUnit(-el.scrollLeft)
        );
        el.style.setProperty("--m-body-scroll-y", convertToUnit(-el.scrollTop));

        if (el !== document.documentElement) {
          el.style.setProperty(
            "--m-scrollbar-offset",
            convertToUnit(scrollbarWidth)
          );
        }

        el.classList.add("m-overlay-scroll-blocked");
      });
  }

  _unblockScroll() {
    const { scrollableParent, scrollElements } = this._prepareBlock();

    scrollElements
      .filter((el) => el.classList.contains("m-overlay-scroll-blocked"))
      .forEach((el, i) => {
        const x = parseFloat(el.style.getPropertyValue("--m-body-scroll-x"));
        const y = parseFloat(el.style.getPropertyValue("--m-body-scroll-y"));

        const scrollBehavior = el.style.scrollBehavior;

        el.style.scrollBehavior = "auto";
        el.style.removeProperty("--m-body-scroll-x");
        el.style.removeProperty("--m-body-scroll-y");
        el.style.removeProperty("--m-scrollbar-offset");
        el.classList.remove("m-overlay-scroll-blocked");

        el.scrollLeft = -x;
        el.scrollTop = -y;

        el.style.scrollBehavior = scrollBehavior;
      });

    if (scrollableParent) {
      this.root.classList.remove("m-overlay--scroll-blocked");
    }
  }
}

function init(root: HTMLElement, contentEl: HTMLElement, props: StrategyProps) {
  return new ScrollStrategies(root, contentEl, props);
}

export { init };
