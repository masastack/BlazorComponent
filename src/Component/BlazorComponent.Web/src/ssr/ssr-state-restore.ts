import { getApp, MASA_BLAZOR_SSR_STATE, MasaBlazorSsrState } from "./";
import { restoreRtl, restoreTheme } from "./page-script";

const stateStr = localStorage.getItem(MASA_BLAZOR_SSR_STATE);
if (stateStr) {
  const state = JSON.parse(stateStr);

  restoreTheme(state);
  restoreRtl(state);
} else {
  var application = getApp();
  const isAppDark = application.classList.contains("theme--dark");
  const isAppRtl = application.classList.contains("m-application--is-rtl");
  const stateStr: MasaBlazorSsrState = {
    dark: isAppDark,
    rtl: isAppRtl,
  };

  localStorage.setItem(MASA_BLAZOR_SSR_STATE, JSON.stringify(stateStr));
}
