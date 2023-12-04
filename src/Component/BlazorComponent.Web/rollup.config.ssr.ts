import { defineConfig } from "rollup";
import { terser } from "rollup-plugin-terser";

import typescript from "@rollup/plugin-typescript";

export default defineConfig([
  {
    input: "./src/ssr/ssr-state-restore.ts",
    output: {
      file: "../../../../MASA.Blazor/wwwroot/js/ssr-state-restore.js",
      format: "iife",
      sourcemap: true,
    },
    plugins: [typescript(), terser()],
  },
  {
    input: "./src/ssr/page-script.ts",
    output: {
      file: "../../../../MASA.Blazor/wwwroot/js/ssr-page-script.js",
      format: "esm",
      sourcemap: true,
    },
    plugins: [typescript(), terser()],
  },
]);
