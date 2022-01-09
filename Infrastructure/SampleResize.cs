using Mahamudra.Imaging.Core;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Mahamudra.Imaging.Infrastructure
{
    public class SampleResize
    {
        public static async Task<Image> Resize(Picture picture)
        {
            await picture.Resize(new Size(512, 512));
            return picture.GetImage();
        } 
    }
}
