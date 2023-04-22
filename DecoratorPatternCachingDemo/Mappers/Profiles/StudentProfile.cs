using AutoMapper;
using DecoratorPatternCachingExample.Dtos;
using DecoratorPatternCachingExample.Models;

namespace DecoratorPatternCachingExample.Mappers.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentDto>();
        }
    }
}
