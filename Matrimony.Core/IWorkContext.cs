using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrimony.Core
{
    public partial interface IWorkContext
    {
        void SetCurrentUserId(int userId);
        int GetCurrentUserId();
    }
}
