class BaiduMapProxy {
    instance;
    dotNetHelper;

    constructor(containerId, initArgs) {
        this.instance = new BMapGL.Map(containerId);

        if (initArgs.enableScrollWheelZoom)
            this.instance.enableScrollWheelZoom();

        this.instance.centerAndZoom(initArgs.center, initArgs.zoom);

        if (initArgs.dark)
            this.instance.setMapStyleV2({
                styleId: initArgs.darkThemeId
            });
    }

    setDotNetObjectReference(dotNetHelper, events) {
        this.dotNetHelper = dotNetHelper;

        events.forEach((event_name) => {
            this.instance.addEventListener(event_name, async function (e) {
                if (event_name == "dragstart" ||
                    event_name == "dragging" ||
                    event_name == "dragend" ||
                    event_name == "dblclick")

                    await dotNetHelper.invokeMethodAsync("OnEvent", event_name, {
                        latlng: e.point,
                        pixel: e.pixel,
                    });

                else if (event_name == "click" ||
                         event_name == "rightclick" ||
                         event_name == "rightdblclick" ||
                         event_name == "mousemove")

                    await dotNetHelper.invokeMethodAsync("OnEvent", event_name, {
                        latlng: e.latlng,
                        pixel: e.pixel,
                    });

                else
                    await dotNetHelper.invokeMethodAsync("OnEvent", event_name, null);
            });
        });
    }

    getOriginInstance = () => this.instance;
}

const init = (containerId, initArgs) => new BaiduMapProxy(containerId, initArgs);

async function initCircle(circle) {
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

async function initMarker(marker) {
    var m = new BMapGL.Marker(marker.point, {
        offset: marker.offset,
        rotation: marker.rotation,
        title: marker.title
    });

    return m;
}

async function initLabel(label) {
    var l = new BMapGL.Label(label.content, {
        offset: label.offset,
        position: label.position
    });

    return l;
}

async function initPolyline(polyline) {
    if (polyline.points == null)
        return null;

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

async function initPolygon(polygon) {
    if (polygon.points == null)
        return null;

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

export { init, initCircle, initMarker, initLabel, initPolyline, initPolygon }