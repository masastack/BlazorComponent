import { rippleShow, rippleHide } from "./ripple";

export default function registerDirective() {
    var observer = new MutationObserver(function (mutationsList) {
        for (let mutation of mutationsList) {
            if (mutation.type === 'childList') {
                var target: any = mutation.target;
                //ripple
                if (!target._bind && target.attributes['ripple']) {

                    target.addEventListener('mousedown', rippleShow)
                    target.addEventListener('mouseup', rippleHide)
                    target.addEventListener('mouseleave', rippleHide)

                    target._bind = true;
                }
                else if (target.nodeName == 'BODY') {
                    var rippleEls = document.querySelectorAll('[ripple]');
                    for (let rippleEl of rippleEls) {
                        var el: any = rippleEl;
                        if (!el._bind) {
                            el.addEventListener('mousedown', rippleShow)
                            el.addEventListener('mouseup', rippleHide)
                            el.addEventListener('mouseleave', rippleHide)

                            el._bind = true;
                        }
                    }
                }
            }
            else if (mutation.type === 'attributes') {
                //ripple
                if (mutation.attributeName == 'ripple') {
                    var target: any = mutation.target;
                    if (target.attributes['ripple']) {
                        target.addEventListener('mousedown', rippleShow)
                        target.addEventListener('mouseup', rippleHide)
                        target.addEventListener('mouseleave', rippleHide)
                    }
                    else {
                        target.removeEventListener('mousedown', rippleShow)
                        target.removeEventListener('mouseup', rippleHide)
                        target.removeEventListener('mouseleave', rippleHide)
                    }
                }
            }
        }
    });

    observer.observe(document, { attributes: true, subtree: true, childList: true, attributeFilter: ['ripple'] });
}