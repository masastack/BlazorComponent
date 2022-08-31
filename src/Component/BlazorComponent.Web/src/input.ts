import { parseChangeEvent } from "./events/EventType";

function registerInputEvents(
  element: Element,
  dotNet: DotNet.DotNetObject,
  debounce: number
) {
  if (!(element && element instanceof HTMLInputElement)) return;

  let compositionInputting = false;
  let listener;

  if (debounce > 0) {
    let timeout;
    listener = function (args: any) {
      if (compositionInputting) return;

      clearTimeout(timeout);
      timeout = setTimeout(() => {
        var changeEventArgs = parseChangeEvent(args);
        console.log(
          "invoke debounce ~~~",
          args.target.value,
          args.target.validity,
          changeEventArgs.value
        );
        dotNet.invokeMethodAsync("Invoke", changeEventArgs);
      }, debounce);
    };
  } else {
    listener = function (args: any) {
      if (compositionInputting) return;

      console.log(
        "invoke ~~~",
        args.target.value,
        args.target.validity,
        changeEventArgs.value
      );

      var changeEventArgs = parseChangeEvent(args);
      dotNet.invokeMethodAsync("Invoke", changeEventArgs);
    };
  }

  console.log("element", element);

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

function setValue(element: HTMLInputElement, value: any) {
  element.value = value;
}

export { registerInputEvents, setValue };
