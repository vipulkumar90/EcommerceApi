using AutoMapper;
using EcommerceApi.DAL.Entities.ProductData;
using EcommerceApi.DAL.Entities.ShopingCart;
using EcommerceApi.DAL.Entities.UserData;
using EcommerceApi.Shared.Dtos;
using EcommerceApi.Web.ViewModels;
using EcommerceApi.Web.ViewModels.ProductViewModels;
using EcommerceApi.Web.ViewModels.ShoppingCart;
using System;

namespace EcommerceApi.Web.Mapper
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile() : base("WebMappingProfile")
        {
            CreateMap<User, UserViewModel>().ReverseMap()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<LogInViewModel, LogInDto>().ReverseMap();

            CreateMap<UserAddressViewModel, UserAddress>()
                .ForMember(dest => dest.Address, opt => 
                opt.MapFrom(src => src.AddressLine1 + ", " +  src.AddressLine2));
            CreateMap<UserAddress, UserAddressViewModel>()
                .ForMember(dest => dest.AddressLine1, opt => opt.MapFrom(src => src.Address));

            CreateMap<UserPayment, UserPaymentViewModel>().ReverseMap();

            CreateMap<Product, ProductViewModel>().ReverseMap();

            CreateMap<ProductCategory, ProductCategoryViewModel>().ReverseMap();

            CreateMap<ProductInventory, ProductInventoryViewModel>().ReverseMap();

            CreateMap<Discount, DiscountViewModel>().ReverseMap();

            CreateMap<CartItem, CartItemViewModel>().ReverseMap();

            CreateMap<OrderDetail, OrderDetailViewModel>().ReverseMap();

            CreateMap<OrderItem, OrderItemViewModel>().ReverseMap();

            CreateMap<PaymentDetail, PaymentDetailViewModel>().ReverseMap();

            CreateMap<ShoppingSession, ShoppingSessionViewModel>().ReverseMap();
        }
    }
}
