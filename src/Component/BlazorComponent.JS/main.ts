import * as interop from "./src/interop";

declare global {
    interface Window {
        BlazorComponent: any;
    }
}

window.BlazorComponent = {
    interop,
};
