import SwiperClass from "swiper";
import { SwiperOptions } from "swiper/types/swiper-options";

declare const Swiper: SwiperClass;

class SwiperProxy {
  swiper: SwiperClass;
  dotnetHelper: DotNet.DotNetObject;

  constructor(
    el: HTMLElement,
    swiperOptions: SwiperOptions,
    dotnetHelper: DotNet.DotNetObject
  ) {
    if (!el) {
      dotnetHelper && dotnetHelper['dispose'] && dotnetHelper['dispose']();
      return
    }

    if (el._swiper) {
      el._swiper.instance.destroy(true);
      delete el["_swiper"];
    }

    this.dotnetHelper = dotnetHelper;

    swiperOptions ??= {};

    if (swiperOptions.pagination) {
      swiperOptions.pagination["type"] =
        swiperOptions.pagination["type"].toLowerCase();
    }

    this.swiper = new (Swiper as any)(el, swiperOptions);
    this.swiper.on("realIndexChange", (e) => this.onRealIndexChange(e, this));

    el._swiper = {
      instance: this.swiper,
      handle: dotnetHelper,
    };
  }

  slideTo(index: number, speed?: number, runCallbacks?: boolean) {
    this.swiper.slideToLoop(index, speed, runCallbacks);
  }

  slideNext(speed?: number) {
    this.swiper.slideNext(speed);
  }

  slidePrev(speed?: number) {
    this.swiper.slidePrev(speed);
  }

  dispose() {
    if (this.dotnetHelper["dispose"]) {
      this.swiper && this.swiper.destroy(true);
      this.dotnetHelper["dispose"]();
    }
  }

  async onRealIndexChange(e: SwiperClass, that: SwiperProxy) {
    if (that.dotnetHelper) {
      await that.dotnetHelper.invokeMethodAsync("OnIndexChanged", e.realIndex);
    }
  }
}

function init(
  el: HTMLElement,
  swiperOptions: SwiperOptions,
  dotnetHelper: DotNet.DotNetObject
) {
  return new SwiperProxy(el, swiperOptions, dotnetHelper);
}

export { init };
