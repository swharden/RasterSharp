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

        // top left pixel
        Assert.That(bmp.GetR(0, 6), Is.EqualTo(164));
        Assert.That(bmp.GetG(0, 6), Is.EqualTo(150));
        Assert.That(bmp.GetB(0, 6), Is.EqualTo(071));

        // bottom left pixel
        Assert.That(bmp.GetR(0, 0), Is.EqualTo(061));
        Assert.That(bmp.GetG(0, 0), Is.EqualTo(038));
        Assert.That(bmp.GetB(0, 0), Is.EqualTo(023));

        // blue pixel
        Assert.That(bmp.GetR(2, 5), Is.EqualTo(0));
        Assert.That(bmp.GetG(2, 5), Is.EqualTo(0));
        Assert.That(bmp.GetB(2, 5), Is.EqualTo(255));
    }
}