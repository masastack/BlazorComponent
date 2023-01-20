const delay = ms => new Promise((resolve, reject) => setTimeout(resolve, ms))

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

async function loadMap(divID) {
    try {
        var map = new BMapGL.Map(divID);
        var point = new BMapGL.Point(116.404, 39.915);
        map.centerAndZoom(point, 15);
    } catch (error) {
        await delay(100);
        await loadMap(divID);
    }
}

export { injectBaiduMapScript, loadMap }
