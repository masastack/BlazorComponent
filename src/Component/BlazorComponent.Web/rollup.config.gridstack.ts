import { defineConfig } from "rollup";
import { terser } from "rollup-plugin-terser";

import commonjs from "@rollup/plugin-commonjs";
import resolve from "@rollup/plugin-node-resolve";
import typescript from "@rollup/plugin-typescript";

export default defineConfig({
  input: "./src/proxies/gridstack/index.ts",
  output: [
    {
      file: "../BlazorComponent/wwwroot/js/gridstack-proxy.js",
      format: "esm",
      sourcemap: true,
    },
  ],
  plugins: [typescript(), resolve(), commonjs(), terser()],
});