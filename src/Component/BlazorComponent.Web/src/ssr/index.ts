export type MasaBlazorApplication = {
  bar?: number;
  top: number;
  right: number;
  bottom: number;
  left: number;
  footer?: number;
  insetFooter?: number;
};

export type MasaBlazorSsrPassiveState = {
  application: MasaBlazorApplication;
};

export type MasaBlazorSsrState = {
  culture?: string;
  rtl?: boolean;
  dark?: boolean;
  passive: MasaBlazorSsrPassiveState;
};

export const MASA_BLAZOR_SSR_STATE = "masablazor@ssr-state";

export function setTheme(dark: boolean) {
  console.log("[index.ts] setTheme", dark);
  const selector = `.${getThemeCss(!dark)}:not(.theme--independent)`
  const elements = document.querySelectorAll(selector);
  for (let i = 0; i < elements.length; i++) {
    elements[i].classList.remove(getThemeCss(!dark));
    elements[i].classList.add(getThemeCss(dark));
  }

  updateStorage({ dark });
}

export function setCulture(culture: string) {
  console.log("[index.ts] setCulture", culture);
  const app = getApp();
  if (!app) return;

  updateStorage({ culture });
}

export function setRtl(rtl: boolean, updateCache: boolean = true) {
  console.log("[index.ts] setRtl", rtl);
  const app = getApp();
  if (!app) return;

  const rtlCss = "m-application--is-rtl";
  if (!rtl) {
    app.classList.remove(rtlCss);
  } else if (!app.classList.contains(rtlCss)) {
    app.classList.add(rtlCss);
  }

  if (updateCache) {
    updateStorage({ rtl });
  }
}

export function updateMain(application: MasaBlazorApplication) {
  const main: HTMLElement = document.querySelector(".m-main");

  const newApplication: MasaBlazorApplication = {
    top: application.top ?? getPadding("Top"),
    right: application.right ?? getPadding("Right"),
    bottom: application.bottom ?? getPadding("Bottom"),
    left: application.left ?? getPadding("Left"),
  };

  restoreMain(newApplication);

  function getPadding(prop: "Top" | "Right" | "Bottom" | "Left") {
    return Number(main.style[`padding${prop}`].match(/\d+/)[0]);
  }
}

export function updatePassiveState(passive: MasaBlazorSsrPassiveState) {
  const oldState = getState() ?? {};
  const state: MasaBlazorSsrState = {
    ...oldState,
    passive,
  };
  console.log("[updatePassiveState] state", state);
  localStorage.setItem(MASA_BLAZOR_SSR_STATE, JSON.stringify(state));
}

export function getThemeCss(dark: boolean) {
  return dark ? "theme--dark" : "theme--light";
}

function getApp() {
  return document.querySelector(".m-application");
}

function updateStorage(obj: Partial<MasaBlazorSsrState>) {
  const stateStr = localStorage.getItem(MASA_BLAZOR_SSR_STATE);
  if (stateStr) {
    const state = JSON.parse(stateStr);
    localStorage.setItem(
      MASA_BLAZOR_SSR_STATE,
      JSON.stringify({
        ...state,
        ...obj,
      })
    );
  }
}

export function restoreMain(application: MasaBlazorApplication) {
  const main = document.querySelector(".m-main") as HTMLElement;
  if (main && application) {
    main.style.paddingTop = application.top + application.bar + "px";
    main.style.paddingRight = application.right + "px";
    main.style.paddingBottom =
      application.bottom + application.insetFooter + application.bottom + "px";
    main.style.paddingLeft = application.left + "px";
  }
}

export function getState(): MasaBlazorSsrState {
  const stateStr = localStorage.getItem(MASA_BLAZOR_SSR_STATE);
  if (stateStr) {
    return JSON.parse(stateStr);
  }
  return null;
}
