using System;
using System.IO;

namespace RasterSharp;

internal static class BitmapIO
{
    public static byte[] GetBitmapBytes(Image img)
    {
        int bytesPerPixel = 4;
        int strideWidth = 4 * ((img.Width * bytesPerPixel + 3) / 4);

        byte[] pixelData = new byte[strideWidth * img.Height];
        for (int y = 0; y < img.Height; y++)
        {
            for (int x = 0; x < img.Width; x++)
            {
                int address = (img.Height - 1 - y) * strideWidth + x * bytesPerPixel;

                pixelData[address + 0] = img.Blue.GetByte(x, y);
                pixelData[address + 1] = img.Green.GetByte(x, y);
                pixelData[address + 2] = img.Red.GetByte(x, y);
                pixelData[address + 3] = img.Alpha.GetByte(x, y);
            }
        }

        return GetBitmapBytes(img.Width, img.Height, pixelData);
    }

    public static byte[] GetBitmapBytes(Channel img, IColormap cmap)
    {

        int bytesPerPixel = 4;
        int strideWidth = 4 * ((img.Width * bytesPerPixel + 3) / 4);

        byte[] pixelData = new byte[strideWidth * img.Height];
        for (int y = 0; y < img.Height; y++)
        {
            for (int x = 0; x < img.Width; x++)
            {
                int address = (img.Height - 1 - y) * strideWidth + x * bytesPerPixel;

                double fraction = img.GetValue(x, y) / 255;
                int rgba = cmap.GetColor(fraction);
                (byte r, byte g, byte b, byte a) = ColorConverter.FromRGBA(rgba);
                pixelData[address + 0] = b; // B
                pixelData[address + 1] = g; // G
                pixelData[address + 2] = r; // R
                pixelData[address + 3] = a; // A
            }
        }

        return GetBitmapBytes(img.Width, img.Height, pixelData);
    }

    public static byte[] GetBitmapBytes(int width, int height, byte[] pixelData)
    {
        const int imageHeaderSize = 54;
        byte[] bmpBytes = new byte[pixelData.Length + imageHeaderSize];
        bmpBytes[0] = (byte)'B';
        bmpBytes[1] = (byte)'M';
        bmpBytes[14] = 40;
        Array.Copy(BitConverter.GetBytes(bmpBytes.Length), 0, bmpBytes, 2, 4);
        Array.Copy(BitConverter.GetBytes(imageHeaderSize), 0, bmpBytes, 10, 4);
        Array.Copy(BitConverter.GetBytes(width), 0, bmpBytes, 18, 4);
        Array.Copy(BitConverter.GetBytes(height), 0, bmpBytes, 22, 4);
        Array.Copy(BitConverter.GetBytes(32), 0, bmpBytes, 28, 2);
        Array.Copy(BitConverter.GetBytes(pixelData.Length), 0, bmpBytes, 34, 4);
        Array.Copy(pixelData, 0, bmpBytes, imageHeaderSize, pixelData.Length);
        return bmpBytes;
    }

    public static Image FromBytes(byte[] Bytes)
    {
        if (Bytes[0] != 'B' || Bytes[1] != 'M')
            throw new InvalidDataException("invalid magic number");

        UInt32 fileSize = BitConverter.ToUInt32(Bytes, 2);
        if (fileSize != Bytes.Length)
            throw new InvalidDataException("file size mismatch");

        UInt32 offset = BitConverter.ToUInt32(Bytes, 10);
        if (offset != 54)
            throw new InvalidDataException($"Unsupported offset: {offset}");
        int DataOffset = (int)offset;

        UInt32 headerSize = BitConverter.ToUInt32(Bytes, 14);
        if (headerSize != 40)
            throw new InvalidDataException($"Unsupported header size: {headerSize}");

        int Width = BitConverter.ToUInt16(Bytes, 18);
        int Height = BitConverter.ToUInt16(Bytes, 22);
        int BytesPerPixel = BitConverter.ToUInt16(Bytes, 28) / 8;

        int StrideWidth = 4 * ((Width * BytesPerPixel + 3) / 4);

        Channel red = new(Width, Height);
        Channel green = new(Width, Height);
        Channel blue = new(Width, Height);

        for (int y = 0; y < Height; y++)
        {
            long yOffset = StrideWidth * (Height - 1 - y);
            for (int x = 0; x < Width; x++)
            {
                long address = DataOffset + yOffset + x * BytesPerPixel;
                red.SetPixel(x, y, Bytes[address + 2]);
                green.SetPixel(x, y, Bytes[address + 1]);
                blue.SetPixel(x, y, Bytes[address + 0]);
            }
        }

        return new Image(red, green, blue);
    }
}
