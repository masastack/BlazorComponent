import { GridItemHTMLElement, GridStack, GridStackElement, GridStackOptions } from "gridstack";

import { getBlazorId } from "../../utils/helper";

function init(
  options: GridStackOptions = {},
  elOrString: GridStackElement = ".grid-stack",
  dotNet: DotNet.DotNetObject
) {
  const grid = GridStack.init(options, elOrString);
  grid["dotNet"] = dotNet;
  addEvents(grid);
  return grid;
}

function setStatic(grid: GridStack, staticValue: boolean) {
  if (grid) {
    grid.setStatic(staticValue);
  }
}

function reload(grid: GridStack) {
  if (grid) {
    const opts = { ...grid.opts };
    const el = grid.el;
    grid.destroy(false);
    const dotNet = grid["dotNet"];
    grid = GridStack.init(opts, el);
    grid["dotNet"] = dotNet;
    addEvents(grid);
    return grid;
  }

  return grid;
}

function addEvents(grid: GridStack) {
  const dotNet: DotNet.DotNetObject = grid["dotNet"];
  grid.on("resize", function (event: Event, el: GridItemHTMLElement) {
    dotNet.invokeMethodAsync("OnResize", ...resize(event, el));
  });

  grid.on("resizestop", function (event: Event, el: GridItemHTMLElement) {
    dotNet.invokeMethodAsync("OnResizeStop", ...resize(event, el));
  });
}

function resize(event: Event, el: GridItemHTMLElement) {
  const customElement = el.firstElementChild.firstElementChild;
  let blazorId;
  let id;
  let width = 0;
  let height = 0;
  if (customElement) {
    blazorId = getBlazorId(customElement);
    id = customElement.getAttribute("id");
    width = customElement.clientWidth;
    height = customElement.clientHeight;
  }
  return [blazorId, id, width, height];
}

export { init, reload, setStatic };
