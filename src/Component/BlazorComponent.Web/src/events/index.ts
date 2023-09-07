import { registerExtraDropEvent, registerExtraMouseEvent, registerExtraTouchEvent } from "./extra";

export function registerExtraEvents() {
    registerExtraMouseEvent("exmousedown", "mousedown");
    registerExtraMouseEvent("exmouseup", "mouseup");
    registerExtraMouseEvent("exclick", "click");
    registerExtraMouseEvent("exmouseleave", "mouseleave");
    registerExtraMouseEvent("exmouseenter", "mouseenter");
    registerExtraMouseEvent("exmousemove", "mousemove");
    registerExtraTouchEvent("extouchstart", "touchstart");
    registerEvent("transitionend", "transitionend")
    registerExtraDropEvent("exdrop", "drop");
}

function registerEvent(eventType: string, eventName: string) {
    if (Blazor) {
        Blazor.registerCustomEventType(eventType, {
            browserEventName: eventName,
        })
    }
}