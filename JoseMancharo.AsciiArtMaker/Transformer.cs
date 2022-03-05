using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Text;

namespace JoseMancharo.AsciiArtMaker;

internal static class Transformer
{
    public static string ToAsciiArt(this Image<Rgba32> image, char[] asciiCharacters)
    {
        var builder = new StringBuilder();
        var isNewLine = false;
        for (var y = 0; y < image.Height; y++)
        {
            for (var x = 0; x < image.Width; x++)
            {
                var pixel = image[x, y];
                var avg = (byte)((pixel.R + pixel.G + pixel.B) / 3);
                var index = (avg * (asciiCharacters.Length - 1)) / 255;
                if (!isNewLine)
                {
                    builder.Append(asciiCharacters[index]);
                }
            }
            if (!isNewLine)
            {
                builder.AppendLine();
                isNewLine = true;
            }
            else
            {
                isNewLine = false;
            }
        }
        return builder.ToString();
    }

    public static string ToAsciiArt(this ImageFrame<Rgba32> image, char[] asciiCharacters)
    {
        var builder = new StringBuilder();
        var isNewLine = false;
        for (var y = 0; y < image.Height; y++)
        {
            for (var x = 0; x < image.Width; x++)
            {
                var pixel = image[x, y];
                var avg = (byte)((pixel.R + pixel.G + pixel.B) / 3);
                var index = (avg * (asciiCharacters.Length - 1)) / 255;
                if (!isNewLine)
                {
                    builder.Append(asciiCharacters[index]);
                }
            }
            if (!isNewLine)
            {
                builder.AppendLine();
                isNewLine = true;
            }
            else
            {
                isNewLine = false;
            }
        }
        return builder.ToString();
    }
}
