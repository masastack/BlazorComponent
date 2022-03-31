export async function GetImageUsingStreaming(imageStreams) {

    const imageUrls = [];
    for (var imageStream of imageStreams) {
        const arrayBuffer = await imageStream.arrayBuffer();
        const blob = new Blob([arrayBuffer]);
        const url = URL.createObjectURL(blob);
        imageUrls.push(url);
    }
    return imageUrls;
}
