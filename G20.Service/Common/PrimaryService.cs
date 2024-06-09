﻿using G20.Core;
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

        public List<EnumClass> GetAddressTypes()
        {
            return EnumHelper.ListFor<AddressTypeEnum>().ToList();
        }

        public List<EnumClass> GetOccupationTypes()
        {
            return EnumHelper.ListFor<OccupationTypeEnum>().ToList();
        }

        public List<EnumClass> GetRelationTypes()
        {
            return EnumHelper.ListFor<RelationTypeEnum>().ToList();
        }

        public List<EnumClass> GetRoles()
        {
            return EnumHelper.ListFor<RoleEnum>().ToList();
        }

        public List<EnumClass> GetGenderTypes()
        {
            return EnumHelper.ListFor<GenderTypeEnum>().ToList();
        }

        public List<EnumClass> GetFileTypes()
        {
            return EnumHelper.ListFor<FileTypeEnum>().ToList();
        }

        public List<EnumClass> GetCouponCalculateTypes()
        {
            return EnumHelper.ListFor<CouponCalculateType>().ToList();
        }
    }
}
