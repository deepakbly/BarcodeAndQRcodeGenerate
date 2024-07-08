using QRCoder;
using System.Drawing;
using System.IO;
using ZXing.QrCode.Internal;

namespace BarcodeAndQRcodeGenerate.Services
{
    public class QRCodeService : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Code to start the service if needed
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Code to stop the service if needed
            return Task.CompletedTask;
        }
        public byte[] GenerateQRCode(string text)
        {
            using (var qrGenerator = new QRCodeGenerator())
            {
                var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
                using (var qrCode = new PngByteQRCode(qrCodeData))
                {
                    return qrCode.GetGraphic(5);
                }
            }
        }

    }
}
