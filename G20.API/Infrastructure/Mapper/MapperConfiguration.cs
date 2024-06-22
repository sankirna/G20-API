using AutoMapper;
using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models;
using G20.API.Models.Categories;
using G20.API.Models.Cities;
using G20.API.Models.Countries;
using G20.API.Models.Coupons;
using G20.API.Models.Roles;
using G20.API.Models.TicketCategories;
using G20.API.Models.States;
using G20.API.Models.SubCategories;
using G20.API.Models.Teams;
using G20.API.Models.Users;
using G20.API.Models.Venue;
using G20.Core;
using G20.Core.Domain;
using G20.Framework.Models;
using Nop.Core.Infrastructure.Mapper;
using Profile = AutoMapper.Profile;
using G20.API.Models.VenueTicketCategoriesMap;
using G20.API.Models.Products;
using G20.API.Models.ProductTicketCategoriesMap;
using G20.API.Models.ProductCombos;
using G20.API.Models.Orders;
using G20.API.Models.ShoppingCarts;

namespace G20.API.Infrastructure.Mapper
{
    public partial class MapperConfiguration : Profile, IOrderedMapperProfile
    {
        public MapperConfiguration()
        {
            CreateCommonMap();
            CreateCountryMap();
            CreateUserMap();
            CreateRoleMap();
            CreateStateMap();
            CreateCityMap();
            CreateCouponMap();
            CreateCategoryMap();
            CreateSubCategoryMap();
            CreateVenueMap();
            CreateTeamMap();
            CreateTicketCategoryMap();
            CreateVenueTicketCategoryMap();
            CreateProductMap();
            CreateProductForSiteMap();
            CreateProductTicketCategoryMap();
            CreateProductComboMap();
            CreateShoppingCart();
            CreateShoppingOrderMap();
        }

        public virtual void CreateCommonMap()
        {
            CreateMap<EnumClass, EnumModel>();
        }

        public virtual void CreateUserMap()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
        }

        public virtual void CreateRoleMap()
        {
            CreateMap<Role, RoleModel>();
            CreateMap<RoleModel, Role>();
        }

        public virtual void CreateCountryMap()
        {
            CreateMap<Country, CountryModel>();
            CreateMap<CountryModel, Country>();
        }

        public virtual void CreateStateMap()
        {
            CreateMap<State, StateModel>().ForMember(desc => desc.Country, opt => opt.MapFrom(src => src.Country == null ? null : src.Country.ToModel<CountryModel>()));
            CreateMap<StateModel, State>();
        }

        public virtual void CreateCityMap()
        {
            CreateMap<City, CityModel>().ForMember(desc => desc.State, opt => opt.MapFrom(src => src.State == null ? null : src.State.ToModel<StateModel>())); ;
            CreateMap<CityModel, City>();
        }

        public virtual void CreateCouponMap()
        {
            CreateMap<CouponModel, Coupon>().AfterMap((src, dest) => dest.ExpirationDate = dest.ExpirationDate.ToUTCDataTime());
            CreateMap<Coupon, CouponModel>().AfterMap((src, dest) => dest.ExpirationDate = dest.ExpirationDate.ToLocalDataTime());
            CreateMap<Coupon, CouponRequestModel>().ReverseMap();
        }

        public virtual void CreateCategoryMap()
        {
            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<Category, CategoryRequestModel>().ReverseMap();
        }

        public virtual void CreateSubCategoryMap()
        {
            CreateMap<SubCategory, SubCategoryModel>().ReverseMap();
            CreateMap<SubCategory, SubCategoryRequestModel>().ReverseMap();
        }
        public virtual void CreateVenueMap()
        {
            CreateMap<Venue, VenueModel>().ReverseMap();
            CreateMap<Venue, VenueTicketCategoryMapRequestModel>().ReverseMap();
        }

        public virtual void CreateTeamMap()
        {
            CreateMap<Team, TeamModel>().ReverseMap();
            CreateMap<Team, TeamRequestModel>().ReverseMap();
        }

