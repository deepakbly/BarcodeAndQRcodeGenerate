using BarcodeAndQRcodeGenerate.Services;
using Microsoft.AspNetCore.Mvc;

namespace BarcodeAndQRcodeGenerate.Controllers
{
    public class BarcodeController : Controller
    {
        private readonly BarcodeService _barcodeService;
        private readonly QRCodeService _qrcodeService;

        public BarcodeController(BarcodeService barcodeService , QRCodeService qrcodeService)
        {
            _barcodeService = barcodeService;
            _qrcodeService = qrcodeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GenerateBarcode(string text)
        {
            var barcode = _barcodeService.GenerateBarcode(text);
            var barcodeBase64 = Convert.ToBase64String(barcode);
            ViewBag.BarcodeImage = barcodeBase64;
            return View("Index");
        }

        [HttpPost]
        public IActionResult GenerateQRCode(string text)
        {
            var qrCode = _qrcodeService.GenerateQRCode(text);
            var qrCodeBase64 = Convert.ToBase64String(qrCode);
            ViewBag.QRCodeImage = qrCodeBase64;
            return View("Index");
        }

    }
}
