namespace RasterSharp;

public interface IColormap
{
    /// <summary>
    /// Return the color from the colormap in the range [0, 1]
    /// </summary>
    int GetColor(double fraction);
}
