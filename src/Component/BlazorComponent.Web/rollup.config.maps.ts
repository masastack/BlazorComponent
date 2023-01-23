import { defineConfig } from "rollup";
import { terser } from "rollup-plugin-terser";

import typescript from "@rollup/plugin-typescript";

export default defineConfig({
  input: "./src/proxies/maps/baidumap/index.js",
  output: [
    {
      file: "../../../../MASA.Blazor/wwwroot/js/proxies/baidumap-proxy.js",
      format: "esm",
      sourcemap: true,
    },
  ],
  plugins: [terser()],
});
