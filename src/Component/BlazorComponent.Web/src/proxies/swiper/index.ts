import SwiperClass from "swiper";
import { SwiperOptions } from "swiper/types/swiper-options";

declare const Swiper: SwiperClass;

class SwiperProxy {
  swiper: SwiperClass;
  dotnetHelper: DotNet.DotNetObject;
  isDisposed: boolean;

  constructor(
    el: HTMLElement,
    swiperOptions: SwiperOptions,
    dotnetHelper: DotNet.DotNetObject
  ) {
    this.dotnetHelper = dotnetHelper;

    swiperOptions ??= {};

    if (swiperOptions.pagination) {
      swiperOptions.pagination["type"] =
        swiperOptions.pagination["type"].toLowerCase();
    }

    this.swiper = new (Swiper as any)(el, swiperOptions);

    this.swiper.on("realIndexChange", this.onRealIndexChange);
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
      this.isDisposed = true;
      this.swiper.off("realIndexChange", this.onRealIndexChange);
      this.swiper.destroy(true);
      this.dotnetHelper["dispose"]();
    }
  }

  async onRealIndexChange(e: SwiperClass) {
    if (this.dotnetHelper) {
      await this.dotnetHelper.invokeMethodAsync("OnIndexChanged", e.realIndex);
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
