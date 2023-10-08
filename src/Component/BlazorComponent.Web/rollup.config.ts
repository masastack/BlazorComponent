import { defineConfig } from "rollup";
import { terser } from "rollup-plugin-terser";

import resolve from "@rollup/plugin-node-resolve";
import typescript from "@rollup/plugin-typescript";

export default defineConfig({
  input: "./src/main.ts",
  output: [
    {
      file: "../BlazorComponent/wwwroot/js/blazor-component.js",
      format: "iife",
      sourcemap: true,
    },
  ],
  plugins: [typescript(), resolve(), terser()],
});
