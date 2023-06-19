namespace RasterSharp.Tests;

internal class DrawingTests
{
    [Test]
    public void Test_Draw_Line()
    {
        Image image = new(50, 40);

        image.Green.DrawLine(10, 20, 15, 35, 255);
        image.Green.DrawLine(10, 20, 15, 05, 128); // TODO: not workig right

        TestIO.SaveBitmap(image.Green.GetBitmapBytes());
    }
}
