import { parseMouseEvent } from "../../events/EventType";
import Delayable from "../delayable";

type Listeners = Record<
  string,
  (e: MouseEvent & KeyboardEvent & FocusEvent) => void
>;

class Activatable extends Delayable {
  activator?: HTMLElement;
  popupElement: HTMLElement;
  disabled: boolean;
  openOnClick: boolean;
  openOnHover: boolean;
  openOnFocus: boolean;

  closeOnOutsideClick: boolean;
  closeOnContentClick: boolean;

  isActive: boolean;
  activatorListeners: Listeners;
  popupListeners: Listeners;
  documentListeners: Listeners;

  constructor(
    activatorSelector: string,
    disabled: boolean,
    openOnClick: boolean,
    openOnHover: boolean,
    openOnFocus: boolean,
    openDelay: number,
    closeDelay: number,
    dotNetHelper: DotNet.DotNetObject
  ) {
    super(openDelay, closeDelay, dotNetHelper);

    const activator = document.querySelector(activatorSelector);
    if (activator) {
      this.activator = activator as HTMLElement;
    }

    this.disabled = disabled;
    this.openOnClick = openOnClick;
    this.openOnHover = openOnHover;
    this.openOnFocus = openOnFocus;
    this.dotNetHelper = dotNetHelper;
  }

  //#region activators

  addActivatorEvents() {
    if (!this.activator || this.disabled) return;

    this.popupListeners = this.genActivatorListeners();
    const keys = Object.keys(this.popupListeners);

    for (const key of keys) {
      this.activator.addEventListener(key, this.popupListeners[key] as any);
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

    const keys = Object.keys(this.popupListeners);

    for (const key of keys) {
      this.activator.removeEventListener(key, this.popupListeners[key]);
    }

    this.popupListeners = {};
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
    console.log("runDelaying", val);
    this.runDelay(val ? "open" : "close");
  }

  //#endregion

  //#region popups

  addPopupEvents2(
    popupSelector: string,
    closeOnOutsideClick: boolean,
    closeOnContentClick: boolean
  ) {
    const popup = document.querySelector(popupSelector);
    if (!popup) return;

    this.popupElement = popup as HTMLElement;
    this.closeOnOutsideClick = closeOnOutsideClick;
    this.closeOnContentClick = closeOnContentClick;
  }

  addPopupEvents() {
    if (!this.popupElement || this.disabled) return;

    this.popupListeners = this.genPopupListeners();
    const keys = Object.keys(this.popupListeners);

    for (const key of keys) {
      this.popupElement.addEventListener(key, this.popupListeners[key] as any);
    }
  }

  addDocumentEvents() {
    if (this.disabled) return;

    this.documentListeners = this.genDocumentListeners();
    const keys = Object.keys(this.documentListeners);

    for (const key of keys) {
      document.addEventListener(key, this.documentListeners[key] as any);
    }
  }

  removePopupEvents() {
    if (!this.popupElement) return;

    const keys = Object.keys(this.popupListeners);

    for (const key of keys) {
      this.popupElement.removeEventListener(key, this.popupListeners[key]);
    }

    this.popupListeners = {};
  }

  removeDocumentEvents() {
    const keys = Object.keys(this.documentListeners);

    for (const key of keys) {
      document.removeEventListener(key, this.documentListeners[key]);
    }

    this.documentListeners = {};
  }

  genPopupListeners() {
    if (this.disabled) return;

    const listeners: Listeners = {};

    if (this.openOnHover) {
      listeners.mouseenter = (e) => {
        console.log("content mouseenter");
        this.setActive(true);
      };

      listeners.mouseleave = (e) => {
        console.log("content mouseleave");
        this.setActive(false);
      };
    }

    if (this.closeOnContentClick) {
      listeners.click = (e) => {
        console.log("content click", e);

        this.setActive(false);
      };
    }

    return listeners;
  }

  genDocumentListeners() {
    const listener: Listeners = {};

    if (this.closeOnOutsideClick) {
      listener.click = () => {
        console.log("outside click");
        this.setActive(false);
      };
    }

    return listener;
  }

  //#endregion
}

function init(
  activatorSelector: string,
  disabled: boolean,
  openOnClick: boolean,
  openOnHover: boolean,
  openOnFocus: boolean,
  openDelay: number,
  closeDelay: number,
  dotNetHelper: DotNet.DotNetObject
) {
  var instance = new Activatable(
    activatorSelector,
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

export { init, Activatable };
