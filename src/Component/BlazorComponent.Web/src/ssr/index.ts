type MainPadding = {
  top: number;
  right: number;
  bottom: number;
  left: number;
};

export function setTheme(dark: boolean, updateCache: boolean = true) {
  const app = document.querySelector(".m-application");
  if (app) {
    app.classList.replace(getThemeCss(!dark), getThemeCss(dark));
  }
}

export function setMain(value: MainPadding) {
  const main: HTMLElement = document.querySelector(".m-main");

  const padding: MainPadding = {
    top: value.top ?? getPadding("Top"),
    right: value.right ?? getPadding("Right"),
    bottom: value.bottom ?? getPadding("Bottom"),
    left: value.left ?? getPadding("Left"),
  };

  main.style.paddingTop = `${padding.top}px`;
  main.style.paddingRight = `${padding.right}px`;
  main.style.paddingBottom = `${padding.bottom}px`;
  main.style.paddingLeft = `${padding.left}px`;

  function getPadding(prop: "Top" | "Right" | "Bottom" | "Left") {
    return Number(main.style[`padding${prop}`].match(/\d+/)[0]);
  }
}

function getThemeCss(dark: boolean) {
  return dark ? "theme--dark" : "theme--light";
}
