import { getElementSelector } from "../utils/helper";
import { parseMouseEvent, parseTouchEvent } from "./EventType";

export function registerExtraMouseEvent(eventType: string, eventName: string) {
  if (Blazor) {
    Blazor.registerCustomEventType(eventType, {
      browserEventName: eventName,
      createEventArgs: e => sharedCreateEventArgs("mouse", e)
    })
  }
}

export function registerExtraTouchEvent(eventType: string, eventName: string) {
  if (Blazor) {
    Blazor.registerCustomEventType(eventType, {
      browserEventName: eventName,
      createEventArgs: e => sharedCreateEventArgs("touch", e)
    })
  }
}

function sharedCreateEventArgs(type: "mouse" | "touch", e: Event,) {
  let args = { target: {} }
  if (type === 'mouse') {
    args = {
      ...args,
      ...parseMouseEvent(e as MouseEvent)
    }
  } else if (type === 'touch') {
    args = {
      ...args,
      ...parseTouchEvent(e as TouchEvent)
    }
  }

  if (e.target) {
    const target = e.target as HTMLElement;
    const elementReferenceId = target.getAttributeNames().find(a => a.startsWith('_bl_'));
    if (elementReferenceId) {
      args.target['elementReferenceId'] = elementReferenceId
      args.target['selector'] = `[${elementReferenceId}]`
    } else {
      args.target['selector'] = getElementSelector(target)
    }

    args.target['class'] = target.getAttribute('class')
  }

  return args;
}