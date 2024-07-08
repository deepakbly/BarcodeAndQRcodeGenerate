using SkiaSharp;
using ZXing;
using ZXing.Common;

namespace BarcodeAndQRcodeGenerate.Services
{
    public class BarcodeService : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Initialization code here
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Cleanup code here
            return Task.CompletedTask;
        }

        public byte[] GenerateBarcode(string text)
        {
            var writer = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    Width = 300,
                    Height = 50
                }
            };

            var pixelData = writer.Write(text);

            using (var bitmap = new SKBitmap(pixelData.Width, pixelData.Height))
            {
                // Set each pixel in the SKBitmap
                for (int y = 0; y < pixelData.Height; y++)
                {
                    for (int x = 0; x < pixelData.Width; x++)
                    {
                        var pixelIndex = (y * pixelData.Width + x) * 4;
                        var red = pixelData.Pixels[pixelIndex];
                        var green = pixelData.Pixels[pixelIndex + 1];
                        var blue = pixelData.Pixels[pixelIndex + 2];
                        var alpha = pixelData.Pixels[pixelIndex + 3];

                        var color = new SKColor(red, green, blue, alpha);
                        bitmap.SetPixel(x, y, color);
                    }
                }

                using (var image = SKImage.FromBitmap(bitmap))
                using (var stream = new System.IO.MemoryStream())
                {
                    image.Encode(SKEncodedImageFormat.Png, 100).SaveTo(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}
