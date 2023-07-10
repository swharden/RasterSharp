using System;
using System.Drawing;

namespace RasterSharp;

public static class TestPattern
{
    public static void DrawLines(Image img, int count = 100)
    {
        Random rand = new(0);
        for (int i = 0; i < count; i++)
        {
            int x1 = rand.Next(img.Width);
            int y1 = rand.Next(img.Height);
            int x2 = rand.Next(img.Width);
            int y2 = rand.Next(img.Height);
            int color = Color.RandomColor(rand);
            img.DrawLine(x1, y1, x2, y2, color);
        }
    }

    public static void DrawRectangles(Image img, int count = 100)
    {
        Random rand = new(0);
        for (int i = 0; i < count; i++)
        {
            int width = rand.Next(100);
            int height = rand.Next(100);
            int x = rand.Next(img.Width - width);
            int y = rand.Next(img.Height - height);
            int color = Color.RandomColor(rand);
            img.DrawRectangle(x, y, width, height, color);
        }
    }

    public static void FillRectangles(Image img, int count = 100)
    {
        Random rand = new(0);
        for (int i = 0; i < count; i++)
        {
            int width = rand.Next(100);
            int height = rand.Next(100);
            int x = rand.Next(img.Width - width);
            int y = rand.Next(img.Height - height);
            int color = Color.RandomColor(rand);
            img.FillRectangle(x, y, width, height, color);
        }
    }
}
