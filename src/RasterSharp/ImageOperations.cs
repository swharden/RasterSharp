using System.Drawing;

namespace RasterSharp;

public static class ImageOperations
{
    public static Image Crop(Image img, Rectangle rect)
    {
        int width = rect.Width;
        int height = rect.Height;

        Channel red = new(width, height);
        Channel green = new(width, height);
        Channel blue = new(width, height);
        Channel alpha = new(width, height);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                red.SetValue(x, y, img.Red.GetValue(x + rect.Left, y + rect.Top));
                green.SetValue(x, y, img.Green.GetValue(x + rect.Left, y + rect.Top));
                blue.SetValue(x, y, img.Blue.GetValue(x + rect.Left, y + rect.Top));
                alpha.SetValue(x, y, img.Alpha.GetValue(x + rect.Left, y + rect.Top));
            }
        }

        return new(red, green, blue, alpha);
    }

    public static Image Resize(Image img, int width, int height, bool smooth = false)
    {
        if (smooth)
        {
            // TODO: add support for bicubic sampling
            throw new System.NotImplementedException("Cubic interpolation is not yet supported.");
        }

        Image img2 = new(width, height);

        for (int y = 0; y < height; y++)
        {
            int sourceY = (int)((double)y / height * img.Height);

            for (int x = 0; x < width; x++)
            {
                int sourceX = (int)((double)x / width * img.Width);

                int color = img.GetRGBA(sourceX, sourceY);
                img2.SetRGBA(x, y, color);
            }
        }

        return img2;
    }
}
