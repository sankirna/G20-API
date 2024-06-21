using G20.Core.Domain;
using G20.Core;
using G20.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G20.Service.Payments
{
    public class PaymentDetailService : IPaymentDetailService
    {
        protected readonly IRepository<PaymentDetail> _entityRepository;

        public PaymentDetailService(IRepository<PaymentDetail> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public virtual async Task<IPagedList<PaymentDetail>> GetPaymentDetailsAsync(int orderId, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var paymentDetails = await _entityRepository.GetAllPagedAsync(query =>
            {
                return query;
            }, pageIndex, pageSize, getOnlyTotalCount, includeDeleted: false);

            return paymentDetails;
        }

        public virtual async Task<PaymentDetail> GetByIdAsync(int id)
        {
            return await _entityRepository.GetByIdAsync(id);
        }

        public virtual async Task InsertAsync(PaymentDetail entity)
        {
            await _entityRepository.InsertAsync(entity);
        }

        public virtual async Task UpdateAsync(PaymentDetail entity)
        {
            await _entityRepository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(PaymentDetail entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _entityRepository.DeleteAsync(entity);
        }
    }
}
