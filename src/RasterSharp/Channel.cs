using System;
using System.Drawing;

namespace RasterSharp;

/// <summary>
/// Floating-point pixel intensity values over an arbitrary range
/// representing a single color channel of an image.
/// </summary>
public class Channel
{
    public readonly int Width;
    public readonly int Height;
    private readonly double[] Values;

    public Channel(int width, int height)
    {
        Width = width;
        Height = height;
        Values = new double[Width * Height];
    }

    public Channel(int width, int height, double[] data)
    {
        Width = width;
        Height = height;
        Values = data;
    }

    public Channel Clone()
    {
        double[] data = new double[Values.Length];
        Array.Copy(Values, 0, data, 0, Values.Length);
        return new Channel(Width, Height, data);
    }

    public double GetValue(int x, int y)
    {
        return Values[y * Width + x];
    }

    public double[] GetValues()
    {
        return Values;
    }

    public byte GetByte(int x, int y)
    {
        double value = GetValue(x, y);
        if (value <= 0)
            return 0;
        else if (value >= 255)
            return 255;
        else
            return (byte)value;
    }

    public void SetValue(int x, int y, double value)
    {
        int address = y * Width + x;
        if (address >= Values.Length || address < 0)
            return;
        Values[address] = value;
    }

    /// <summary>
    /// Adjust contrast (mutating the image) to achieve the given min/max intensity
    /// </summary>
    public void Rescale(double min = 0, double max = 255)
    {
        double originalMin = Values[0];
        double originalMax = Values[0];

        for (int i = 1; i < Values.Length; i++)
        {
            originalMin = Math.Min(originalMin, Values[i]);
            originalMax = Math.Max(originalMax, Values[i]);
        }

        double originalSpan = originalMax - originalMin;
        double newSpan = max - min;

        for (int i = 0; i < Values.Length; i++)
        {
            Values[i] = Values[i] - originalMin; // remove original offset
            Values[i] = Values[i] / originalSpan; // remove original scale (now 0-1)
            Values[i] = Values[i] * newSpan; // add new scale
            Values[i] = Values[i] + min; // add new offset
        }
    }

    public byte[] GetBitmapBytes()
    {
        IColormap cmap = new Colormaps.Grayscale();
        return GetBitmapBytes(cmap);
    }

    public byte[] GetBitmapBytes(IColormap cmap)
    {
        return BitmapIO.GetBitmapBytes(this, cmap);
    }

    public void Save(string path)
    {
        IColormap cmap = new Colormaps.Grayscale();
        Save(path, cmap);
    }

    public void Save(string path, IColormap cmap)
    {
        if (!path.EndsWith(".bmp", StringComparison.InvariantCultureIgnoreCase))
            throw new InvalidOperationException("save filename must end with .bmp");

        System.IO.File.WriteAllBytes(path, GetBitmapBytes());
    }

    public void FillRectangle(Rectangle rect, double value)
    {
        for (int x = rect.Left; x <= rect.Right; x++)
        {
            for (int y = rect.Top; y <= rect.Bottom; y++)
            {
                SetValue(x, y, value);
            }
        }
    }

    public void DrawRectangle(Rectangle rect, double value)
    {
        Point topLeft = new(rect.Left, rect.Top);
        Point topRight = new(rect.Right, rect.Top);
        Point bottomLeft = new(rect.Left, rect.Bottom);
        Point bottomRight = new(rect.Right, rect.Bottom);

        DrawLine(topLeft, topRight, value);
        DrawLine(topRight, bottomRight, value);
        DrawLine(bottomRight, bottomLeft, value);
        DrawLine(bottomLeft, topLeft, value);
    }

    public void DrawLine(Point pt1, Point pt2, double value)
    {
        int xSpan = Math.Abs(pt2.X - pt1.X);
        int ySpan = Math.Abs(pt2.Y - pt1.Y);

        if (xSpan > ySpan)
            DrawLineShallow(pt1, pt2, value);
        else
            DrawLineSteep(pt1, pt2, value);
    }

    private void DrawLineShallow(Point pt1, Point pt2, double value)
    {
        if (pt1.X > pt2.X)
            (pt1, pt2) = (pt2, pt1);

        int xSpan = pt2.X - pt1.X;
        int ySpan = pt2.Y - pt1.Y;
        double slope = (double)ySpan / xSpan;

        double dy = 0;
        for (int dx = 0; dx < xSpan; dx++)
        {
            int x = pt1.X + dx;
            int y = (int)(pt1.Y + dy);
            dy += slope;
            SetValue(x, y, value);
        }
    }

    private void DrawLineSteep(Point pt1, Point pt2, double value)
    {
        if (pt1.Y > pt2.Y)
            (pt1, pt2) = (pt2, pt1);

        int xSpan = pt2.X - pt1.X;
        int ySpan = pt2.Y - pt1.Y;
        double changePerPixel = (double)xSpan / ySpan;

        double dx = 0;
        for (int dy = 0; dy < ySpan; dy++)
        {
            int y = pt1.Y + dy;
            int x = (int)(pt1.X + dx);
            dx += changePerPixel;
            SetValue(x, y, value);
        }
    }
}