namespace RasterSharp.Tests;

internal class ChannelTests
{
    [Test]
    public void Test_Channel_Colormap()
    {
        Image image = new(SampleData.SmallBmpPath);
        Channel green = image.Green;

        IColormap cmap = new Colormaps.Viridis();
        byte[] bytes = green.GetBitmapBytes(cmap);
        TestIO.SaveBitmap(bytes);
    }

    [Test]
    public void Test_Crop_HasExpectedValues()
    {
        Image img = new(SampleData.MandrillBmpPath);
        Image cropped = img.Crop(129, 68, 112, 56);
        TestIO.SaveBitmap(cropped.GetBitmapBytes());

        Assert.That(cropped.Width, Is.EqualTo(112));
        Assert.That(cropped.Height, Is.EqualTo(56));
    }
}
