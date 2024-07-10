using G20.API.Models.Coupons;
using G20.API.Models.Users;
using G20.Core;
using G20.Core.Domain;
using G20.Core.Enums;
using G20.Framework.Models;

namespace G20.API.Models.Orders
{
    public partial record OrderDetailModel : BaseNopEntityModel
    {
        public int UserId { get; set; }
        public string? CouponCode { get; set; }
        public int? CouponId { get; set; }
        public decimal GrossTotal { get; set; }
        public decimal? Discount { get; set; }
        public decimal GrandTotal { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int? PaymentId { get; set; }
        public int OrderStatusId { get; set; }
        public string POSTransactionId { get; set; }
        public string OrderStatus
        {
            get
            {
                return EnumHelper.GetDescription((OrderStatusEnum)OrderStatusId);
            }
        }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }

        public UserModel UserDetail { get; set; }
        public CouponModel CouponDetail { get; set; }
        public List<OrderProductItemModel> Items { get; set; }
        public int PaymentTypeId { get; set; }
        public string PaymnetType
        {
            get
            {
                return EnumHelper.GetDescription((PaymentTypeEnum)PaymentTypeId);
            }
        }
    }
}
