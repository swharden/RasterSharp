using System.Drawing;

namespace RasterSharp.Tests;

internal class DrawingTests
{
    [Test]
    public void Test_DrawLine_Steep()
    {
        Channel image = new(400, 400);

        for (int i = 0; i < image.Width; i += image.Width / 10)
        {
            Point start = new(image.Width / 2, image.Height / 2);
            Point endTop = new(i, 25);
            Point endBottom = new(i, 375);

            int radius = 2;
            Rectangle rectRight = new(
                x: endTop.X - radius,
                y: endTop.Y - radius,
                width: radius * 2,
                height: radius * 2);

            Rectangle rectLeft = new(
                x: endBottom.X - radius,
                y: endBottom.Y - radius,
                width: radius * 2,
                height: radius * 2);

            image.FillRectangle(rectRight, 50);
            image.FillRectangle(rectLeft, 50);
            image.DrawLine(start, endTop, 255);
            image.DrawLine(start, endBottom, 255);
        }

        TestIO.SaveBitmap(image.GetBitmapBytes());
    }

    [Test]
    public void Test_DrawLine_Shallow()
    {
        Channel image = new(400, 400);

        for (int i = 0; i < image.Height; i += image.Height / 10)
        {
            Point start = new(image.Width / 2, image.Height / 2);
            Point endRight = new(375, i);
            Point endLeft = new(25, i);

            int radius = 2;
            Rectangle rectRight = new(
                x: endRight.X - radius,
                y: endRight.Y - radius,
                width: radius * 2,
                height: radius * 2);

            Rectangle rectLeft = new(
                x: endLeft.X - radius,
                y: endLeft.Y - radius,
                width: radius * 2,
                height: radius * 2);

            image.FillRectangle(rectRight, 50);
            image.FillRectangle(rectLeft, 50);
            image.DrawLine(start, endRight, 255);
            image.DrawLine(start, endLeft, 255);
        }

        TestIO.SaveBitmap(image.GetBitmapBytes());
    }

    [Test]
    public void Test_Drawing_RandomLines()
    {
        Random rand = new(0);

        Channel image = new(400, 400);

        Point RandomPoint(int padding = 20)
        {
            int x = rand.Next(padding, image.Width - padding);
            int y = rand.Next(padding, image.Height - padding);
            return new Point(x, y);
        }

        for (int i = 0; i < 20; i++)
        {
            Point pt1 = RandomPoint();
            Point pt2 = RandomPoint();

            int color = rand.Next(50, 255);
            image.FillRectangle(pt1, 5, color);
            image.DrawRectangle(pt2, 5, color);
            image.DrawLine(pt1, pt2, color);
        }

        TestIO.SaveBitmap(image.GetBitmapBytes());
    }
}
