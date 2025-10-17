using ZXing;
using ZXing.Common;
using ZXing.ImageSharp;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using System.IO;

app.MapGet("/barcode/{text}", (string text) =>
{
    var writer = new ZXing.ImageSharp.BarcodeWriterPixelData
    {
        Format = ZXing.BarcodeFormat.CODE_128,
        Options = new EncodingOptions
        {
            Height = 150,
            Width  = 400,
            Margin = 1
        }
    };

    var pixelData = writer.Write(text); // gives PixelData (byte[])

    // Create ImageSharp image from raw pixels
    using var image = Image.LoadPixelData<Rgba32>(pixelData.Pixels, pixelData.Width, pixelData.Height);

    using var ms = new MemoryStream();
    image.Save(ms, new PngEncoder());
    return Results.File(ms.ToArray(), "image/png");
});
