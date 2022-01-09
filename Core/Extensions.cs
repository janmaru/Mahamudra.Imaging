using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;

namespace Mahamudra.Imaging.Core
{
    public static class Extensions
    {
        public static async Task<byte[]> ToBytes(this Image image, IImageFormat format)
        { 
            using Stream outputStream = new MemoryStream();
            {
                if (format.Name == "JPEG")
                    await image.SaveAsJpegAsync(outputStream);
                else if (format.Name == "PNG")
                    await image.SaveAsPngAsync(outputStream);
                else if (format.Name == "GIF")
                    await image.SaveAsGifAsync(outputStream);
                else if (format.Name == "BMP")
                    await image.SaveAsBmpAsync(outputStream);
                else if (format.Name == "TGA")
                    await image.SaveAsTgaAsync(outputStream);
                else
                    throw new NotSupportedException($"{format} is not supported!");
                return outputStream.ToBytes();
            } 
        }

        public static byte[] ToBytes(this Stream value)
        {
            value.Position = 0;
            byte[] buffer = new byte[value.Length];
            for (int totalBytesCopied = 0; totalBytesCopied < value.Length;)
                totalBytesCopied += value.Read(buffer, totalBytesCopied, Convert.ToInt32(value.Length) - totalBytesCopied);
            return buffer;
        }

        public static Stream ToStream(this byte[] bytes)
        {
            return new MemoryStream(bytes)
            {
                Position = 0
            };
        }

        public static Stream Copy(this byte[] bytes, Stream stream)
        {
            using (var writer = new BinaryWriter(stream))
                writer.Write(bytes);
            return stream;
        }

        public static byte[] Copy(this Stream stream, byte[] bytes)
        {
            using (var writer = new BinaryWriter(stream))
                writer.Write(bytes);
            return bytes;
        }

        public static (Image Image, IImageFormat Format) GetFormat(this byte[] value)
        {
            return (Image.Load(value, out IImageFormat format), format);
        } 

        public static IImageFormat GetFormat(this Stream value)
        {
            using var image = Image.Load(value, out IImageFormat format);
            return format;
        }

        public static async Task<(Image Image, IImageFormat Format)> Load(this string path)
        {
            var bytes = await path.ReadImageFromIO();
            return await Image.LoadWithFormatAsync(bytes.ToStream());
        }

        private static async Task<byte[]> ReadImageFromIO(this string pathSource)
        {
            using FileStream fsSource = new(pathSource, FileMode.Open, FileAccess.Read); 
            byte[] bytes = new byte[fsSource.Length];
            int numBytesToRead = (int)fsSource.Length;
            int numBytesRead = 0;
            while (numBytesToRead > 0)
            { 
                int n = await fsSource.ReadAsync(bytes, numBytesRead, numBytesToRead); 
                if (n == 0)
                    break; 
                numBytesRead += n;
                numBytesToRead -= n;
            }
            numBytesToRead = bytes.Length;
            return bytes;
        }


        public static ColorMatrix ToColorMatrix(this ColorMatrix colorMatrix, Rgba32 red, Rgba32 green, Rgba32 blue)
        {
            colorMatrix.M11 = (red.R * 1.0F) / 255F;
            colorMatrix.M12 = (red.G * 1.0F) / 255F;
            colorMatrix.M13 = (red.B * 1.0F) / 255F;
            colorMatrix.M14 = 0F;

            colorMatrix.M21 = (green.R * 1.0F) / 255F;
            colorMatrix.M22 = (green.G * 1.0F) / 255F;
            colorMatrix.M23 = (green.B * 1.0F) / 255F;
            colorMatrix.M24 = 0F;

            colorMatrix.M31 = (blue.R * 1.0F) / 255F;
            colorMatrix.M32 = (blue.G * 1.0F) / 255F;
            colorMatrix.M33 = (blue.B * 1.0F) / 255F;
            colorMatrix.M34 = 0F;

            colorMatrix.M41 = 0F;
            colorMatrix.M42 = 0F;
            colorMatrix.M43 = 0F;
            colorMatrix.M44 = 1F;

            colorMatrix.M51 = 0F;
            colorMatrix.M52 = 0F;
            colorMatrix.M53 = 0F;
            colorMatrix.M54 = 0F;

            return colorMatrix;
        }
    }
}
