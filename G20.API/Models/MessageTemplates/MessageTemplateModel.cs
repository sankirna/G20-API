using G20.Core.Enums;
using G20.Framework.Models;

namespace G20.API.Models.MessageTemplates
{
    public partial record MessageTemplateModel : BaseNopEntityModel
    {
        public string Name { get; set; }
        public string BccEmailAddresses { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsActive { get; set; }
        public int? DelayBeforeSend { get; set; }
        public int DelayPeriodId { get; set; }
        public int AttachedDownloadId { get; set; }
        public bool AllowDirectReply { get; set; }
        public int EmailAccountId { get; set; }
        public bool LimitedToStores { get; set; }
        public MessageDelayPeriodEnum DelayPeriod
        {
            get => (MessageDelayPeriodEnum)DelayPeriodId;
            set => DelayPeriodId = (int)value;
        }
    }
}
