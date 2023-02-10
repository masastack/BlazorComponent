import { ECharts, EChartsResizeOption } from "echarts";

class EChartsProxy {
  instance: ECharts;
  observer: IntersectionObserver;

  constructor(
    elOrString: HTMLDivElement | HTMLCanvasElement,
    theme?: string,
    initOptions?: any
  ) {
    this.instance = echarts.init(elOrString, theme, initOptions);

    window.addEventListener("resize", () => {
      this.instance.resize();
    });

    this.observer = new IntersectionObserver((entries) => {
      if (entries.some((e) => e.isIntersecting)) {
        this.instance.resize();
      }
    });

    this.observer.observe(this.instance.getDom());
  }

  setOption(
    option: any,
    notMerge: boolean = false,
    lazyUpdate: boolean = false
  ) {
    this.instance.setOption(option, notMerge, lazyUpdate);
  }

  resize(opts?: EChartsResizeOption) {
    this.instance.resize(opts);
  }

  dispose() {
    if (this.instance.isDisposed()) return;

    this.observer.disconnect();

    this.instance.dispose();
  }
}

function init(elOrString, theme, initOptions) {
  return new EChartsProxy(elOrString, theme, initOptions);
}

export { init };
