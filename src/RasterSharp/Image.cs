using System;
using System.IO;

namespace RasterSharp;

public class Image
{
    public int Width { get; }
    public int Height { get; }
    public Channel Red { get; }
    public Channel Green { get; }
    public Channel Blue { get; }
    public Channel Alpha { get; }

    public Image(int width, int height)
    {
        Width = width;
        Height = height;

        Red = new(width, height);
        Green = new(width, height);
        Blue = new(width, height);
        Alpha = new(width, height);
    }

    public Image(Channel r, Channel g, Channel b)
    {
        if (r.Width != g.Width || r.Width != b.Width)
            throw new InvalidOperationException("image widths must be equal");

        if (r.Height != g.Height || r.Height != b.Height)
            throw new InvalidOperationException("image widths must be equal");

        Width = r.Width;
        Height = r.Height;

        Red = r;
        Green = g;
        Blue = b;
        Alpha = new(r.Width, r.Height);
    }

    public Image(Channel r, Channel g, Channel b, Channel a)
    {
        if (r.Width != g.Width || r.Width != b.Width || r.Width != a.Width)
            throw new InvalidOperationException("image widths must be equal");

        if (r.Height != g.Height || r.Height != b.Height || r.Height != a.Height)
            throw new InvalidOperationException("image widths must be equal");

        Width = r.Width;
        Height = r.Height;

        Red = r;
        Green = g;
        Blue = b;
        Alpha = a;
    }

    public Image(string path)
    {
        byte[] bytes = File.ReadAllBytes(path);
        Image image = BitmapIO.FromBytes(bytes);

        Width = image.Width;
        Height = image.Height;

        Red = image.Red;
        Green = image.Green;
        Blue = image.Blue;
        Alpha = image.Alpha;
    }

    public Image(byte[] bytes)
    {
        Image image = BitmapIO.FromBytes(bytes);

        Width = image.Width;
        Height = image.Height;

        Red = image.Red;
        Green = image.Green;
        Blue = image.Blue;
        Alpha = image.Alpha;
    }

    public int GetRGBA(int x, int y)
    {
        byte r = Red.GetByte(x, y);
        byte g = Green.GetByte(x, y);
        byte b = Blue.GetByte(x, y);
        byte a = Alpha.GetByte(x, y);
        return ColorConverter.ToRGBA(r, g, b, a);
    }

    public byte[] GetBitmapBytes()
    {
        return BitmapIO.GetBitmapBytes(this);
    }

    public void Save(string path)
    {
        if (!path.EndsWith(".bmp", StringComparison.InvariantCultureIgnoreCase))
            throw new InvalidOperationException("filename must end with .bmp");

        System.IO.File.WriteAllBytes(path, GetBitmapBytes());
    }

    public Image Crop(int left, int top, int width, int height)
    {
        Channel red = new(width, height);
        Channel green = new(width, height);
        Channel blue = new(width, height);
        Channel alpha = new(width, height);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                red.SetValue(x, y, Red.GetValue(x + left, y + top));
                green.SetValue(x, y, Green.GetValue(x + left, y + top));
                blue.SetValue(x, y, Blue.GetValue(x + left, y + top));
                alpha.SetValue(x, y, Alpha.GetValue(x + left, y + top));
            }
        }

        return new(red, green, blue, alpha);
    }
}
