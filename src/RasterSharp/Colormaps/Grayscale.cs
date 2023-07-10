using System;

namespace RasterSharp.Colormaps;

public class Grayscale : IColormap
{
    public int GetColor(double fraction)
    {
        fraction = Math.Max(0, fraction);
        fraction = Math.Min(1, fraction);
        byte value = (byte)(fraction * 255);
        return Color.FromRGB(value, value, value);
    }
}
