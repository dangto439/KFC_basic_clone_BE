using AutoMapper;
using KFC.Entity;
using KFC.ModelViews.UserModels;


namespace KFC.Services.MapperProfile
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            //CreateMap<ApplicationUser, ResponseUserModel>();
            //CreateMap<User, ResponseUserModel>()
            //.ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
        }
    }
}