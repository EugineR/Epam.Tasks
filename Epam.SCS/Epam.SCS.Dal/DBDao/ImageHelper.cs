using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.SCS.Dal.DBDao
{
    public static class ImageHelper
    {
        public static Image Resize(Image sourceImage, int newWidth, int maxHeight, bool reduceOnly)
        {
            // Гарантия того, что не будет использована сохранённая внутри изображения миниатюра 
            sourceImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
            sourceImage.RotateFlip(RotateFlipType.Rotate180FlipNone);

            if (reduceOnly && sourceImage.Width <= newWidth)
            {
                newWidth = sourceImage.Width;
            }

            int newHeight = sourceImage.Height * newWidth / sourceImage.Width;
            if (newHeight > maxHeight)
            {
                newWidth = sourceImage.Width * maxHeight / sourceImage.Height;
                newHeight = maxHeight;
            }

            Image newImage = sourceImage.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);

            return newImage;
        }
    }
}
