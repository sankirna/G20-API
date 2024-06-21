using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Service.QRCodes
{
    public interface IQRCodeService
    {
        Task<byte[]> GenerateQRCode(string text);
        Task<byte[]> GenerateQRCode<T>(T data);
        Task<T> GetData<T>(string data);
    }
}
