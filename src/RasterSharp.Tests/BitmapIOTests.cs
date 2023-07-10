namespace RasterSharp.Tests;

public class BitmapIOTests
{
    [Test]
    public void Test_Read_Bitmap()
    {
        Image image = new(SampleData.SmallBmpPath);

        Assert.That(image.Width, Is.EqualTo(13));
        Assert.That(image.Height, Is.EqualTo(7));

        // top left pixel
        Assert.That(image.Red.GetValue(0, 0), Is.EqualTo(164));
        Assert.That(image.Green.GetValue(0, 0), Is.EqualTo(150));
        Assert.That(image.Blue.GetValue(0, 0), Is.EqualTo(071));

        // bottom left pixel
        Assert.That(image.Red.GetValue(0, 6), Is.EqualTo(061));
        Assert.That(image.Green.GetValue(0, 6), Is.EqualTo(038));
        Assert.That(image.Blue.GetValue(0, 6), Is.EqualTo(023));

        // blue pixel
        Assert.That(image.Red.GetValue(2, 1), Is.EqualTo(0));
        Assert.That(image.Green.GetValue(2, 1), Is.EqualTo(0));
        Assert.That(image.Blue.GetValue(2, 1), Is.EqualTo(255));
    }

    [Test]
    public void Test_Write_Bitmap()
    {
        string outputFolder = Path.GetFullPath("./");
        Console.WriteLine(outputFolder);

        Image image = new(SampleData.SmallBmpPath);
        image.Red.Save(Path.Join(outputFolder, "save-red.bmp"));
        image.Green.Save(Path.Join(outputFolder, "save-green.bmp"));
        image.Blue.Save(Path.Join(outputFolder, "save-blue.bmp"));
        image.SaveBmp(Path.Join(outputFolder, "save-rgb.bmp"));

        Image image2 = new(Path.Join(outputFolder, "save-rgb.bmp"));
        for (int y = 0; y < image.Height; y++)
        {
            for (int x = 0; x < image.Width; x++)
            {
                int original = image.GetRGBA(x, y);
                int saved = image2.GetRGBA(x, y);
                Assert.That(original, Is.EqualTo(saved));
            }
        }
    }
}