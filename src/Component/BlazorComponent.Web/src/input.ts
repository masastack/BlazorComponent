import { parseChangeEvent } from "./events/EventType";

function registerInputEvents(
  element: Element,
  onInput: DotNet.DotNetObject,
  debounce: number
) {
  if (
    !(
      element &&
      (element instanceof HTMLInputElement ||
        element instanceof HTMLTextAreaElement)
    )
  )
    return;

  let compositionInputting = false;

  let timeout;

  element.addEventListener("compositionstart", (_) => {
    compositionInputting = true;
  });

  element.addEventListener("compositionend", (event: CompositionEvent) => {
    compositionInputting = false;

    const changeEventArgs = parseChangeEvent(event);
    changeEventArgs.value = element.value;

    if (
      element.maxLength !== -1 &&
      changeEventArgs.value.length > element.maxLength
    ) {
      changeEventArgs.value = changeEventArgs.value.substring(
        0,
        element.maxLength
      );
    }

    onInput.invokeMethodAsync("Invoke", changeEventArgs);
  });

  element.addEventListener("input", (event: InputEvent) => {
    if (compositionInputting) return;

    var changeEventArgs = parseChangeEvent(event);

    clearTimeout(timeout);
    timeout = setTimeout(() => {
      onInput.invokeMethodAsync("Invoke", changeEventArgs);
    }, debounce);
  });
}

function setValue(element: HTMLInputElement, value: any) {
  element.value = value;
}

export { registerInputEvents, setValue };
