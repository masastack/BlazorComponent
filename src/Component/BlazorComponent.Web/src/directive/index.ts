import { keyCodes } from "utils/helper";

import { rippleHide, ripples, rippleShow } from "./ripple";

interface RippleOptions {
  class?: string;
  center?: boolean;
  circle?: boolean;
}

function isRippleEnabled(value: any): value is true {
  return typeof value === "undefined" || !!value;
}

function rippleCancelShow(e: MouseEvent | TouchEvent) {
  const element = e.currentTarget as HTMLElement | undefined;

  if (!element || !element._ripple) return;

  if (element._ripple.showTimerCommit) {
    element._ripple.showTimerCommit = null;
  }

  window.clearTimeout(element._ripple.showTimer);
}

class Ripple {
  el: HTMLElement;
  options: RippleOptions;
  keyboardRipple: boolean;

  constructor(el: HTMLElement, options?: RippleOptions) {
    this.el = el;
    this.updateRipple(options, false);
  }

  updateRipple(options: RippleOptions, wasEnabled: boolean) {
    const enabled = isRippleEnabled(options);
    if (!enabled) {
      ripples.hide(this.el);
    }

    this.el._ripple = this.el._ripple || {};
    this.el._ripple.enabled = enabled;
    const value = options || {};
    if (value.center) {
      this.el._ripple.centered = true;
    }
    if (value.class) {
      this.el._ripple.class = options.class;
    }
    if (value.circle) {
      this.el._ripple.circle = value.circle;
    }
    if (enabled && !wasEnabled) {
      this.el.addEventListener("touchstart", rippleShow, { passive: true });
      this.el.addEventListener("touchend", rippleHide, { passive: true });
      this.el.addEventListener("touchmove", rippleCancelShow, {
        passive: true,
      });
      this.el.addEventListener("touchcancel", rippleHide);

      this.el.addEventListener("mousedown", rippleShow);
      this.el.addEventListener("mouseup", rippleHide);
      this.el.addEventListener("mouseleave", rippleHide);

      this.el.addEventListener("keydown", this.keyboardRippleShow);
      this.el.addEventListener("keyup", this.keyboardRippleHide);

      this.el.addEventListener("blur", this.focusRippleHide);

      // Anchor tags can be dragged, causes other hides to fail - #1537
      this.el.addEventListener("dragstart", rippleHide, { passive: true });
    } else if (!enabled && wasEnabled) {
      this.removeListeners();
    }
  }

  removeListeners() {
    this.el.removeEventListener("mousedown", rippleShow);
    this.el.removeEventListener("touchstart", rippleShow);
    this.el.removeEventListener("touchend", rippleHide);
    this.el.removeEventListener("touchmove", rippleCancelShow);
    this.el.removeEventListener("touchcancel", rippleHide);
    this.el.removeEventListener("mouseup", rippleHide);
    this.el.removeEventListener("mouseleave", rippleHide);
    this.el.removeEventListener("keydown", this.keyboardRippleShow);
    this.el.removeEventListener("keyup", this.keyboardRippleHide);
    this.el.removeEventListener("dragstart", rippleHide);
    this.el.removeEventListener("blur", this.focusRippleHide);
  }

  keyboardRippleShow(e: KeyboardEvent) {
    if (
      !this.keyboardRipple &&
      (e.keyCode === keyCodes.enter || e.keyCode === keyCodes.space)
    ) {
      this.keyboardRipple = true;
      rippleShow(e);
    }
  }

  keyboardRippleHide(e: KeyboardEvent) {
    this.keyboardRipple = false;
    rippleHide(e);
  }

  focusRippleHide(e: FocusEvent) {
    if (this.keyboardRipple === true) {
      this.keyboardRipple = false;
      rippleHide(e);
    }
  }

  update(options: RippleOptions) {
    if (options == this.options) {
      return;
    }

    const wasEnabled = isRippleEnabled(this.options);
    this.updateRipple(options, wasEnabled);
  }

  unbind() {
    delete this.el._ripple;
    this.removeListeners();
  }
}

export function init(el: HTMLElement, options: RippleOptions) {
  return new Ripple(el, options);
}

// export default function registerDirective() {
//   var observer = new MutationObserver(function (mutationsList) {
//     for (let mutation of mutationsList) {
//       if (mutation.type === "childList") {
//         var target: any = mutation.target;
//         //ripple
//         if (!target._bind && target.attributes && target.attributes["ripple"]) {
//           target.addEventListener("mousedown", rippleShow);
//           target.addEventListener("mouseup", rippleHide);
//           target.addEventListener("mouseleave", rippleHide);

//           target._bind = true;
//         } else if (target.nodeName == "BODY") {
//           var rippleEls = document.querySelectorAll("[ripple]");
//           for (let rippleEl of rippleEls) {
//             var el: any = rippleEl;
//             if (!el._bind) {
//               el.addEventListener("mousedown", rippleShow);
//               el.addEventListener("mouseup", rippleHide);
//               el.addEventListener("mouseleave", rippleHide);

//               el._bind = true;
//             }
//           }
//         }
//       } else if (mutation.type === "attributes") {
//         //ripple
//         if (mutation.attributeName == "ripple") {
//           var target: any = mutation.target;
//           if (target.attributes["ripple"]) {
//             target.addEventListener("mousedown", rippleShow);
//             target.addEventListener("mouseup", rippleHide);
//             target.addEventListener("mouseleave", rippleHide);
//           } else {
//             target.removeEventListener("mousedown", rippleShow);
//             target.removeEventListener("mouseup", rippleHide);
//             target.removeEventListener("mouseleave", rippleHide);
//           }
//         }
//       }
//     }
//   });

//   observer.observe(document, {
//     attributes: true,
//     subtree: true,
//     childList: true,
//     attributeFilter: ["ripple"],
//   });
// }
