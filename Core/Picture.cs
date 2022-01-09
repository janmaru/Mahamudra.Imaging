using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;


namespace Mahamudra.Imaging.Core
{
    public class Picture : IPicture
    {
        public int Id { get; set; }
        private readonly Image<Rgba32> _image;
        private readonly IImageFormat _format;
        public Picture(byte[] bytes)
        {
            var fm = bytes.GetFormat();
            this._format = fm.Format;
            this._image = (Image<Rgba32>)fm.Image;
        }

        public Picture(Image image, IImageFormat format, int id)
        {
            this._format = format;
            this._image = (Image<Rgba32>)image;
            this.Id = id;
        }

        public async Task Resize(Size size)
        {
            await Task.Run(() => _image.Mutate(c => c.Resize(size.Width, size.Height)));
        }

        public async Task Crop(Point point, Size size)
        {
            await Task.Run(() => _image.Mutate(c => c.Crop(new Rectangle() { Height = size.Height, Width = size.Width, X = point.X, Y = point.Y })));
        }

        public async Task Filter(ColorMatrix matrix)
        {
            await Task.Run(() => _image.Mutate(x => x.Filter(matrix)));
        }

        public async Task<byte[]> GetBytes()
        {
            return await _image.ToBytes(_format);
        }

        public IImageFormat GetFormat()
        {
            return _format;
        }

        public Image GetImage()
        {
            return _image;
        }

        public Size Size()
        {
            return new Size() { Height = _image.Height, Width = _image.Width };
        }

        public async Task<byte[]> Clone()
        {
            using Image<Rgba32> clone = _image.Clone();
            return await clone.ToBytes(_format);
        }
    }
}
