import { registerExtraMouseEvent, registerExtraTouchEvent } from "./extra";

export function registerExtraEvents() {
    registerExtraMouseEvent("exmousedown", "mousedown");
    registerExtraMouseEvent("exmouseup", "mouseup");
    registerExtraMouseEvent("exclick", "click");
    registerExtraMouseEvent("exmouseleave", "mouseleave");
    registerExtraMouseEvent("exmouseenter", "mouseenter");
    registerExtraMouseEvent("exmousemove", "mousemove");
    registerExtraTouchEvent("extouchstart", "touchstart");
}