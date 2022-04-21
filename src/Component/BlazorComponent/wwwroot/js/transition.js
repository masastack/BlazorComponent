function registerTransitionEvents(element, dotNet) {
    if (typeof element === 'string') {
        element = document.querySelector(element);
    }

    console.log('element', element)

    if (!element) return;

    element.addEventListener("transitionend", (e) => {

        let _bl_ = e.target.getAttributeNames().find(a => a.startsWith('_bl_'))
        if (_bl_) {
            _bl_ = _bl_.substring(4);
        }

        console.log(`_bl_:${_bl_}, dotNet:${Boolean(dotNet)}`)

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

        dotNet.invokeMethodAsync('OnTransitionEnd', _bl_, transition)
    })
}

export {
    registerTransitionEvents,
}