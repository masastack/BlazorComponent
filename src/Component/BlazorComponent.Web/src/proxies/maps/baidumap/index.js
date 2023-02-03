async function init(containerId, initArgs, dotNetObjRef) {
    var map = new BMapGL.Map(containerId);

    if (initArgs.enableScrollWheelZoom)
        map.enableScrollWheelZoom();

    map.centerAndZoom(initArgs.center, initArgs.zoom);

    if (initArgs.dark)
        map.setMapStyleV2({
            styleId: initArgs.darkThemeId
        });

    map.addEventListener('zoomend', async function (e) {
        await dotNetObjRef.invokeMethodAsync("OnJsZoomEnd", map.getZoom());
    });

    map.addEventListener('moveend', async function (e) {
        await dotNetObjRef.invokeMethodAsync("OnJsMoveEnd", map.getCenter());
    });

    return map;
}

export { init }