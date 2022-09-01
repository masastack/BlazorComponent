import { parseChangeEvent } from "./events/EventType";

function registerInputEvents(
  element: Element,
  onInput: DotNet.DotNetObject,
  onDebounceInput: DotNet.DotNetObject,
  debounce: number
) {
  if (!(element && element instanceof HTMLInputElement)) return;

  let compositionInputting = false;
  let listener;

  if (debounce > 0) {
    let timeout;
    listener = function (args: any) {
      if (compositionInputting) return;

      var changeEventArgs = parseChangeEvent(args);

      onInput && onInput.invokeMethodAsync("Invoke", changeEventArgs);

      clearTimeout(timeout);
      timeout = setTimeout(() => {
        console.log(
          "invoke debounce ~~~",
          args.target.value,
          args.target.validity,
          changeEventArgs.value
        );
        onDebounceInput.invokeMethodAsync("Invoke", changeEventArgs);
      }, debounce);
    };
  } else {
    listener = function (args: any) {
      if (compositionInputting) return;

      var changeEventArgs = parseChangeEvent(args);

      onInput && onInput.invokeMethodAsync("Invoke", changeEventArgs);

      console.log(
        "invoke ~~~",
        args.target.value,
        args.target.validity,
        changeEventArgs.value
      );

      onDebounceInput.invokeMethodAsync("Invoke", changeEventArgs);
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
  console.log("setValue", element, value);
  element.value = value;
}

export { registerInputEvents, setValue };
