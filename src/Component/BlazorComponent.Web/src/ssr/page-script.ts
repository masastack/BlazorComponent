import { MASA_BLAZOR_SSR_STATE, MasaBlazorApplication, MasaBlazorSsrState, restoreMain } from "./";

let firstUpdate = true;

// Called when the script first gets loaded on the page.
export function onLoad() {}

// Called when an enhanced page update occurs, plus once immediately after
// the initial load.
export function onUpdate() {
  if (firstUpdate) {
    firstUpdate = false;
    return;
  }

  console.log("Update");

  const stateStr = localStorage.getItem(MASA_BLAZOR_SSR_STATE);
  console.log("[Update] stateStr", stateStr);
  if (!stateStr) return;

  const state: MasaBlazorSsrState = JSON.parse(stateStr);
  if (!state) return;

  console.log("[Update] state", state);

  restoreMain(state.passive.application);
  restoreTheme(state);
  restoreRtl(state);
}

// Called when an enhanced page update removes the script from the page.
export function onDispose() {
  console.log("Dispose");
}

function restoreTheme(state: MasaBlazorSsrState) {
  console.log("restoreTheme", state);
  if (typeof state.dark === "boolean") {
    window.BlazorComponent.interop.ssr.setTheme(state.dark);
  }
}

function restoreRtl(state: MasaBlazorSsrState) {
  console.log("restoreRtl", state);
  if (typeof state.rtl === "boolean") {
    window.BlazorComponent.interop.ssr.setRtl(state.rtl);
  }
}
