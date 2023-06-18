using System;
using System.IO;

namespace RasterSharp;

public class BitmapRGB
{
    public readonly int Width;
    public readonly int Height;
    public readonly int BytesPerPixel;
    private readonly int StrideWidth;
    private readonly int DataOffset;
    private readonly byte[] Bytes;

    public BitmapRGB(string path)
    {
        Bytes = File.ReadAllBytes(path);

        if (Bytes[0] != 'B' || Bytes[1] != 'M')
            throw new InvalidDataException("invalid magic number");

        UInt32 fileSize = BitConverter.ToUInt32(Bytes, 2);
        if (fileSize != Bytes.Length)
            throw new InvalidDataException("file size mismatch");

        UInt32 offset = BitConverter.ToUInt32(Bytes, 10);
        if (offset != 54)
            throw new InvalidDataException($"Unsupported offset: {offset}");
        DataOffset = (int)offset;

        UInt32 headerSize = BitConverter.ToUInt32(Bytes, 14);
        if (headerSize != 40)
            throw new InvalidDataException($"Unsupported header size: {headerSize}");

        Width = BitConverter.ToUInt16(Bytes, 18);
        Height = BitConverter.ToUInt16(Bytes, 22);
        BytesPerPixel = BitConverter.ToUInt16(Bytes, 28) / 8;
        StrideWidth = 4 * ((Width * BytesPerPixel + 3) / 4);
    }

    private byte GetPixel(int x, int y, int delta)
    {
        long address = DataOffset + StrideWidth * y + x * BytesPerPixel;
        return Bytes[address + delta];
    }

    public byte GetR(int x, int y) => GetPixel(x, y, 2);
    public byte GetG(int x, int y) => GetPixel(x, y, 1);
    public byte GetB(int x, int y) => GetPixel(x, y, 0);
}
