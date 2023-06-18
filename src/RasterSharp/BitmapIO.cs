using System;

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

                byte value = img.GetByte(x, y);
                pixelData[address + 0] = value; // B
                pixelData[address + 1] = value; // G
                pixelData[address + 2] = value; // R
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
}
