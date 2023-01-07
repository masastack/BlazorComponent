import { parseMouseEvent } from "../../events/EventType";
import Delayable from "../delayable";

type Listeners = Record<
  string,
  (e: MouseEvent & KeyboardEvent & FocusEvent) => void
>;

class Activatable extends Delayable {
  activator: HTMLElement;
  disabled: boolean;
  openOnClick: boolean;
  openOnHover: boolean;
  openOnFocus: boolean;

  isActive: boolean;
  listeners: Listeners;

  constructor(
    selector: string,
    disabled: boolean,
    openOnClick: boolean,
    openOnHover: boolean,
    openOnFocus: boolean,
    openDelay: number,
    closeDelay: number,
    dotNetHelper: DotNet.DotNetObject
  ) {
    super(openDelay, closeDelay, dotNetHelper);

    const activator = document.querySelector(selector);

    if (!activator) return;

    this.activator = activator as HTMLElement;
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
      this.activator.addEventListener(key, this.listeners[key] as any);
    }
  }

  genActivatorListeners() {
    if (this.disabled) return {};

    const listeners: Listeners = {};

    if (this.openOnHover) {
      listeners.mouseenter = (e: MouseEvent) => {
        console.log("mouseenter", this.isActive);
        this.runDelay("open");
      };
      listeners.mouseleave = (e: MouseEvent) => {
        console.log("mouseleave", this.isActive);
        this.runDelay("close");
      };
    } else if (this.openOnClick) {
      listeners.click = (e: MouseEvent) => {
        if (this.activator) this.activator.focus();

        e.stopPropagation();

        this.dotNetHelper.invokeMethodAsync("OnClick", parseMouseEvent(e));

        this.setActive(!this.isActive);
      };
    }

    if (this.openOnFocus) {
      listeners.focus = (e: FocusEvent) => {
        e.stopPropagation();

        this.runDelay("open");
      };

      listeners.blur = (e: FocusEvent) => {
        this.runDelay("close");
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

  resetActivator(
    disabled: boolean,
    openOnHover: boolean,
    openOnFocus: boolean
  ) {
    this.disabled = disabled;
    this.openOnHover = openOnHover;
    this.openOnFocus = openOnFocus;

    this.removeActivatorEvents();
    this.addActivatorEvents();
  }

  runDelaying(val: boolean) {
    console.log('runDelaying', val)
    this.runDelay(val ? "open" : "close");
  }
}

function init(
  selector: string,
  disabled: boolean,
  openOnClick: boolean,
  openOnHover: boolean,
  openOnFocus: boolean,
  openDelay: number,
  closeDelay: number,
  dotNetHelper: DotNet.DotNetObject
) {
  var instance = new Activatable(
    selector,
    disabled,
    openOnClick,
    openOnHover,
    openOnFocus,
    openDelay,
    closeDelay,
    dotNetHelper
  );

  instance.addActivatorEvents();

  return instance;
}

export { init };
