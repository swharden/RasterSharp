namespace RasterSharp.Tests;

public class SampleData
{
    public static string SampleImageFolder => Path.Combine(
        TestContext.CurrentContext.TestDirectory,
        "../../../../../data/images/");

    public static string MandrillBmpPath => Path.Combine(
        SampleImageFolder,
        "mandrill.bmp");

    public static string SmallBmpPath => Path.Combine(
        SampleImageFolder,
        "small.bmp");

    public static string WashedBmpPath => Path.Combine(
        SampleImageFolder,
        "washed.bmp");

    [Test]
    public void Test_SampleDataFiles_AreFound()
    {
        Assert.That(Directory.Exists(SampleImageFolder), Is.True);
        Assert.That(File.Exists(MandrillBmpPath), Is.True);
        Assert.That(File.Exists(SmallBmpPath), Is.True);
        Assert.That(File.Exists(WashedBmpPath), Is.True);
    }
}