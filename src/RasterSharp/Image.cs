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

    public Image Clone()
    {
        Image img = new(Width, Height);

        for (int y = 0; y < img.Height; y++)
        {
            for (int x = 0; x < img.Width; x++)
            {
                img.SetRGBA(x, y, GetRGBA(x, y));
            }
        }

        return img;
    }

    public int GetRGBA(int x, int y)
    {
        byte r = Red.GetByte(x, y);
        byte g = Green.GetByte(x, y);
        byte b = Blue.GetByte(x, y);
        byte a = Alpha.GetByte(x, y);
        return Color.FromRGBA(r, g, b, a);
    }

    public void SetRGBA(int x, int y, int rgba)
    {
        var colors = Color.Bytes(rgba);
        Red.SetValue(x, y, colors.r);
        Green.SetValue(x, y, colors.g);
        Blue.SetValue(x, y, colors.b);
        Alpha.SetValue(x, y, colors.a);
    }

    public byte[] GetBitmapBytes()
    {
        return BitmapIO.GetBitmapBytes(this);
    }

    public void SaveBmp(string path)
    {
        if (!path.EndsWith(".bmp", StringComparison.InvariantCultureIgnoreCase))
            throw new InvalidOperationException("filename must end with .bmp");

        System.IO.File.WriteAllBytes(path, GetBitmapBytes());
    }

    public void Fill(int color = 0)
    {
        Rectangle rect = new(0, 0, Width, Height);
        FillRectangle(rect, color);
    }

    public void FillRectangle(int x, int y, int width, int height, int color)
    {
        Rectangle rect = new(x, y, width, height);
        FillRectangle(rect, color);
    }

    public void FillRectangle(Rectangle rect, int color)
    {
        var colors = Color.Bytes(color);
        Red.FillRectangle(rect, colors.r);
        Green.FillRectangle(rect, colors.g);
        Blue.FillRectangle(rect, colors.b);
        Alpha.FillRectangle(rect, colors.a);
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

    public void DrawRectangle(int x, int y, int width, int height, int color)
    {
        Rectangle rect = new(x, y, width, height);
        DrawRectangle(rect, color);
    }

    public void DrawRectangle(Rectangle rect, int color)
    {
        var colors = Color.Bytes(color);
        Red.DrawRectangle(rect, colors.r);
        Green.DrawRectangle(rect, colors.g);
        Blue.DrawRectangle(rect, colors.b);
        Alpha.DrawRectangle(rect, colors.a);
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

    public void DrawLine(int x1, int y1, int x2, int y2, int color)
    {
        Point pt1 = new(x1, y1);
        Point pt2 = new(x2, y2);
        DrawLine(pt1, pt2, color);
    }

    public void DrawLine(Point pt1, Point pt2, int color)
    {
        var colors = Color.Bytes(color);
        Red.DrawLine(pt1, pt2, colors.r);
        Green.DrawLine(pt1, pt2, colors.g);
        Blue.DrawLine(pt1, pt2, colors.b);
        Alpha.DrawLine(pt1, pt2, colors.a);
    }

    // TODO: draw a thick line tangend to the line angle

    public void DrawThickLineX(Point pt1, Point pt2, int color, int thickness)
    {
        int x1 = pt1.X;
        int y1 = pt1.Y;
        int x2 = pt2.X;
        int y2 = pt2.Y;

        for (int i = 0; i < thickness; i++)
        {
            DrawLine(x1 + i, y1, x2 + i, y2, color);
        }
    }

    public void DrawThickLineY(Point pt1, Point pt2, int color, int thickness)
    {
        int x1 = pt1.X;
        int y1 = pt1.Y;
        int x2 = pt2.X;
        int y2 = pt2.Y;

        for (int i = 0; i < thickness; i++)
        {
            DrawLine(x1, y1 + i, x2, y2 + i, color);
        }
    }
}
