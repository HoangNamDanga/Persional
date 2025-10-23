using AutoMapper;
using DemoCtyLamHai.Application.Command.Model;
using DemoCtyLamHai.Application.Query.Model;
using DemoCtyLamHai.Domain;

namespace DemoCtyLamHai.Application.AutoMapper
{
    public class CtyLamHaiDemoProfile : Profile
    {
        public CtyLamHaiDemoProfile()
        {
            CreateMap<UserCreateCommandModel, User>();
            CreateMap<UserUpdateCommandModel, User>();

            CreateMap<User, UserQuery>();
        }
    }
}
