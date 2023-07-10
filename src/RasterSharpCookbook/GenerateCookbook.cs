namespace RasterSharpCookbook;

public class GenerateCookbook
{
    [Test]
    public void Recipe_Quickstart()
    {
        // Create an image from scratch
        RasterSharp.Image img = new(400, 300);

        // Fill the whole image with a solid color
        img.Fill(RasterSharp.Colors.Navy);

        // Draw some shapes
        img.DrawLine(50, 50, 300, 200, RasterSharp.Colors.White);
        img.DrawRectangle(100, 50, 150, 75, RasterSharp.Colors.Yellow);
        img.FillRectangle(75, 200, 100, 50, RasterSharp.Colors.Magenta);

        // Save the image as a bitmap
        img.SaveCookbookImage();
    }

    [Test]
    public void Recipe_TestPattern_Lines()
    {
        RasterSharp.Image img = new(400, 300);
        img.Fill(RasterSharp.Colors.Navy);

        RasterSharp.TestPattern.DrawLines(img);

        img.SaveCookbookImage();
    }

    [Test]
    public void Recipe_TestPattern_DrawRectangle()
    {
        RasterSharp.Image img = new(400, 300);
        img.Fill(RasterSharp.Colors.Navy);

        RasterSharp.TestPattern.DrawRectangles(img);

        img.SaveCookbookImage();
    }

    [Test]
    public void Recipe_TestPattern_FillRectangle()
    {
        RasterSharp.Image img = new(400, 300);
        img.Fill(RasterSharp.Colors.Navy);

        RasterSharp.TestPattern.FillRectangles(img);

        img.SaveCookbookImage();
    }
}