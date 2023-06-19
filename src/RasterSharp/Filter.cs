using System;

namespace RasterSharp;

public static class Filter
{
    public static void AutoContrast(Image img)
    {
        (double min, double max) = Analysis.MinMax(img);

        WindowContrast(img.Red, min, max);
        WindowContrast(img.Green, min, max);
        WindowContrast(img.Blue, min, max);
    }

    public static void AutoContrastEachChannel(Image img)
    {
        AutoContrast(img.Red);
        AutoContrast(img.Green);
        AutoContrast(img.Blue);
    }

    public static void WindowContrast(Channel ch, double lower, double upper, double newMax = 255)
    {
        double[] values = ch.GetValues();

        double span = upper - lower;

        for (int i = 0; i < values.Length; i++)
        {
            values[i] -= lower;
            values[i] /= span;
            values[i] *= newMax;
        }
    }

    public static void AutoContrast(Channel ch, double newMax = 255)
    {
        double[] values = ch.GetValues();

        (double min, double max) = Analysis.MinMax(ch);
        double span = max - min;

        for (int i = 0; i < values.Length; i++)
        {
            values[i] -= min;
            values[i] /= span;
            values[i] *= newMax;
        }
    }

    public static void Add(Channel ch, double value)
    {
        double[] values = ch.GetValues();
        for (int i = 0; i < values.Length; i++)
            values[i] += value;
    }

    public static void Subtract(Channel ch, double value)
    {
        double[] values = ch.GetValues();
        for (int i = 0; i < values.Length; i++)
            values[i] -= value;
    }

    public static void Multiply(Channel ch, double value)
    {
        double[] values = ch.GetValues();
        for (int i = 0; i < values.Length; i++)
            values[i] *= value;
    }

    public static void Divide(Channel ch, double value)
    {
        double[] values = ch.GetValues();
        for (int i = 0; i < values.Length; i++)
            values[i] /= value;
    }

    public static void Sqrt(Channel ch)
    {
        double[] values = ch.GetValues();
        for (int i = 0; i < values.Length; i++)
            values[i] = Math.Sqrt(values[i]);
    }
}
