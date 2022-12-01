using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApi.BLL.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() : base("MappingProfile")
        {
            //CreateMap<ReimbursementDto, ReimbursementDomain>().ReverseMap();
            //CreateMap<ApprovalDto, ApprovalDomain>().ReverseMap();
        }
    }
}
