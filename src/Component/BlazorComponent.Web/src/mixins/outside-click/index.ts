class OutsideClick {
  dotNetHelper: DotNet.DotNetObject;
  listener: (e: MouseEvent & KeyboardEvent & FocusEvent) => void;
  mousedownListener: (e: MouseEvent) => void;
  excludedSelectors: string[];
  lastMousedownWasOutside: boolean = true;

  constructor(dotNetHelper: DotNet.DotNetObject, excludedSelectors: string[]) {
    this.dotNetHelper = dotNetHelper;
    this.excludedSelectors = excludedSelectors;
  }

  genListeners() {
    this.listener = (e) => {
      if (this.checkEvent(e)) return;

      if (this.lastMousedownWasOutside) {
        this.dotNetHelper.invokeMethodAsync("OnOutsideClick");
      }
    };

    this.mousedownListener = (e) => {
      this.lastMousedownWasOutside = !this.checkEvent(e);
    };
  }

  addListeners() {
    this.genListeners();
    document.addEventListener("click", this.listener, true);
    document.addEventListener("mousedown", this.mousedownListener, true);
  }

  removeListeners() {
    document.removeEventListener("click", this.listener);
    document.removeEventListener("mousedown", this.mousedownListener);
  }

  resetListener() {
    this.removeListeners();
    this.addListeners();
  }

  updateExcludeSelectors(selectors: string[]) {
    this.excludedSelectors = selectors;
  }

  checkEvent(e: MouseEvent) {
    return this.excludedSelectors.some((selector) => {
      const el = document.querySelector(selector);
      if (!el) return false;

      return el.contains(e.target as HTMLElement);
    });
  }
}

function init(dotNetHelper: DotNet.DotNetObject, excludeSelectors: string[]) {
  var instance = new OutsideClick(dotNetHelper, excludeSelectors);

  instance.addListeners();

  return instance;
}

export { init };
