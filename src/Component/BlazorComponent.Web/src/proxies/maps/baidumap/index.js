const delay = (ms) => new Promise((resolve) => setTimeout(resolve, ms))

async function initMap(containerId, initArgs) {
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

        return map;
    } catch (error) {
        console.log(error);
        await delay(100);
        return await initMap(containerId, initArgs);
    }
}

export { initMap }