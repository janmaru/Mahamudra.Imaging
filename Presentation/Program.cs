using Mahamudra.Imaging.Infrastructure;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using static Microsoft.AspNetCore.Http.Results;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseStaticFiles();
var files = await Loader.Load();

app.MapGet("/", async (HttpContext http, string? action) =>
{
    action ??= "compose";
    Image? image = null;
    switch (action)
    {
        case "resize":
            image = await SampleResize.Resize(files.Last());
            break;
        case "compose":
            image = await SampleCompose.Compose(files.Filter(new int[] { 21, 11 }));
            break;
        case "matrix":
            image = await SampleMatrix.Matrix(files.Last(), Rgba32.ParseHex("#6666aa"), Rgba32.ParseHex("#0f4256"), Rgba32.ParseHex("#265a6e"));
            break;
        default:
            break;
    }

    byte[] img;
    using (var memoryStream = new MemoryStream())
        img = await image.SaveAsync(memoryStream, PngFormat.Instance)
             .ContinueWith(i => memoryStream.ToArray());
    return Bytes(img, "image/png");
});

app.Run();