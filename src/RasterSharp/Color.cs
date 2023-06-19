using System;

namespace RasterSharp;

public static class Color
{
    public static int Mix(int colorA, int colorB, double fractionB = 0.5)
    {
        double fractionA = 1 - fractionB;

        var bytesA = Bytes(colorA);
        var bytesB = Bytes(colorB);

        byte r = (byte)(bytesA.r * fractionA + bytesB.r * fractionB);
        byte g = (byte)(bytesA.g * fractionA + bytesB.g * fractionB);
        byte b = (byte)(bytesA.b * fractionA + bytesB.b * fractionB);
        byte a = (byte)(bytesA.a * fractionA + bytesB.a * fractionB);

        return ToInt(r, g, b, a);
    }

    public static int RandomColor(Random rand, byte min = 100, byte max = 255, byte alpha = 0)
    {
        byte r = (byte)rand.Next(min, max);
        byte g = (byte)rand.Next(min, max);
        byte b = (byte)rand.Next(min, max);
        return ToInt(r, g, b, alpha);
    }

    public static int ToInt(byte r, byte g, byte b, byte a = 0)
    {
        return (r << 24) | (g << 16) | (b << 8) | (a << 0);
    }

    public static (byte r, byte g, byte b, byte a) Bytes(int rgba)
    {
        byte r = (byte)(rgba >> 24);
        byte g = (byte)(rgba >> 16);
        byte b = (byte)(rgba >> 8);
        byte a = (byte)(rgba >> 0);
        return (r, g, b, a);
    }

    public static int Fractional(double r, double g, double b, double a = 0)
    {
        byte r2 = (byte)(r * 255);
        byte g2 = (byte)(g * 255);
        byte b2 = (byte)(b * 255);
        byte a2 = (byte)(a * 255);
        return ToInt(r2, g2, b2, a2);
    }

    public static int Replace(int color, byte? red = null, byte? green = null, byte? blue = null, byte? alpha = null)
    {
        (byte r, byte g, byte b, byte a) = Bytes(color);
        r = red ?? r;
        g = green ?? g;
        b = blue ?? b;
        a = alpha ?? a;
        return ToInt(r, g, b, a);
    }
}
