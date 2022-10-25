import { defineConfig } from "rollup";

import commonjs from "@rollup/plugin-commonjs";
import typescript from "@rollup/plugin-typescript";

export default defineConfig({
  input: "./src/proxies/markdown-it/index.ts",
  output: [
    {
      file: "../BlazorComponent/wwwroot/js/markdown-it-proxy.js",
      format: "esm",
      sourcemap: true,
    },
  ],
  plugins: [
    typescript(),
    // json(),
    // resolve(),
    commonjs(),
  ],
});
