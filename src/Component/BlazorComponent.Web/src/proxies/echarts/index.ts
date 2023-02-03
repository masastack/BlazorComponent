function init(elOrString, theme, initOptions) {
  const chart = echarts.init(elOrString, theme, initOptions);

  window.addEventListener("resize", () => {
    chart.resize();
  });

  const observer = new IntersectionObserver((entries) => {
    if (entries.some((e) => e.isIntersecting)) {
      chart.resize();
    }
  });

  observer.observe(chart.getDom());

  if (!!chart["origin_dispose"]) {
    chart["origin_dispose"] = chart.dispose;
    chart.dispose = () => {
      if (chart.isDisposed()) return;

      observer.disconnect();

      chart["origin_dispose"] && chart["origin_dispose"]();
    };
  }

  return chart;
}

function setOption(
  instance: echarts.ECharts,
  option,
  notMerge: boolean = false,
  lazyUpdate: boolean = false
) {
  if (instance) {
    instance.setOption(option, notMerge, lazyUpdate);
  }
}

export { init, setOption };
