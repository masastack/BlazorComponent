import { parseChangeEvent } from "./events/EventType";
import { createSharedEventArgs } from "./events/extra";

function registerInputEvents(
  inputElement: Element,
  inputSlotElement: Element,
  dotNetHelper: DotNet.DotNetObject,
  debounce: number
) {
  if (!inputElement || !inputSlotElement) return;

  registerClickEvent(inputSlotElement, dotNetHelper);

  registerMouseUpEvent(inputSlotElement, dotNetHelper);

  if (
    !(
      inputElement &&
      (inputElement instanceof HTMLInputElement ||
        inputElement instanceof HTMLTextAreaElement)
    )
  )
    return;

  registerInputEvent(inputElement, dotNetHelper, debounce);
}

function registerInputEvent(
  inputElement: HTMLInputElement | HTMLTextAreaElement,
  dotNetHelper: DotNet.DotNetObject,
  debounce: number
) {
  let compositionInputting = false;

  let timeout;

  inputElement.addEventListener("compositionstart", (_) => {
    compositionInputting = true;
  });

  inputElement.addEventListener("compositionend", (event: CompositionEvent) => {
    compositionInputting = false;

    const changeEventArgs = parseChangeEvent(event);
    changeEventArgs.value = inputElement.value;

    if (
      inputElement.maxLength !== -1 &&
      changeEventArgs.value.length > inputElement.maxLength
    ) {
      changeEventArgs.value = changeEventArgs.value.substring(
        0,
        inputElement.maxLength
      );
    }

    dotNetHelper.invokeMethodAsync("OnInput", changeEventArgs);
  });

  inputElement.addEventListener("input", (event: InputEvent) => {
    if (compositionInputting) return;

    var changeEventArgs = parseChangeEvent(event);

    clearTimeout(timeout);
    timeout = setTimeout(() => {
      dotNetHelper.invokeMethodAsync("OnInput", changeEventArgs);
    }, debounce);
  });
}

function setValue(element: HTMLInputElement, value: any) {
  if (!element) return;
  element.value = value;
}

function registerClickEvent(
  inputSlot: Element,
  dotNetHelper: DotNet.DotNetObject
) {
  inputSlot.addEventListener("click", (e) => {
    if (checkIfStopPropagation(e, "click")) {
      return;
    }

    var eventArgs = createSharedEventArgs("mouse", e);
    dotNetHelper.invokeMethodAsync("OnClick", eventArgs);
  });
}

function registerMouseUpEvent(
  inputSlot: Element,
  dotNetHelper: DotNet.DotNetObject
) {
  inputSlot.addEventListener("mouseup", (e) => {
    if (checkIfStopPropagation(e, "mouseup")) {
      return;
    }

    var eventArgs = createSharedEventArgs("mouse", e);
    dotNetHelper.invokeMethodAsync("OnMouseUp", eventArgs);
  });
}

// @event:stopPropagation only works in Blazor,
// so need to capture it manually.
function checkIfStopPropagation(event: Event, eventName: string) {
  const z = ["_blazorEvents_1", "stopPropagationFlags", eventName];
  let isFlag = event.composedPath()[0];
  let i = 0;
  while (isFlag[z[i]]) {
    isFlag = isFlag[z[i]];
    i++;
  }

  return i == z.length && typeof isFlag === "boolean" && isFlag;
}

export { registerInputEvents, setValue };
