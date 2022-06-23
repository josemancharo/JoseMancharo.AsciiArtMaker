using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Security.Cryptography;
using System.Text;

const int size = 3;

using var image = new Image<Rgba32>(size, size);

var input = string.Join("", args);

var hash = SHA256.HashData(Encoding.UTF8.GetBytes(input));

if (size > hash.Length - 3)
{
    throw new Exception($"{nameof(size)} is too large to produce identicon image");
}

var firstBlock = hash[0..(size * size - 1)];

var colors = hash[(size * size - 1)..].Take(3).ToArray();

var red = colors[0];
var green = colors[1];
var blue = colors[2];
var color = new Rgba32(red, green, blue);

for (var x = 0; x < image.Width; x++)
{
    for (var y = 0; y < image.Height; y++)
    {
        var byteValue = firstBlock[(firstBlock.Length - 1) % x + y];
        image[x, y] =
            byteValue > byte.MaxValue/2 ? color : new Rgba32(255, 255, 255);
    }
}

await image.SaveAsPngAsync(@$"C:\Users\ipettingill\Downloads\{Encoding.UTF8.GetString(hash)}.png");
