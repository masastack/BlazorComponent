type MainPadding = {
  top: number;
  right: number;
  bottom: number;
  left: number;
};

type MasaBlazorSsrState = {
  main: MainPadding;
  rtl: boolean;
  dark: boolean;
};

const MASA_BLAZOR_SSR_STATE = "masablazor@ssr-state";

export function setTheme(dark: boolean) {
  console.log('[index.ts] setTheme', dark)
  const app = getApp();
  if (app) {
    app.classList.replace(getThemeCss(!dark), getThemeCss(dark));
  }

  updateStorage({ dark });
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

export function setRtl(rtl: boolean) {
  console.log('[index.ts] setRtl', rtl)
  const app = getApp();
  if (!app) return;

  const rtlCss = "m-application--is-rtl";
  if (!rtl) {
    app.classList.remove(rtlCss);
  } else if (!app.classList.contains(rtlCss)) {
    app.classList.add(rtlCss);
  }

  updateStorage({ rtl });
}

export function updateMain(value: MainPadding) {
  const main: HTMLElement = document.querySelector(".m-main");

  const padding: MainPadding = {
    top: value.top ?? getPadding("Top"),
    right: value.right ?? getPadding("Right"),
    bottom: value.bottom ?? getPadding("Bottom"),
    left: value.left ?? getPadding("Left"),
  };

  main.style.paddingTop = `${padding.top}px`;
  main.style.paddingRight = `${padding.right}px`;
  main.style.paddingBottom = `${padding.bottom}px`;
  main.style.paddingLeft = `${padding.left}px`;

  function getPadding(prop: "Top" | "Right" | "Bottom" | "Left") {
    return Number(main.style[`padding${prop}`].match(/\d+/)[0]);
  }
}

export function saveMain(state: MasaBlazorSsrState) {
  console.log('saveMain', state)
  localStorage.setItem(MASA_BLAZOR_SSR_STATE, JSON.stringify(state));
}

function getThemeCss(dark: boolean) {
  return dark ? "theme--dark" : "theme--light";
}

function getApp() {
  return document.querySelector(".m-application");
}
