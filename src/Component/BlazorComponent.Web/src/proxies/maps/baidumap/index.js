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

async function constructCircle(circle) {
    var c = new BMapGL.Circle(circle.center, circle.radius, {
        strokeColor: circle.strokeColor,
        strokeWeight: circle.strokeWeight,
        strokeOpacity: circle.strokeOpacity,
        strokeStyle: circle.strokeStyle == 0 ? "solid" : "dashed",
        fillColor: circle.fillColor,
        fillOpacity: circle.fillOpacity
    });

    return c;
}

export { init, constructCircle }