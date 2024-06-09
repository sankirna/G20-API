using Matrimony.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Service.Common
{
    public interface IPrimaryService
    {
        List<EnumClass> GetAddressTypes();
        List<EnumClass> GetOccupationTypes();
        List<EnumClass> GetRelationTypes();
        List<EnumClass> GetRoles();
        List<EnumClass> GetGenderTypes();
        List<EnumClass> GetFileTypes();


    }
}
