using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Text;

namespace JoseMancharo.AsciiArtMaker;
static class Program
{
    /// <summary> Generates ASCII art from an image </summary>
    /// <param name="file">path to image to convert</param>
    /// <param name="lightMode">should characters be inverted to support light-mode</param>
    /// <param name="asciiCharacters">An array of ascii characters to build the art out of</param>
    /// <param name="scale">The scale to resize the image to. Image resizes to 1/scale</param>
    static void Main(FileInfo file, string asciiCharacters = " .,`\"*:;!|+=%@780#", bool lightMode = false, int scale = 225)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        var asciiArray = asciiCharacters.ToArray();
        if (lightMode)
        {
            asciiArray = asciiCharacters.Reverse().ToArray();
        }

        using var image = Image.Load<Rgba32>(file.FullName);
        var newHeight = (int)Math.Round(image.Height / (image.Width / (scale * 1f)));
        image.Mutate(x => x.Resize(scale, newHeight));

        if (image.Frames.Count > 1)
        {
            var ascii_frames = new List<string>();
            for (var i = 0; i < image.Frames.Count; i++)
            {
                ascii_frames.Add(image.Frames[i].ToAsciiArt(asciiArray));
            }

            using var outputStream = Console.OpenStandardOutput();
            var emptyFrame = string.Join('\n', Enumerable.Range(0, newHeight).Select(_ => ""));
            while (true)
            {
                foreach (var frame in ascii_frames)
                {
                    new MemoryStream(Encoding.UTF8.GetBytes(emptyFrame + frame)).CopyTo(outputStream);
                    Thread.Sleep(10);

                }
            }
        }
        else
        {
            Console.WriteLine(image.ToAsciiArt(asciiArray));
        }

    }
}

