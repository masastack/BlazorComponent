import { defineConfig } from "rollup";
import { terser } from "rollup-plugin-terser";

import typescript from "@rollup/plugin-typescript";

export default defineConfig({
  input: "./src/map.js",
  output: [
    {
      file: "../BlazorComponent/wwwroot/js/map.js",
      format: "esm",
      sourcemap: true,
    },
  ],
  plugins: [typescript(), terser()],
});
