const delay = (ms) => new Promise((resolve) => setTimeout(resolve, ms))

var map;

async function injectBaiduMapScript(ak) {
    var script = document.createElement("script");
    script.src = "https://api.map.baidu.com/getscript?type=webgl&v=1.0&ak=" + ak;
    script.type = "text/javascript";

    var css = document.createElement("link");
    css.href = "https://api.map.baidu.com/res/webgl/10/bmap.css";
    css.type = "text/css";
    css.rel = "stylesheet";

    window.BMAP_PROTOCOL = "https";
    window.BMapGL_loadScriptTime = (new Date).getTime();
    document["head"].append(script, css);
}

async function loadMap(divID, initArgs) {
    try {
        map = new BMapGL.Map(divID);
        map.enableScrollWheelZoom(initArgs.canZoom);
        map.centerAndZoom(initArgs.mapCenter, initArgs.zoom);
    } catch (error) {
        await delay(100);
        await loadMap(divID);
    }
}

export { injectBaiduMapScript, loadMap, map }