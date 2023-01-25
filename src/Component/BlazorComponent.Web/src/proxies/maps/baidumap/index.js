const delay = (ms) => new Promise((resolve) => setTimeout(resolve, ms))

async function injectBaiduMapScript(ak) {
    if (!document.getElementById(ak)) {
        var script = document.createElement("script");
        script.src = "https://api.map.baidu.com/getscript?type=webgl&v=1.0&ak=" + ak;
        script.type = "text/javascript";
        script.id = ak;
        document["head"].append(script);
    }

    if (!document.getElementById("baidu-map-css")) {
        var css = document.createElement("link");
        css.href = "https://api.map.baidu.com/res/webgl/10/bmap.css";
        css.type = "text/css";
        css.rel = "stylesheet";
        css.id = "baidu-map-css";
        document["head"].append(css);
    }

    window.BMAP_PROTOCOL = "https";
    window.BMapGL_loadScriptTime = (new Date).getTime();
}

async function loadMap(containerId, initArgs) {
    try {
        var map = new BMapGL.Map(containerId);

        if (initArgs.enableScrollWheelZoom)
            map.enableScrollWheelZoom();
        else
            map.disableScrollWheelZoom();
        
        map.centerAndZoom(new BMapGL.Point(initArgs.center.x, initArgs.center.y), initArgs.zoom);

        return map;
    } catch (error) {
        await delay(100);
        return await loadMap(containerId, initArgs);
    }
}

export { injectBaiduMapScript, loadMap }