import * as slider from "./components/slider";
import * as interop from "./interop";
import * as overlayable from "./mixins/overlayable";
import { MarkdownParser } from "./proxies/markdown-it";

declare global {
  interface Window {
    BlazorComponent: any;
    MasaBlazor: {
      extendMarkdownIt?: (parser: MarkdownParser) => void;
      xgplayerPlugins: any[];
      xgplayerPluginOptions?: { [prop: string]: any };
    };
  }
}

window.BlazorComponent = {
  interop: {
    ...interop,
    ...overlayable,
    ...slider,
  },
};

window.MasaBlazor = {
  xgplayerPlugins: [],
};
