import { parseChangeEvent } from "./events/default";

function registerInputEvents(
  element: Element,
  dotNet: DotNet.DotNetObject,
  debounce: number
) {
  if (!element) return;

  let compositionInputting = false;
  let listener;

  if (debounce > 0) {
    let timeout;
    listener = function (args: any) {
      if (compositionInputting) return;

      clearTimeout(timeout);
      timeout = setTimeout(() => {
        console.log("invoke debounce ~~~");

        var changeEventArgs = parseChangeEvent(args);
        dotNet.invokeMethodAsync("Invoke", changeEventArgs);
      }, debounce);
    };
  } else {
    listener = function (args: any) {
      if (compositionInputting) return;

      console.log("invoke ~~~");

      var changeEventArgs = parseChangeEvent(args);
      dotNet.invokeMethodAsync("Invoke", changeEventArgs);
    };
  }

  element.addEventListener(
    "compositionstart",
    (_) => (compositionInputting = true)
  );
  element.addEventListener(
    "compositionend",
    (_) => (compositionInputting = false)
  );
  element.addEventListener("input", listener);
}

export { registerInputEvents };
