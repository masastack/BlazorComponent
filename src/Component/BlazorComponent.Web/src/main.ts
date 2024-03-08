import * as slider from "./components/slider";
import * as interop from "./interop";
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
    ...slider,
  },
};

window.MasaBlazor = {
  xgplayerPlugins: [],
};
