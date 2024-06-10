
namespace G20.Core.Domain
{
    public partial class SubCategory : BaseEntityWithTacking
    {
        public string Name { get; set; } 
        public string Description  { get; set; }
        public int CategoryId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
