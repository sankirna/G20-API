using G20.Core;
using G20.Core.Enums;
using G20.Service.QRCodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Nop.Core.Infrastructure;
using Org.BouncyCastle.Utilities;

namespace G20.API.Controllers
{
    public class QRCodeController : BaseController
    {
        protected readonly INopFileProvider _fileService;
        protected readonly INopFileProvider _fileProvider;
        private readonly QRCodeService _qrCodeService;

        public QRCodeController(INopFileProvider fileService,
            INopFileProvider fileProvider,
            QRCodeService qrCodeService)
        {
            _qrCodeService = qrCodeService;
            _fileProvider = fileProvider;
            _fileService = fileService;
        }

        [HttpPost]
        public virtual async Task<IActionResult> Generate(string text)
        {
            var qrCodeImage = _qrCodeService.GenerateQRCode(text);
            using (var stream = new MemoryStream())
            {
                string path = string.Format("{0}{1}", FileTypeEnum.Other.ToGetFolderPath(), Guid.NewGuid() + ".png");

                _fileProvider.CreateFile(path);
                await _fileProvider.WriteAllBytesAsync(path, qrCodeImage);

            }
            return Success("ok");
        }
    }
}
