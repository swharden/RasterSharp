namespace RasterSharp.Tests;

public class BitmapReadingTests
{
    [Test]
    public void Test_Read_Bytes()
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
        Assert.That(red.GetPixel(0, 0), Is.EqualTo(164));
        Assert.That(green.GetPixel(0, 0), Is.EqualTo(150));
        Assert.That(blue.GetPixel(0, 0), Is.EqualTo(071));

        // bottom left pixel
        Assert.That(red.GetPixel(0, 6), Is.EqualTo(061));
        Assert.That(green.GetPixel(0, 6), Is.EqualTo(038));
        Assert.That(blue.GetPixel(0, 6), Is.EqualTo(023));

        // blue pixel
        Assert.That(red.GetPixel(2, 1), Is.EqualTo(0));
        Assert.That(green.GetPixel(2, 1), Is.EqualTo(0));
        Assert.That(blue.GetPixel(2, 1), Is.EqualTo(255));
    }
}