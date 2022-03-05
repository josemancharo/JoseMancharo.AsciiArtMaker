using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using System.Text;
using System.Linq;
using JoseMancharo.AsciiArtMaker;

var asciiCharacters = new[] { '#', '0', '8', '7', '@', '%', '=', '+', '|', '!', ';', ':', '*', '"', '`', ',', '.', ' ' }.Reverse().ToArray();

Console.WriteLine("input path to file: ");
var file = Console.ReadLine();

using var image = Image.Load<Rgba32>(file);
var newHeight = (int)Math.Round(image.Height / (image.Width / 225f));
image.Mutate(x => x.Resize(225, newHeight));

if (image.Frames.Count > 1)
{
    var ascii_frames = new List<string>();
    for (var i = 0; i<image.Frames.Count; i++)
    {
        ascii_frames.Add(image.Frames[i].ToAsciiArt(asciiCharacters));
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
    Console.WriteLine(image.ToAsciiArt(asciiCharacters));
}
