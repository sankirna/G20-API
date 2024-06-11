using AutoMapper;
using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models;
using G20.API.Models.Categories;
using G20.API.Models.Cities;
using G20.API.Models.Countries;
using G20.API.Models.Coupons;
using G20.API.Models.Roles;
using G20.API.Models.StandCategories;
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
            CreateStandCategoryMap();
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
            CreateMap<Coupon, CouponModel>().ReverseMap();
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
            CreateMap<Venue, VenueRequestModel>().ReverseMap();
        }

        public virtual void CreateTeamMap()
        {
            CreateMap<Team, TeamModel>().ReverseMap();
            CreateMap<Team, TeamRequestModel>().ReverseMap();
        }

        public virtual void CreateStandCategoryMap()
        {
            CreateMap<StandCategory, StandCategoryModel>().ReverseMap();
            CreateMap<StandCategory, StandCategoryRequestModel>().ReverseMap();
        }

        public int Order => 0;
    }
}
