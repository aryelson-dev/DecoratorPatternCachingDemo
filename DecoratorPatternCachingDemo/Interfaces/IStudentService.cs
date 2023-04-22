using DecoratorPatternCachingExample.Dtos;

namespace DecoratorPatternCachingExample.Interfaces
{
    public interface IStudentService
    {
        Task<StudentListDto> StudentListAsync();
    }
}
