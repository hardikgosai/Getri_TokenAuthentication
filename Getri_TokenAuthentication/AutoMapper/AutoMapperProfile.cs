using AutoMapper;
using Getri_TokenAuthentication.DTOs;
using Getri_TokenAuthentication.Models;

namespace Getri_TokenAuthentication.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterDTO, TblUser>();
            CreateMap<LoginDTO, TblUser>();
        }
    }
}
