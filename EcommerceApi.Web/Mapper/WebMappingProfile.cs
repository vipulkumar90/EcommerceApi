using AutoMapper;
using EcommerceApi.DAL.Entities.UserData;
using EcommerceApi.Shared.Dtos;
using EcommerceApi.Web.ViewModels;
using System;

namespace EcommerceApi.Web.Mapper
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile() : base("WebMappingProfile")
        {
            CreateMap<User, UserViewModel>().ReverseMap()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.ModifiedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<LogInViewModel, LogInDto>().ReverseMap();

            CreateMap<UserAddressViewModel, UserAddress>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.AddressLine1 + ", " +  src.AddressLine2));
            CreateMap<UserAddress, UserAddressViewModel>()
                .ForMember(dest => dest.AddressLine1, opt => opt.MapFrom(src => src.Address));

            CreateMap<UserPayment, UserPaymentViewModel>().ReverseMap();
        }
    }
}
