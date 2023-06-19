namespace RasterSharp.Tests;

internal class ColorsTests
{
    public void Test_Colors_CanBeLookedUp()
    {
        (byte r, byte g, byte b, byte a) = Color.Bytes(Colors.CornflowerBlue);
        Assert.That(r, Is.EqualTo(100));
        Assert.That(g, Is.EqualTo(149));
        Assert.That(b, Is.EqualTo(237));
        Assert.That(a, Is.EqualTo(0));
    }
}
