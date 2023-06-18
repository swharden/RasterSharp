using System;
using System.IO;
using System.Net;

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

    private double[] GetChannelData(int delta)
    {
        double[] data = new double[Width * Height];

        for (int y = 0; y < Height; y++)
        {
            long yOffset = StrideWidth * (Height - 1 - y);
            for (int x = 0; x < Width; x++)
            {
                long xOffset = x * BytesPerPixel;
                long address = DataOffset + xOffset + yOffset + delta;
                data[y * Width + x] = Bytes[address];
            }
        }

        return data;
    }

    public Image GetImageR() => new(Width, Height, GetChannelData(2));
    public Image GetImageG() => new(Width, Height, GetChannelData(1));
    public Image GetImageB() => new(Width, Height, GetChannelData(0));
}
