import * as interop from "./Components/Core/JsInterop/interop";

declare global {
    interface Window {
        BlazorComponent: any;
    }
}

window.BlazorComponent = {
    interop,
};
