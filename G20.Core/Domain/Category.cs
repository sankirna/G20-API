
namespace G20.Core.Domain
{
    public partial class Category : BaseEntityWithTacking
    {
        public string Name { get; set; } 
        public string Description  { get; set; }
        public bool IsDeleted { get; set; }
    }
}
