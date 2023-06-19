using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RasterSharp.Tests;
internal class ImageOperationTests
{
    [Test]
    public void Test_Crop_Values()
    {
        Image img = new(SampleData.MandrillBmpPath);
        Rectangle rect = new(136, 66, 110, 55);
        Image img2 = ImageOperations.Crop(img, rect);
        Assert.That(img2.Width, Is.EqualTo(rect.Width));
        Assert.That(img2.Height, Is.EqualTo(rect.Height));

        // verified with ImageJ
        Assert.That(img2.Red.GetValue(7, 3), Is.EqualTo(58));
        Assert.That(img2.Green.GetValue(7, 3), Is.EqualTo(59));
        Assert.That(img2.Blue.GetValue(7, 3), Is.EqualTo(47));
    }

    [Test]
    public void Test_Resize_Smaller()
    {
        Image img = new(SampleData.MandrillBmpPath);
        Image img2 = ImageOperations.Resize(img, 100, 200);
        TestIO.SaveBitmap(img2.GetBitmapBytes());
    }

    [Test]
    public void Test_Resize_Larger()
    {
        Image img = new(SampleData.SmallBmpPath);
        Image img2 = ImageOperations.Resize(img, 300, 200);
        TestIO.SaveBitmap(img2.GetBitmapBytes());
    }
}
