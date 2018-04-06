using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace Common
{
    public static class ImageHelper
    {
        /// <summary>
        ///  Tạo hình thumbnail cho hình ảnh
        /// </summary>
        /// <param name="maxWidth">Chiều dài lớn nhất</param>
        /// <param name="maxHeight">Chiều rộng lớn nhất</param>
        /// <param name="path">đường dẫn hình</param>
        /// <param name="pathSave">đường dẫn save hình</param>
        /// <returns></returns>
        public static string CreateThumbnail(int maxWidth, int maxHeight, string path, string pathSave)
        {
            var image = Image.FromFile(path);
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);
            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);
            var newImage = new Bitmap(newWidth, newHeight);
            Graphics thumbGraph = Graphics.FromImage(newImage);

            thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
            thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
            //thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            thumbGraph.DrawImage(image, 0, 0, newWidth, newHeight);
            image.Dispose();

            string fileRelativePath = Path.GetFileName(path);
            newImage.Save(pathSave + fileRelativePath, newImage.RawFormat);
            return fileRelativePath;
        }
    }
}
