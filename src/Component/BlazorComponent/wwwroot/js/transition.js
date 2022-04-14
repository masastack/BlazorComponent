function addTransitionEnd(element, dotNet) {
    if (typeof element === 'string') {
        element = document.querySelector(element);
    }

    if (!element) return;

    element.addEventListener('transitionstart', (e) => {
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

        // console.log(e, e.target, e.target.className)
        // console.log('_bl_', _bl_)
        console.log('start', _bl_, e.elapsedTime, classNames.filter(c => c.includes('transition')))

        if (transition === 'leave') {
            dotNet.invokeMethodAsync('OnTransitionLeave')
        }


        // dotNet.invokeMethodAsync('OnTransitionEnd', _bl_, transition)
    })


    element.addEventListener('transitionrun', (e) => {
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

        // console.log(e, e.target, e.target.className)
        // console.log('_bl_', _bl_)
        console.log('run', _bl_, e.elapsedTime, classNames.filter(c => c.includes('transition')))

        // dotNet.invokeMethodAsync('OnTransitionEnd', _bl_, transition)
    })

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

        // console.log(e, e.target, e.target.className)
        console.log('end', _bl_, e.elapsedTime, classNames.filter(c => c.includes('transition')))


        dotNet.invokeMethodAsync('OnTransitionEnd', _bl_, transition)
    })

    element.addEventListener("transitioncancel", (e) => {

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

        // console.log(e, e.target, e.target.className)

        console.log('cancel', _bl_, e.elapsedTime, classNames.filter(c => c.includes('transition')))

        // dotNet.invokeMethodAsync('OnTransitionCancel')
    })
}

export {
    addTransitionEnd,
}