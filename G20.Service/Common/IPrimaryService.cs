using G20.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Service.Common
{
    public interface IPrimaryService
    {
        List<EnumClass> GetRoles();
        List<EnumClass> GetFileTypes();
        List<EnumClass> GetCouponCalculateTypes();
        List<EnumClass> GetProductTypes();
        List<EnumClass> GetOrderStatuses();


    }
}