        public virtual void CreateTicketCategoryMap()
        {
            CreateMap<TicketCategory, TicketCategoryModel>().ReverseMap().ForMember(x => x.File, opt => opt.Ignore()); 
            CreateMap<TicketCategory, TicketCategoryRequestModel>().ReverseMap().ForMember(x => x.File, opt => opt.Ignore());
        }
        public virtual void CreateProductMap()
        {
            CreateMap<ProductModel, Product > ()
                .ForMember(x => x.File, opt => opt.Ignore())
                .AfterMap((src, dest) => dest.StartDateTime = dest.StartDateTime.ToUTCDataTime())  
                .AfterMap((src, dest) => dest.EndDateTime = dest.EndDateTime.ToUTCDataTime())  
                .AfterMap((src, dest) => dest.ScheduleDateTime = dest.ScheduleDateTime.ToUTCDataTime());
            CreateMap<Product, ProductModel>()
                .ForMember(x => x.File, opt => opt.Ignore())
                .AfterMap((src, dest) => dest.StartDateTime = dest.StartDateTime.ToLocalDataTime())
                .AfterMap((src, dest) => dest.EndDateTime = dest.EndDateTime.ToLocalDataTime())
                .AfterMap((src, dest) => dest.ScheduleDateTime = dest.ScheduleDateTime.ToLocalDataTime());

            CreateMap<ProductRequestModel, Product>()
                .ForMember(x => x.File, opt => opt.Ignore())
                .AfterMap((src, dest) => dest.StartDateTime = dest.StartDateTime.ToUTCDataTime())
                .AfterMap((src, dest) => dest.EndDateTime = dest.EndDateTime.ToUTCDataTime())
                .AfterMap((src, dest) => dest.ScheduleDateTime = dest.ScheduleDateTime.ToUTCDataTime());
            CreateMap<Product, ProductRequestModel>()
                .ForMember(x => x.File, opt => opt.Ignore())
                .AfterMap((src, dest) => dest.StartDateTime = dest.StartDateTime.ToLocalDataTime())
                .AfterMap((src, dest) => dest.EndDateTime = dest.EndDateTime.ToLocalDataTime())
                .AfterMap((src, dest) => dest.ScheduleDateTime = dest.ScheduleDateTime.ToLocalDataTime());
        }

        public virtual void CreateVenueTicketCategoryMap()
        {
            CreateMap<VenueTicketCategoryMap, VenueTicketCategoryMapModel>().ReverseMap().ForMember(x => x.File, opt => opt.Ignore());
            CreateMap<VenueTicketCategoryMap, VenueTicketCategoryMapRequestModel>().ReverseMap().ForMember(x => x.File, opt => opt.Ignore());
        }

        public virtual void CreateProductTicketCategoryMap()
        {
            CreateMap<ProductTicketCategoryMap, ProductTicketCategoryMapModel>().ReverseMap()
                .ForMember(x => x.TicketCategory, opt => opt.Ignore())
                .ForMember(x => x.Product, opt => opt.Ignore()); 
            CreateMap<ProductTicketCategoryMap, ProductTicketCategoryMapRequestModel>().ReverseMap()
                .ForMember(x => x.TicketCategory, opt => opt.Ignore())
                .ForMember(x => x.Product, opt => opt.Ignore());
        }

        public virtual void CreateProductComboMap()
        {
            CreateMap<ProductCombo, ProductComboModel>().ReverseMap();
        }
         public virtual void CreateProductForSiteMap()
        {
            CreateMap<Product, ProductForSiteModel>().ReverseMap();
        }



        public virtual void CreateShoppingCart()
        {
            CreateMap<ShoppingCartModel, ShoppingCart>().ReverseMap();
            CreateMap<ShoppingCartItemModel, ShoppingCartItem>().ReverseMap();
        }

        public virtual void CreateShoppingOrderMap()
        {
            CreateMap<ShoppingCartModel, OrderModel>().ReverseMap();
            CreateMap<ShoppingCartItemModel, OrderProductItemModel>().ReverseMap();
        }

        public int Order => 0;
    }
}
