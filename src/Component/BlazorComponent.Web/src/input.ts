import { parseChangeEvent } from "./events/EventType";

function registerInputEvents(
  element: Element,
  onInput: DotNet.DotNetObject,
  debounce: number
) {
  if (!(element && element instanceof HTMLInputElement)) return;

  let compositionInputting = false;

  let timeout;
  const listener = function (args: any) {
    if (compositionInputting) return;

    var changeEventArgs = parseChangeEvent(args);

    clearTimeout(timeout);
    timeout = setTimeout(() => {
      console.log(
        "invoke debounce ~~~",
        args.target.value,
        args.target.validity,
        changeEventArgs.value
      );
      onInput.invokeMethodAsync("Invoke", changeEventArgs);
    }, debounce);
  };

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
