type Listeners = Record<
  string,
  (e: MouseEvent & KeyboardEvent & FocusEvent) => void
>;

class Activatable {
  activator: HTMLElement;
  disabled: boolean;
  openOnClick: boolean;
  openOnHover: boolean;
  openOnFocus: boolean;
  dotNetHelper: DotNet.DotNetObject;

  isActive: boolean;
  listeners: Listeners;

  constructor(
    selector: string,
    disabled: boolean,
    openOnClick: boolean,
    openOnHover: boolean,
    openOnFocus: boolean,
    dotNetHelper: DotNet.DotNetObject
  ) {
    this.activator = document.querySelector(selector);
    this.disabled = disabled;
    this.openOnClick = openOnClick;
    this.openOnHover = openOnHover;
    this.openOnFocus = openOnFocus;
    this.dotNetHelper = dotNetHelper;
  }

  addActivatorEvents() {
    if (!this.activator || this.disabled) return;

    this.listeners = this.genActivatorListeners();
    const keys = Object.keys(this.listeners);

    for (const key of keys) {
      this.activator.addEventListener(key, this.listeners[key]);
    }
  }

  genActivatorListeners() {
    if (this.disabled) return {};

    const listeners: Listeners = {};

    if (this.openOnHover) {
      listeners.mouseenter = (e: MouseEvent) => {
        console.log("mouseenter");
        this.runDelay("open");
      };
      listeners.mouseleave = (e: MouseEvent) => {
        console.log("mouseleave");
        this.runDelay("close");
      };
    } else if (this.openOnClick) {
      listeners.click = (e: MouseEvent) => {
        if (this.activator) this.activator.focus();

        e.stopPropagation();

        this.setActive(!this.isActive);
      };
    }

    if (this.openOnFocus) {
      listeners.focus = (e: FocusEvent) => {
        e.stopPropagation();

        this.setActive(!this.isActive);
      };
    }

    return listeners;
  }

  removeActivatorEvents() {
    if (!this.activator) return;

    const keys = Object.keys(this.listeners);

    for (const key of keys) {
      (this.activator as any).removeEventListener(key, this.listeners[key]);
    }

    this.listeners = {};
  }

  resetActivator() {
    this.removeActivatorEvents();
    this.addActivatorEvents();
  }

  runDelay(type: "open" | "close") {
    this.setActive({ open: true, close: false }[type]);
  }

  setActive(active: boolean) {
    this.isActive = active;
    this.dotNetHelper.invokeMethodAsync("SetActive", this.isActive);
  }
}

const instances: Record<string, Activatable> = {};

function init(
  selector: string,
  disabled: boolean,
  openOnClick: boolean,
  openOnHover: boolean,
  openOnFocus: boolean,
  dotNetHelper: DotNet.DotNetObject
) {
  const key = dotNetHelper["_id"];
  console.log("key", key, dotNetHelper);

  var instance = new Activatable(
    selector,
    disabled,
    openOnClick,
    openOnHover,
    openOnFocus,
    dotNetHelper
  );

  instances[key] = instance;

  instance.addActivatorEvents();
}

function reset(dotNetHelper: DotNet.DotNetObject) {
  const key = dotNetHelper["_id"];
  const instance = instances[key];
  instance.resetActivator();
}

export { init, reset };
