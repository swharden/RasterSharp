namespace RasterSharp;

public static class ColorConverter
{
    public static int ToRGBA(byte r, byte g, byte b, byte a)
    {
        return (r << 24) | (g << 16) | (b << 8) | (a << 0);
    }

    public static (byte r, byte g, byte b, byte a) FromRGBA(int rgba)
    {
        byte r = (byte)(rgba >> 24);
        byte g = (byte)(rgba >> 16);
        byte b = (byte)(rgba >> 8);
        byte a = (byte)(rgba >> 0);
        return (r, g, b, a);
    }
}
