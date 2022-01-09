using Mahamudra.Imaging.Core;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Mahamudra.Imaging.Infrastructure
{
    public class SampleCompose
    { 
        public static async Task<Image> Compose(List<Picture> pics)
        {
            var root = pics.First().GetImage();
            for (int i = 0; i < pics.Count; i++)
            {
                await pics[i].Resize(new Size(512, 512));
                var imageLayer = pics[i].GetImage();
                root.Mutate(o => o
                   .DrawImage(imageLayer, 1f)
                );
            }
            return root;
        }
    }
}
