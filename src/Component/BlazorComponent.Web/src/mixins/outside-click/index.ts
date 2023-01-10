class OutsideClick {
  dotNetHelper: DotNet.DotNetObject;
  listener: (e: MouseEvent & KeyboardEvent & FocusEvent) => void;
  excludedElements: HTMLElement[];

  constructor(dotNetHelper: DotNet.DotNetObject, excludeSelectors: string[]) {
    this.dotNetHelper = dotNetHelper;

    if (excludeSelectors) {
      this.excludedElements = excludeSelectors.map((selector) =>
        document.querySelector(selector)
      );

      this.excludedElements = this.excludedElements.filter((e) => !!e);

      console.log("this.excludedElements", this.excludedElements);
    }
  }

  genListener() {
    this.listener = (e) => {
      if (
        this.excludedElements.some((el) => el.contains(e.target as HTMLElement))
      ) {
        return;
      }

      console.log("outside-click js clicked");

      this.dotNetHelper.invokeMethodAsync("OnOutsideClick");
    };
  }

  addListener() {
    console.log("addListener.................");
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
}

function init(dotNetHelper: DotNet.DotNetObject, excludeSelectors: string[]) {
  console.log("init outside click instance");

  var instance = new OutsideClick(dotNetHelper, excludeSelectors);

  instance.addListener();

  return instance;
}

export { init };
