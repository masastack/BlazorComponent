class Intersect {
  el: HTMLElement;
  dotnetHelper: DotNet.DotNetObject;
  observer: IntersectionObserver;

  constructor(
    el: HTMLElement,
    dotnetHelper: DotNet.DotNetObject,
    options?: IntersectionObserverInit
  ) {
    this.el = el;
    this.dotnetHelper = dotnetHelper;

    this.observer = new IntersectionObserver(
      (
        entries: IntersectionObserverEntry[] = [],
        observer: IntersectionObserver
      ) => {
        const isIntersecting = entries.some((entry) => entry.isIntersecting);
        if (isIntersecting) {
          dotnetHelper.invokeMethodAsync("invoke");
        }
      },
      options ?? {}
    );

    this.observer.observe(el);
  }

  dispose() {
    if (this.dotnetHelper["dispose"]) {
      this.observer.unobserve(this.el);
      this.dotnetHelper["dispose"]();
    }
  }
}

function init(
  el: HTMLElement,
  dotnetHelper: DotNet.DotNetObject,
  options?: IntersectionObserverInit
) {
  return new Intersect(el, dotnetHelper, options);
}

export { init };
