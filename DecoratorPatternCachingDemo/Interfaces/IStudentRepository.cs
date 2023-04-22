using DecoratorPatternCachingExample.Models;

namespace DecoratorPatternCachingExample.Interfaces
{
    public interface IStudentRepository
    {
        public Task<List<Student>> ListAsync();
    }
}
