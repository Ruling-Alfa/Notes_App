using AutoMapper;
using CRUD_API.Business.Models;
using CRUD_API.Data.Entities;


namespace Discount.API.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<NoteModel, Note>().ReverseMap();
            CreateMap<UserModel,User>().ReverseMap();
            CreateMap<UserRegisterModel, User>().ReverseMap();
            CreateMap<UserRegisterResponseModel, User>().ReverseMap();
        }
    }
}
