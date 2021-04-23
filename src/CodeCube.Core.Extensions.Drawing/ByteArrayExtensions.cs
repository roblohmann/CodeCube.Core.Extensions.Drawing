using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace CodeCube.Core.Extensions.Drawing
{
    /// <summary>
    /// Class with extension methods for bytearrays.
    /// </summary>
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Changes the DPI value of an image to 72 DPI
        /// </summary>
        /// <param name="imageToFit">The image to modify.</param>
        /// <param name="mimeType">The mimetype of the image.</param>
        /// <param name="newSize">The desired size of the image</param>
        /// <param name="dpi">The DPI's to set.</param>
        /// <returns>The memorystream with the resize image as byte array.</returns>
        public static byte[] SetDpi(this byte[] imageToFit, string mimeType, Size newSize, int dpi = 72)
        {
            using (MemoryStream memoryStream = new MemoryStream(), newMemoryStream = new MemoryStream())
            {
                memoryStream.Write(imageToFit, 0, imageToFit.Length);
                var originalImage = new Bitmap(memoryStream);
                var newBitmap = new Bitmap(newSize.Width, newSize.Height);

                using (var canvas = Graphics.FromImage(originalImage))
                {
                    canvas.SmoothingMode = SmoothingMode.AntiAlias;
                    canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    canvas.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    canvas.DrawImage(originalImage, 0, 0, newSize.Width, newSize.Height);

                    newBitmap.SetResolution(dpi, dpi);
                    newBitmap.Save(newMemoryStream, ImageFunctions.GetEncoderInfo(mimeType), null);
                }
                return newMemoryStream.ToArray();
            }
        }
    }
}
