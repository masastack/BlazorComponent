import * as slider from "./components/slider";
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
    ...slider,
  },
};

window.MasaBlazor = {};
