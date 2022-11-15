import * as echarts from "echarts";

function init(elOrString, theme, initOptions) {
  const chart = echarts.init(elOrString, theme, initOptions);
  window.onresize = function () {
    chart.resize();
  };
}

function setOption(
  instance: echarts.ECharts,
  option,
  notMerge: boolean = false,
  lazyUpdate: boolean = false
) {
  instance.setOption(option, notMerge, lazyUpdate);
}

function dispose(instance: echarts.ECharts) {
  instance.dispose();
}

function resize(instance: echarts.ECharts, width?: number, height?: number) {
  instance.resize({
    width,
    height,
  });
}

export { init, setOption, resize, dispose };
