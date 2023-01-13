class OutsideClick {
  dotNetHelper: DotNet.DotNetObject;
  listener: (e: MouseEvent & KeyboardEvent & FocusEvent) => void;
  excludedSelectors: string[];

  constructor(dotNetHelper: DotNet.DotNetObject, excludedSelectors: string[]) {
    this.dotNetHelper = dotNetHelper;
    this.excludedSelectors = excludedSelectors;
  }

  genListener() {
    this.listener = (e) => {
      if (
        this.excludedSelectors.some((selector) => {
          const el = document.querySelector(selector);
          if (!el) return false;

          return el.contains(e.target as HTMLElement);
        })
      ) {
        return;
      }

      this.dotNetHelper.invokeMethodAsync("OnOutsideClick");
    };
  }

  addListener() {
    this.genListener();
    document.addEventListener("click", this.listener, true);
  }

  removeListener() {
    document.removeEventListener("click", this.listener);
  }

  resetListener() {
    this.removeListener();
    this.addListener();
  }

  updateExcludeSelectors(selectors: string[]) {
    this.excludedSelectors = selectors;
    // this.resetListener();
  }
}

function init(dotNetHelper: DotNet.DotNetObject, excludeSelectors: string[]) {
  var instance = new OutsideClick(dotNetHelper, excludeSelectors);

  instance.addListener();

  return instance;
}

export { init };
