using Mahamudra.Imaging.Core;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Mahamudra.Imaging.Infrastructure
{
    public static class Loader
    {
        public static async Task<List<Picture>> Load()
        {
            var pics = new List<Picture>();
            var wwwroot = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images");
            foreach (var filePath in Directory.EnumerateFiles(wwwroot))
            {
                if (!filePath.Contains("volleyball-clipart-transparent-"))
                    continue; 
                var imf = await filePath.Load();
                pics.Add(new Picture(imf.Image, imf.Format, filePath.GetId()));
            }
            return pics;
        }

        public static List<Picture> Filter(this List<Picture> pics, int[] ids)
        {
            return pics.Where(x => ids.Contains(x.Id)).ToList();
        }
    }
}
