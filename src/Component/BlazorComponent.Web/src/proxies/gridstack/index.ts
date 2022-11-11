import { GridStack, GridStackElement, GridStackOptions } from "gridstack";

import { getBlazorId, getElementSelector } from "../../utils/helper";

type GridStackDict = {
  [prop: string]: GridStack;
};

const gsDict: GridStackDict = {};

function init(
  elOrString: GridStackElement = ".grid-stack",
  options: GridStackOptions = {}
) {
  console.log(options);
  const grid = GridStack.init(
    {
      column: 10,
      minRow: 1,
    },
    elOrString
  );
  const key = genGridKey(elOrString);
  gsDict[key] = grid;
}

function reload(elOrString: GridStackElement) {
  const key = genGridKey(elOrString);
  let grid = gsDict[key];
  if (grid) {
    grid.destroy(false);
    grid = GridStack.init(grid.opts, elOrString);
    gsDict[key] = grid;
  }
}

function genGridKey(elOrString: GridStackElement) {
  let selector;
  if (typeof elOrString === "string") {
    selector = elOrString;
  } else {
    selector = getBlazorId(elOrString);
    if (!selector) {
      selector = getElementSelector(elOrString);
    }
  }

  return selector;
}

export { init, reload };
