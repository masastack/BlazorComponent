import { ECharts, EChartsResizeOption } from "echarts";

class EChartsProxy {
  instance: ECharts;
  observer: IntersectionObserver;
  dotNetHelper: DotNet.DotNetObject;

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

  setDotNetObjectReference(
    dotNetHelper: DotNet.DotNetObject,
    events: string[]
  ) {
    this.dotNetHelper = dotNetHelper;

    events.forEach((e) => this.#registerEvent(e));
  }

  getOriginInstance() {
    return this.instance;
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

  #registerEvent(eventName: string) {
    this.instance.on(eventName, (params: any) => {
      const {
        componentType,
        seriesType,
        seriesIndex,
        seriesName,
        name,
        dataIndex,
        data,
        dataType,
        value,
        color,
      } = params;

      this.dotNetHelper.invokeMethodAsync(
        "OnEvent",
        eventName,
        eventName === "globalout"
          ? null
          : {
              componentType,
              seriesType,
              seriesIndex,
              seriesName,
              name,
              dataIndex,
              data,
              dataType,
              value: Array.isArray(value) ? value : [value],
              color,
            }
      );
    });
  }
}

function init(elOrString, theme, initOptions) {
  try {
    return new EChartsProxy(elOrString, theme, initOptions);
  } catch (error) {
    console.error(error.message)
  }
}

export { init };
