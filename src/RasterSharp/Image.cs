using System;
using System.Drawing;
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

    public void DrawRectangle(Point pt, int radius, int color)
    {
        Rectangle rect = new(
            x: pt.X - radius,
            y: pt.Y - radius,
            width: radius * 2,
            height: radius * 2);

        DrawRectangle(rect, color);
    }

    public void FillRectangle(Point pt, int radius, int color)
    {
        Rectangle rect = new(
            x: pt.X - radius,
            y: pt.Y - radius,
            width: radius * 2,
            height: radius * 2);

        FillRectangle(rect, color);
    }

    public void FillRectangle(Rectangle rect, int color)
    {
        var colors = ColorConverter.FromRGBA(color);
        Red.FillRectangle(rect, colors.r);
        Green.FillRectangle(rect, colors.g);
        Blue.FillRectangle(rect, colors.b);
        Alpha.FillRectangle(rect, colors.a);
    }

    public void DrawRectangle(Rectangle rect, int color)
    {
        var colors = ColorConverter.FromRGBA(color);
        Red.DrawRectangle(rect, colors.r);
        Green.DrawRectangle(rect, colors.g);
        Blue.DrawRectangle(rect, colors.b);
        Alpha.DrawRectangle(rect, colors.a);
    }

    public void DrawLine(Point pt1, Point pt2, int color)
    {
        var colors = ColorConverter.FromRGBA(color);
        Red.DrawLine(pt1, pt2, colors.r);
        Green.DrawLine(pt1, pt2, colors.g);
        Blue.DrawLine(pt1, pt2, colors.b);
        Alpha.DrawLine(pt1, pt2, colors.a);
    }
}
