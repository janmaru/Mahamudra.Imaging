using Mahamudra.Imaging.Core;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Mahamudra.Imaging.Infrastructure
{
    public static class SampleMatrix
    { 
        public static async Task<Image> Matrix(Picture picture, Rgba32 red, Rgba32 green, Rgba32 blue)
        {
            var matrix  = new ColorMatrix().ToColorMatrix(red: red, green: green, blue: blue);
            await picture.Filter(matrix);
            return picture.GetImage();
        }
    }
}
