function addTransitionEnd(element, dotNet) {
    if (typeof element === 'string') {
        element = document.querySelector(element);
    }

    if (!element) return;

    element.addEventListener("transitionend", (e) => {
        // if (!['transform', 'opacity'].includes(e.propertyName))
        //     return;


        let _bl_ = e.target.getAttributeNames().find(a => a.startsWith('_bl_'))
        if (_bl_) {
            _bl_ = _bl_.substring(4);
        }

        const classNames = e.target.className.split(' ');
        let transition;
        const isLeave = classNames.some(c => c.includes('transition-leave'));
        if (isLeave) {
            transition = 'leave';
        } else {
            const isEnter = classNames.some(c => c.includes('transition-enter'));
            if (isEnter) {
                transition = 'enter';
            }
        }

        if (!transition) return;

        console.log(e, e.target, e.target.className)
        console.log('_bl_', _bl_)

        dotNet.invokeMethodAsync('OnTransitionEnd', _bl_, transition)
    })

    element.addEventListener("transitioncancel", (e) => {

        const classNames = e.target.className.split(' ');
        let transition;
        const isLeave = classNames.some(c => c.includes('transition-leave'));
        if (isLeave) {
            transition = 'leave';
        } else {
            const isEnter = classNames.some(c => c.includes('transition-enter'));
            if (isEnter) {
                transition = 'enter';
            }
        }

        if (!transition) return;

        // console.log(e, e.target, e.target.className)

        console.log('cancel!', e)

        // dotNet.invokeMethodAsync('OnTransitionCancel')
    })
}

export {
    addTransitionEnd,
}