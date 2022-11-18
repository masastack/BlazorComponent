import * as interop from "./interop";
import * as overlayable from "./mixins/overlayable";

declare global {
  interface Window {
    BlazorComponent: any;
    MasaBlazor: any;
  }
}

window.BlazorComponent = {
  interop: {
    ...interop,
    ...overlayable,
  },
};

window.MasaBlazor = {};
