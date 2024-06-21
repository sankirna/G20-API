using Newtonsoft.Json;
using QRCoder;

namespace G20.Service.QRCodes
{
    public class QRCoderService: IQRCodeService
    {
        public QRCoderService() { }

        public virtual async Task<byte[]> GenerateQRCode(string text)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q))
            using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
            {
                return qrCode.GetGraphic(20);
            }
        }

        public virtual async Task<byte[]> GenerateQRCode<T>(T data)
        {
            return await GenerateQRCode(JsonConvert.SerializeObject(data));
        }

        public virtual async Task<T> GetData<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
