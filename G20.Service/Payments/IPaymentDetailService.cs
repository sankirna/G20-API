using G20.Core.Domain;
using G20.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Service.Payments
{
    public interface IPaymentDetailService
    {
        Task<IPagedList<PaymentDetail>> GetPaymentDetailsAsync(int orderId, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);
        Task<PaymentDetail> GetByIdAsync(int id);
        Task InsertAsync(PaymentDetail entity);
        Task UpdateAsync(PaymentDetail entity);
        Task DeleteAsync(PaymentDetail entity);
    }
}
