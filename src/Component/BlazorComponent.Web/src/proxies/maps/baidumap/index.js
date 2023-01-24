const delay = (ms) => new Promise((resolve) => setTimeout(resolve, ms))

async function injectBaiduMapScript(ak) {
    // if script has been loaded, return. 
    if (document.getElementById("baidumap-script"))
        return;

    var script = document.createElement("script");
    script.src = "https://api.map.baidu.com/getscript?type=webgl&v=1.0&ak=" + ak;
    script.type = "text/javascript";
    script.id = "baidumap-script";

    var css = document.createElement("link");
    css.href = "https://api.map.baidu.com/res/webgl/10/bmap.css";
    css.type = "text/css";
    css.rel = "stylesheet";

    window.BMAP_PROTOCOL = "https";
    window.BMapGL_loadScriptTime = (new Date).getTime();
    document["head"].append(script, css);
}

async function loadMap(containerID, initArgs) {
    try {
        var map = new BMapGL.Map(containerID);
        map.enableScrollWheelZoom(initArgs.canZoom);
        map.centerAndZoom(new BMapGL.Point(initArgs.mapCenter.x, initArgs.mapCenter.y), initArgs.zoom);
        return map;
    } catch (error) {
        await delay(100);
        return await loadMap(containerID);
    }
}

export { injectBaiduMapScript, loadMap }