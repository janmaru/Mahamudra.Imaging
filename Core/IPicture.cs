using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;

namespace Mahamudra.Imaging.Core
{
    public interface IPicture
    {
        int Id { get; set; }

        Task<byte[]> Clone();
        Task Crop(Point point, Size size);
        Task Filter(ColorMatrix matrix);
        Task<byte[]> GetBytes();
        IImageFormat GetFormat();
        Image GetImage();
        Task Resize(Size size);
        Size Size();
    }
}