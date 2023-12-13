import { getThemeCss, MASA_BLAZOR_SSR_STATE, MasaBlazorSsrState, restoreMain, setRtl } from "./";

// Called when the script first gets loaded on the page.
export function onLoad() {}

// Called when an enhanced page update occurs, plus once immediately after
// the initial load.
export function onUpdate() {
  const stateStr = localStorage.getItem(MASA_BLAZOR_SSR_STATE);
  if (!stateStr) return;

  const state: MasaBlazorSsrState = JSON.parse(stateStr);
  if (!state) return;

  // restoreMain(state.passive.application);
  restoreTheme(state);
  restoreRtl(state);
}

// Called when an enhanced page update removes the script from the page.
export function onDispose() {}

export function restoreTheme(state: MasaBlazorSsrState) {
  console.log('restoreTheme', state.dark)
  if (typeof state.dark === "boolean") {
    const selector = `.${getThemeCss(!state.dark)}:not(.theme--independent)`;
    const elements = document.querySelectorAll(selector);
    for (let i = 0; i < elements.length; i++) {
      elements[i].classList.remove(getThemeCss(!state.dark));
      elements[i].classList.add(getThemeCss(state.dark));
    }
  }
}

export function restoreRtl(state: MasaBlazorSsrState) {
  if (typeof state.rtl === "boolean") {
    setRtl(state.rtl, false);
  }
}
