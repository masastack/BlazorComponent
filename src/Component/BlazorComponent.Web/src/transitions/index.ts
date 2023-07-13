class Transition {
  dotnetHelper: DotNet.DotNetObject;
  name: string;
  el: HTMLElement;
  leaveAbsolute: boolean;

  enterClass: string;
  enterActiveClass: string;
  enterToClass: string;
  leaveClass: string;
  leaveActiveClass: string;
  leaveToClass: string;

  constructor(
    dotnetHelper: DotNet.DotNetObject,
    name: string,
    elOrSelector: HTMLElement | string,
    leaveAbsolute: boolean
  ) {
    this.dotnetHelper = dotnetHelper;
    this.name = name;
    this.leaveAbsolute = leaveAbsolute;

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

  onEnter(newEl: HTMLElement) {
    if (newEl) {
      this.el = newEl;
    }

    console.log("this.el", this.el);

    this.addTransitionClass(this.el, this.enterClass);
    this.addTransitionClass(this.el, this.enterActiveClass);

    console.log("add enter from class", this.el.className);

    const resolve = () => {
      this.removeTransitionClass(this.el, this.enterToClass);
      this.removeTransitionClass(this.el, this.enterActiveClass);

      console.log("remove enter to class", this.el.className);
    };

    requestAnimationFrame(() => {
      requestAnimationFrame(() => {
        this.removeTransitionClass(this.el, this.enterClass);
        this.addTransitionClass(this.el, this.enterToClass);

        console.log("add enter to class", this.el.className);

        this.el.addEventListener("transitionend", resolve);
      });
    });
  }

  delay(cb, ms) {
    return new Promise((resolve) => {
      setTimeout(() => resolve(cb), ms);
    });
  }

  onEnterTo(newEl: HTMLElement) {
    if (newEl) {
      this.el = newEl;
    }

    const resolve = () => {
      this.removeTransitionClass(this.el, this.enterToClass);
      this.removeTransitionClass(this.el, this.enterActiveClass);

      console.log("remove enter to class", this.el.className);
    };

    requestAnimationFrame(() => {
      requestAnimationFrame(() => {
        console.log("this.el", this.el, this.el.className, this.el.classList);
        this.removeTransitionClass(this.el, this.enterClass);
        this.addTransitionClass(this.el, this.enterToClass);

        console.log("add enter to class", this.el.className);

        this.el.addEventListener("transitionend", resolve);
      });
    });
  }

  onLeave() {
    console.log("this.leaveAbsolute", this.leaveAbsolute);
    if (this.leaveAbsolute) {
      const { offsetTop, offsetLeft, offsetWidth, offsetHeight } = this.el;

      this.el["_transitionInitialStyles"] = {
        position: this.el.style.position,
        top: this.el.style.top,
        left: this.el.style.left,
        width: this.el.style.width,
        height: this.el.style.height,
      };

      console.log("this.el.offsetLeft", this.el.offsetLeft);

      this.el.style.position = "absolute";
      this.el.style.top = offsetTop + "px";
      this.el.style.left = offsetLeft + "px";
      this.el.style.width = offsetWidth + "px";
      this.el.style.height = offsetHeight + "px";

      console.log("this.el.style.left", this.el.style.left);
    }

    this.addTransitionClass(this.el, this.leaveClass);
    this.addTransitionClass(this.el, this.leaveActiveClass);

    console.log("this.el.className", this.el.className);

    const resolve = async () => {
      console.log("OnTransitionEnd.....");
      // It is called here rather than after method removeTransitionClass
      // because the js interop call is affected by the network latency.
      await this.dotnetHelper.invokeMethodAsync("OnTransitionEnd");

      console.log('before removeTransitionClass ->', this.el.className)

      this.removeTransitionClass(this.el, this.leaveToClass);
      this.removeTransitionClass(this.el, this.leaveActiveClass);

      console.log('removeTransitionClass this.el.className ->', this.el.className)

      if (this.el && this.el["_transitionInitialStyles"]) {
        const { position, top, left, width, height } = this.el["_transitionInitialStyles"];
        delete this.el["_transitionInitialStyles"];
        this.el.style.position = position || "";
        this.el.style.top = top || "";
        this.el.style.left = left || "";
        this.el.style.width = width || "";
        this.el.style.height = height || "";
      }
    };
    requestAnimationFrame(() => {
      requestAnimationFrame(() => {
        console.log("requestAnimationFrame...");
        this.removeTransitionClass(this.el, this.leaveClass);
        this.addTransitionClass(this.el, this.leaveToClass);

        // todo: transitionend once?
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

function init(
  dotnetHelper: DotNet.DotNetObject,
  name: string,
  elOrSelector: HTMLElement | string,
  leaveAbsolute: boolean
) {
  const transitionEl = new Transition(
    dotnetHelper,
    name,
    elOrSelector,
    leaveAbsolute
  );
  return transitionEl;
}

export { init };
