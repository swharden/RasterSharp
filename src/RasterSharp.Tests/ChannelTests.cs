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
}
