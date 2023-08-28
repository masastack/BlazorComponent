// import Swiper from "swiper/types/swiper-class";
// import Swiper from "swiper";
import { SwiperOptions } from "swiper/types/swiper-options";

declare const Swiper;

// class Swiper {
//     (el: HTMLElement, swiperOptions: SwiperOptions): boolean;
// }
// class Swiper {
//     constructor(el: HTMLElement, swiperOptions: SwiperOptions) {
//     }
// }

class SwiperProxy {
    constructor(el: HTMLElement, swiperOptions: SwiperOptions = {}) {

        // const swiperOptions: SwiperOptions = {}

        // for (const key of Object.keys(modules)) {
        //     if (key === "pagination") {
        //         swiperOptions.pagination = modules[key]
        //     } else if (key === "navigation") {
        //         swiperOptions.navigation = modules[key]
        //     }
        // }

        console.log('el', el)
        console.log('swiperOptions', swiperOptions)
        Object.keys(swiperOptions).forEach(k => {
            
        })

        const swiper = new Swiper(el, swiperOptions);
    }
}

function init(el: HTMLElement, swiperOptions: SwiperOptions) {
    return new SwiperProxy(el, swiperOptions);
}

export {
    init
}