namespace Matrimony.Framework.Models
{
    public partial record EnumModel : BaseNopEntityModel
    {
        public string Key { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }
    }
}
