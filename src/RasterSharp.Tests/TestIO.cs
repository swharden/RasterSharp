using System.Diagnostics;
using System.Reflection;

namespace RasterSharp.Tests;

internal static class TestIO
{
    public static void SaveBitmap(byte[] bytes)
    {
        StackTrace stackTrace = new();

        StackFrame frame = stackTrace.GetFrame(1)
            ?? throw new InvalidOperationException("bad caller");

        MethodBase method = frame.GetMethod()
            ?? throw new InvalidDataException("bad method");

        string callingMethod = method.Name;

        string saveAs = Path.GetFullPath(
            Path.Combine("./",
            callingMethod + ".bmp"));

        File.WriteAllBytes(saveAs, bytes);
        Console.WriteLine(saveAs);
    }
}
