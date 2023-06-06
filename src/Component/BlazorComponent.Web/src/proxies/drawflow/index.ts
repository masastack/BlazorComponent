import Drawflow, { DrawFlowEditorMode } from "drawflow";

class DrawflowProxy {
  editor: Drawflow;

  constructor(selector: string, mode: DrawFlowEditorMode) {
    var el = document.querySelector<HTMLElement>(selector);
    this.editor = new Drawflow(el);
    this.editor.start();
    this.editor.editor_mode = mode;
  }

  setMode(mode: DrawFlowEditorMode) {
    this.editor.editor_mode = mode;
  }

  addNode(
    name: string,
    inputs: number,
    outputs: number,
    pos_x: number,
    pos_y: number,
    className: string,
    data: JSON,
    html: string
  ) {
    return this.editor.addNode(
      name,
      inputs,
      outputs,
      pos_x,
      pos_y,
      className,
      data,
      html,
      false
    );
  }

  removeNodeId(id: string) {
    this.editor.removeNodeId(id);
  }

  export() {
    var res = this.editor.export();
    console.log('res', res)
    return JSON.stringify(res);
  }
}

function init(selector: string, mode: DrawFlowEditorMode = 'edit') {
  return new DrawflowProxy(selector, mode);
}

export { init };
