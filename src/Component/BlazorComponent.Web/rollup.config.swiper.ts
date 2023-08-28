import { defineConfig } from "rollup";
import { terser } from "rollup-plugin-terser";

import commonjs from "@rollup/plugin-commonjs";
import resolve from "@rollup/plugin-node-resolve";
import typescript from "@rollup/plugin-typescript";

export default defineConfig({
  input: "./src/proxies/swiper/index.ts",
  output: [
    {
      file: "../../../../MASA.Blazor/wwwroot/js/proxies/swiper-proxy.js",
      format: "esm",
      sourcemap: true,
    },
  ],
  plugins: [typescript(), resolve(), commonjs(),terser()],
  watch: {
    exclude: "node_modules/**",
  },
});
