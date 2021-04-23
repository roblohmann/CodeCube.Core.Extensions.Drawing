using System;
using System.Drawing;
using System.IO;

namespace CodeCube.Core.Extensions.Drawing
{
    public static class StreamExtensions
    {
        /// <summary>
        /// Get the image dimensions from the provided stream.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static Size GetImageDimensions(this Stream stream)
        {
            try
            {
                using (var myImage = Image.FromStream(stream))
                {
                    return new Size(myImage.Width, myImage.Height);
                }
            }
            catch(Exception exception)
            {
                throw new InvalidOperationException("Error getting the dimensions from the image!",exception);
            }
        }
    }
}
