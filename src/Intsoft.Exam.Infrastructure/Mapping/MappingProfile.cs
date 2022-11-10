using AutoMapper;
using Intsoft.Exam.Application.Models;
using Intsoft.Exam.Domain.Entities;

namespace Intsoft.Exam.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<User, CreateUserModel>().ReverseMap();
            CreateMap<User, UpdateUserModel>().ReverseMap();
        }
    }
}
