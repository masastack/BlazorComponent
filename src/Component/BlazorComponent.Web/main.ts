import * as interop from "./src/interop";
import * as overlayable from "./src/mixins/overlayable/index";

declare global {
    interface Window {
        BlazorComponent: any;
    }
}

window.BlazorComponent = {
    interop: { ...interop, ...overlayable }
};
