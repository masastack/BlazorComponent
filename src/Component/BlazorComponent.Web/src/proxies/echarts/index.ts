function init(elOrString, theme, initOptions) {
  const chart = echarts.init(elOrString, theme, initOptions);

  window.addEventListener("resize", () => {
    const dom = chart.getDom();
    const width = dom.clientWidth;
    const height = dom.clientHeight;
    chart.resize({ width, height });
  });

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

function dispose(instance: echarts.ECharts) {
  if (instance) {
    instance.dispose();
  }
}

function resize(instance: echarts.ECharts, width?: number, height?: number) {
  if (instance) {
    console.log("resize", width, height);
    instance.resize({ width, height });
  }
}

export { init, setOption, resize, dispose };
