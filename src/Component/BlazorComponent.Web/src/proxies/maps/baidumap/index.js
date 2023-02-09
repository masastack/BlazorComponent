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

async function constructMarker(marker) {
    var m = new BMapGL.Marker(marker.point, {
        offset: marker.offset,
        rotation: marker.rotation,
        title: marker.title
    });

    return m;
}

async function constructLabel(label) {
    var l = new BMapGL.Label(label.content, {
        offset: label.offset,
        position: label.position
    });

    return l;
}

async function constructPolyline(polyline) {
    var pl = new BMapGL.Polyline(polyline.points, {
        strokeColor: polyline.strokeColor,
        strokeWeight: polyline.strokeWeight,
        strokeOpacity: polyline.strokeOpacity,
        strokeStyle: polyline.strokeStyle == 0 ? "solid" : "dashed",
        geodesic: polyline.geodesic,
        clip: polyline.clip
    });

    return pl;
}

const toBMapGLPoint = (point) => new BMapGL.Point(point.lng, point.lat);

async function constructPolygon(polygon) {
    var bmapPoints = [];

    polygon.points.forEach(element => {
        bmapPoints.push(toBMapGLPoint(element));
    });

    var pg = new BMapGL.Polygon(bmapPoints, {
        strokeColor: polygon.strokeColor,
        strokeWeight: polygon.strokeWeight,
        strokeOpacity: polygon.strokeOpacity,
        strokeStyle: polygon.strokeStyle == 0 ? "solid" : "dashed",
        fillColor: polygon.fillColor,
        fillOpacity: polygon.fillOpacity
    });

    return pg;
}

export { init, constructCircle, constructMarker, constructLabel, constructPolyline, constructPolygon }