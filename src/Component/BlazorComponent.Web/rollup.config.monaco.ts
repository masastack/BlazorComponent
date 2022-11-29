import { defineConfig } from "rollup";
import { terser } from "rollup-plugin-terser";

import typescript from "@rollup/plugin-typescript";

export default defineConfig({
  input: "./src/proxies/monaco/index.ts",
  output: [
    {
      file: "../../../../MASA.Blazor/wwwroot/js/proxies/monaco-proxy.js",
      format: "esm",
      sourcemap: true,
    },
  ],
  plugins: [typescript(), terser()],
});
