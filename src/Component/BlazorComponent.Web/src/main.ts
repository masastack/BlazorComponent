import * as slider from "./components/slider";
import * as interop from "./interop";
import * as overlayable from "./mixins/overlayable";
import * as ssr from "./ssr";

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
    ssr
  },
};

window.MasaBlazor = {};
