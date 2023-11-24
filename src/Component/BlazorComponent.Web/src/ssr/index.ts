const themeKey = "masa-blazor-web-app-theme";

export function updateTheme(dark: boolean, updateCache: boolean = true) {
  const app = document.querySelector(".m-application");
  if (app) {
    app.classList.replace(getThemeCss(!dark), getThemeCss(dark));
  }

  if (updateCache) {
    localStorage.setItem(themeKey, dark ? "dark" : "light");
  }
}

export function initTheme() {
  const theme = localStorage.getItem(themeKey);
  if (theme) {
    updateTheme(theme === "dark", false);
  }

  return theme;
}

function getThemeCss(dark: boolean) {
  return dark ? "theme--dark" : "theme--light";
}
