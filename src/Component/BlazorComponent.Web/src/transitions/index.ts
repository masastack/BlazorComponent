class Transition {
  name: string;
  el: HTMLElement;
  enterClass: string;
  enterActiveClass: string;
  enterToClass: string;
  leaveClass: string;
  leaveActiveClass: string;
  leaveToClass: string;

  constructor(name: string, elOrSelector: HTMLElement | string) {
    this.name = name;
    this.enterClass = `${this.name}-enter`;
    this.enterActiveClass = `${this.name}-enter-active`;
    this.enterToClass = `${this.name}-enter-to`;
    this.leaveClass = `${this.name}-leave`;
    this.leaveActiveClass = `${this.name}-leave-active`;
    this.leaveToClass = `${this.name}-leave-to`;

    if (typeof elOrSelector === "string") {
      this.el = document.querySelector(elOrSelector);
    } else {
      this.el = elOrSelector;
    }
  }

  onEnter() {
    this.addTransitionClass(this.el, this.enterClass);
    this.addTransitionClass(this.el, this.enterActiveClass);

    const resolve = () => {
      this.removeTransitionClass(this.el, this.enterToClass);
      this.removeTransitionClass(this.el, this.enterActiveClass);
    };

    requestAnimationFrame(() => {
      requestAnimationFrame(() => {
        this.removeTransitionClass(this.el, this.enterClass);
        this.addTransitionClass(this.el, this.enterToClass);

        this.el.addEventListener("transitionend", resolve);
      });
    });
  }

  onLeave() {
    const resolve = () => {
      this.removeTransitionClass(this.el, this.leaveToClass);
      this.removeTransitionClass(this.el, this.leaveActiveClass);
    };

    this.addTransitionClass(this.el, this.leaveClass);
    this.addTransitionClass(this.el, this.leaveActiveClass);

    requestAnimationFrame(() => {
      requestAnimationFrame(() => {
        this.removeTransitionClass(this.el, this.leaveClass);
        this.addTransitionClass(this.el, this.leaveToClass);

        this.el.addEventListener("transitionend", resolve);
      });
    });
  }

  addTransitionClass(el: HTMLElement, cls: string) {
    cls.split(/\s+/).forEach((c) => c && el.classList.add(c));
  }

  removeTransitionClass(el: HTMLElement, cls: string) {
    cls.split(/\s+/).forEach((c) => c && el.classList.remove(c));
  }
}

function init(name: string, elOrSelector: HTMLElement | string) {
  const transitionEl = new Transition(name, elOrSelector);
  return transitionEl;
}

export { init };
