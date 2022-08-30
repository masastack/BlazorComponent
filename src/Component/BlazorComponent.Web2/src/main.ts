import * as interop from "./interop";

declare global {
  interface Window {
    BlazorComponent: any;
  }
}

window.BlazorComponent = {
  interop: {
    ...interop,
  },
};
