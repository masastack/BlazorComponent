import { rippleHide, rippleShow } from "./ripple";

export default function registerDirective() {
  var observer = new MutationObserver(function (mutationsList) {
    for (let mutation of mutationsList) {
      if (mutation.type === "childList") {
        var target: any = mutation.target;
        //ripple
        if (!target._bind && target.attributes["ripple"]) {
          target.addEventListener("mousedown", rippleShow);
          target.addEventListener("mouseup", rippleHideWrapper);
          target.addEventListener("mouseleave", rippleHideWrapper);

          target._bind = true;
        } else if (target.nodeName == "BODY") {
          var rippleEls = document.querySelectorAll("[ripple]");
          for (let rippleEl of rippleEls) {
            var el: any = rippleEl;
            if (!el._bind) {
              el.addEventListener("mousedown", rippleShow);
              el.addEventListener("mouseup", (e) => rippleHide(e));
              el.addEventListener("mouseleave", (e) => rippleHide(e));

              el._bind = true;
            }
          }
        }
      } else if (mutation.type === "attributes") {
        //ripple
        if (mutation.attributeName == "ripple") {
          var target: any = mutation.target;
          if (target.attributes["ripple"]) {
            target.addEventListener("mousedown", rippleShow);
            target.addEventListener("mouseup", (e) => rippleHide(e));
            target.addEventListener("mouseleave", (e) => rippleHide(e));
          } else {
            target.removeEventListener("mousedown", rippleShow);
            target.removeEventListener("mouseup", (e) => rippleHide(e));
            target.removeEventListener("mouseleave", (e) => rippleHide(e));
          }
        }
      }
    }
  });

  observer.observe(document, {
    attributes: true,
    subtree: true,
    childList: true,
    attributeFilter: ["ripple"],
  });
}
