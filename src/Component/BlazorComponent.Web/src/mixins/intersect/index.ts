interface IntersectionObserverOptions {
  rootSelector?: string;
  rootMargin?: string;
  threshold: number[];
  once: boolean;
}

function observe(
  el: HTMLElement,
  handle: DotNet.DotNetObject,
  options?: IntersectionObserverOptions
) {
  if (!handle) {
    throw new Error("the handle cannot be null");
  }

  if (!el) {
    handle.dispose();
    return;
  }

  const once = options?.once ?? false;
  const standardOptions = formatToStandardOptions(options);

  const observer = new IntersectionObserver(
    async (
      entries: IntersectionObserverEntry[] = [],
      observer: IntersectionObserver
    ) => {
      const isIntersecting = entries.some((entry) => entry.isIntersecting);
      if (!once || isIntersecting) {
        await handle.invokeMethodAsync("Invoke", { isIntersecting });
      }

      if (isIntersecting && once) {
        unobserve(el);
      }
    },
    standardOptions
  );

  el["_observe"] = Object(el["_observe"]);
  el["_observe"] = { handle, observer };

    observer.observe(el);
}

function unobserve(el: HTMLElement) {
  if (!el) return;

  const observe = el["_observe"];
  if (!observe) return;

  observe.observer.unobserve(el);
  observe.handle.dispose();
  delete el["_observe"];
}

function formatToStandardOptions(
  options?: IntersectionObserverOptions
): IntersectionObserverInit | null {
  if (!options) {
    return null;
  }

  return {
    rootMargin: options.rootMargin,
    root: options.rootSelector
      ? document.querySelector(options.rootSelector)
      : null,
    threshold: options.threshold,
  };
}

export { observe, unobserve };
