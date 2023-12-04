import { MASA_BLAZOR_SSR_STATE } from "./";
import { restoreRtl, restoreTheme } from "./page-script";

const stateStr = localStorage.getItem(MASA_BLAZOR_SSR_STATE);
if (stateStr) {
  const state = JSON.parse(stateStr);

  restoreTheme(state);
  restoreRtl(state);
}
