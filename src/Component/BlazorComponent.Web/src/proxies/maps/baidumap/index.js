const delay = (ms) => new Promise((resolve) => setTimeout(resolve, ms))

var dotNetObjectReference;

async function initMap(containerId, initArgs, dotNetObjRef) {
    try {
        var map = new BMapGL.Map(containerId);

        if (initArgs.enableScrollWheelZoom)
            map.enableScrollWheelZoom();
        else
            map.disableScrollWheelZoom();

        map.centerAndZoom(new BMapGL.Point(initArgs.center.x, initArgs.center.y), initArgs.zoom);

        if (initArgs.dark)
            map.setMapStyleV2({
                styleId: initArgs.darkThemeId
            });

        dotNetObjectReference = dotNetObjRef;

        map.addEventListener('zoomend', async function (e) {
            await dotNetObjRef.invokeMethodAsync("OnJsZoomEnd", map.getZoom());
        });

        return map;
    } catch (error) {
        console.log(error);
        await delay(100);
        return await initMap(containerId, initArgs);
    }
}

export { initMap }