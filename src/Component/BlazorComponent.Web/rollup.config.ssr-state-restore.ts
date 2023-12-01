import { defineConfig } from "rollup";
import { terser } from "rollup-plugin-terser";

import resolve from "@rollup/plugin-node-resolve";
import typescript from "@rollup/plugin-typescript";

export default defineConfig({
  input: "./src/ssr/ssr-state-restore.ts",
  output: [
    {
      file: "../../../../MASA.Blazor/wwwroot/js/ssr-state-restore.js",
      format: "iife",
      sourcemap: true,
    },
  ],
  plugins: [
    typescript(),
    // resolve(),
    // terser()
  ],
});
