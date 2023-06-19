namespace RasterSharp.Tests;

internal class ColorConverterTests
{
    [Test]
    public void Test_Color_RGBA()
    {
        Random rand = new(0);

        for (int i = 0; i < 100; i++)
        {
            int original = rand.Next();
            (byte r, byte g, byte b, byte a) = Color.Bytes(original);
            int returned = Color.ToInt(r, g, b, a);
            Assert.That(returned, Is.EqualTo(original));
        }
    }
}
