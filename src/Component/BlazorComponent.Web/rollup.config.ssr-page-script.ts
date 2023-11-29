import { defineConfig } from "rollup";
import { terser } from "rollup-plugin-terser";

import resolve from "@rollup/plugin-node-resolve";
import typescript from "@rollup/plugin-typescript";

export default defineConfig({
  input: "./src/ssr/page-script.ts",
  output: [
    {
      file: "../../../../MASA.Blazor/wwwroot/js/ssr-page-script.js",
      format: "esm",
      sourcemap: true,
    },
  ],
  plugins: [
    typescript(),
    // resolve(),
    // terser()
  ],
});
