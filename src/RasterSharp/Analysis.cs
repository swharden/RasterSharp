using System;

namespace RasterSharp;

public static class Analysis
{
    public static (double min, double max) MinMax(Image img)
    {
        (double rMin, double rMax) = Analysis.MinMax(img.Red);
        (double gMin, double gMax) = Analysis.MinMax(img.Green);
        (double bMin, double bMax) = Analysis.MinMax(img.Blue);

        double min = Math.Min(Math.Min(rMin, gMin), bMin);
        double max = Math.Max(Math.Max(rMax, gMax), bMax);

        return (min, max);
    }

    public static (double min, double max) MinMax(Channel img)
    {
        double[] values = img.GetValues();

        double min = values[0];
        double max = values[0];

        for (int i = 1; i < values.Length; i++)
        {
            min = Math.Min(min, values[i]);
            max = Math.Max(max, values[i]);
        }

        return (min, max);
    }

    public static double GetPercentile(Channel img, double percent)
    {
        double fraction = percent / 100;

        if (fraction <= 0)
            return MinMax(img).min;

        if (fraction >= 1)
            return MinMax(img).max;

        double[] values = img.GetValues();
        double[] sorted = new double[values.Length];
        Array.Copy(values, 0, sorted, 0, values.Length);
        Array.Sort(sorted);
        int index = (int)(fraction * sorted.Length);
        return sorted[index];
    }
}
