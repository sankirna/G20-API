using G20.Core;
using G20.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Service.Common
{
    public class PrimaryService: IPrimaryService
    {
        public PrimaryService() { }


        public List<EnumClass> GetRoles()
        {
            return EnumHelper.ListFor<RoleEnum>().ToList();
        }

        public List<EnumClass> GetFileTypes()
        {
            return EnumHelper.ListFor<FileTypeEnum>().ToList();
        }

        public List<EnumClass> GetCouponCalculateTypes()
        {
            return EnumHelper.ListFor<CouponCalculateType>().ToList();
        }

        public List<EnumClass> GetProductTypes()
        {
            return EnumHelper.ListFor<ProductTypeEnum>().ToList();
        }

        public List<EnumClass> GetOrderStatuses()
        {
            return EnumHelper.ListFor<OrderStatusEnum>().ToList();
        }
    }
}
