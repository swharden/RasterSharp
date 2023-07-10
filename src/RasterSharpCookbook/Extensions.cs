using System.Diagnostics;
using System.Reflection;

namespace RasterSharpCookbook;

internal static class Extensions
{
    public static void SaveCookbookImage(this RasterSharp.Image img)
    {
        // determine filename based on name of calling function
        StackTrace stackTrace = new();
        StackFrame frame = stackTrace.GetFrame(1) ?? throw new InvalidOperationException("bad caller");
        MethodBase method = frame.GetMethod() ?? throw new InvalidDataException("bad method");
        string callingMethod = method.Name;

        string saveAs = Path.GetFullPath($"test-{callingMethod}.bmp");
        Console.WriteLine(saveAs);
        img.SaveBmp(saveAs);
    }
}
