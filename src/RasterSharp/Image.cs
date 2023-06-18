using System;

namespace RasterSharp;

/// <summary>
/// Floating-point pixel intensity values over an arbitrary range.
/// </summary>
public class Image
{
    public readonly int Width;
    public readonly int Height;
    private readonly double[] Data;

    public Image(int width, int height)
    {
        Width = width;
        Height = height;
        Data = new double[Width * Height];
    }

    public Image(int width, int height, double[] data)
    {
        Width = width;
        Height = height;
        Data = data;
    }

    public Image Clone()
    {
        double[] data = new double[Data.Length];
        Array.Copy(Data, 0, data, 0, Data.Length);
        return new Image(Width, Height, data);
    }

    public double GetPixel(int x, int y)
    {
        return Data[y * Width + x];
    }

    public double SetPixel(int x, int y)
    {
        return Data[y * Width + x];
    }

    /// <summary>
    /// Adjust contrast (mutating the image) to achieve the given min/max intensity
    /// </summary>
    public void Rescale(double min = 0, double max = 255)
    {
        double originalMin = Data[0];
        double originalMax = Data[0];

        for (int i = 1; i < Data.Length; i++)
        {
            originalMin = Math.Min(originalMin, Data[i]);
            originalMax = Math.Max(originalMax, Data[i]);
        }

        double originalSpan = originalMax - originalMin;
        double newSpan = max - min;

        for (int i = 0; i < Data.Length; i++)
        {
            Data[i] = Data[i] - originalMin; // remove original offset
            Data[i] = Data[i] / originalSpan; // remove original scale (now 0-1)
            Data[i] = Data[i] * newSpan; // add new scale
            Data[i] = Data[i] + min; // add new offset
        }
    }
}
