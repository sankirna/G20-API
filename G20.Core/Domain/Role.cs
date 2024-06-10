
namespace G20.Core.Domain
{
    public partial class Role : BaseEntityWithTacking
    {
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
