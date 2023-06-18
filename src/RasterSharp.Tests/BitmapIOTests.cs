namespace RasterSharp.Tests;

public class BitmapIOTests
{
    [Test]
    public void Test_Read_Bitmap()
    {
        BitmapRGB bmp = new(SampleData.SmallBmpPath);
        Assert.That(bmp.Width, Is.EqualTo(13));
        Assert.That(bmp.Height, Is.EqualTo(7));
        Assert.That(bmp.BytesPerPixel, Is.EqualTo(3));

        Image red = bmp.GetImageR();
        Image green = bmp.GetImageG();
        Image blue = bmp.GetImageB();

        Assert.That(red.Width, Is.EqualTo(bmp.Width));
        Assert.That(red.Height, Is.EqualTo(bmp.Height));

        // top left pixel
        Assert.That(red.GetValue(0, 0), Is.EqualTo(164));
        Assert.That(green.GetValue(0, 0), Is.EqualTo(150));
        Assert.That(blue.GetValue(0, 0), Is.EqualTo(071));

        // bottom left pixel
        Assert.That(red.GetValue(0, 6), Is.EqualTo(061));
        Assert.That(green.GetValue(0, 6), Is.EqualTo(038));
        Assert.That(blue.GetValue(0, 6), Is.EqualTo(023));

        // blue pixel
        Assert.That(red.GetValue(2, 1), Is.EqualTo(0));
        Assert.That(green.GetValue(2, 1), Is.EqualTo(0));
        Assert.That(blue.GetValue(2, 1), Is.EqualTo(255));
    }

    [Test]
    public void Test_Write_Bitmap()
    {
        BitmapRGB bmp = new(SampleData.SmallBmpPath);
        Image red = bmp.GetImageR();
        Image green = bmp.GetImageG();
        Image blue = bmp.GetImageB();

        string outputFolder = Path.GetFullPath("./");
        Console.WriteLine(outputFolder);
        red.Save(Path.Join(outputFolder, "save-red.bmp"));
        green.Save(Path.Join(outputFolder, "save-green.bmp"));
        blue.Save(Path.Join(outputFolder, "save-blue.bmp"));
    }
}