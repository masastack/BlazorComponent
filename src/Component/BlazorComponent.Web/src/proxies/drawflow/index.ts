import Drawflow, { DrawFlowEditorMode, DrawflowModuleData } from "drawflow";

class DrawflowProxy {
  editor: Drawflow;

  constructor(selector: string, mode: DrawFlowEditorMode) {
    var el = document.querySelector<HTMLElement>(selector);
    this.editor = new Drawflow(el);
    this.editor.start();
    this.editor.editor_mode = mode;
    const that = this;

    this.editor.on("nodeCreated", function (id) {
      const node = that.editor.getNodeFromId(id);
      console.log("nodeCreated", id, node);
    });
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
    data: object = {},
    html: string
  ) {
    var nodeId = this.editor.addNode(
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

    return nodeId;
  }

  removeNodeId(id: string) {
    this.editor.removeNodeId(id);
  }

  updateNodeDataFromId(id: number, data: object) {
    this.editor.updateNodeDataFromId(id, data);
  }

  export(withoutData: boolean = false) {
    const res = this.editor.export();

    if (withoutData) {
      const workspace_keys = Object.keys(res.drawflow);
      for (const workspace_key of workspace_keys) {
        const workspace = res.drawflow[workspace_key].data;
        const node_keys = Object.keys(workspace);
        for (const node_key of node_keys) {
          const node = workspace[node_key];
          node.data = {};
        }
      }
    }

    return JSON.stringify(res);
  }
}

function init(selector: string, mode: DrawFlowEditorMode = "edit") {
  return new DrawflowProxy(selector, mode);
}

export { init };
