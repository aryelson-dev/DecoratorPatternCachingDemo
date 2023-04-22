using Bogus;
using DecoratorPatternCachingExample.Interfaces;
using DecoratorPatternCachingExample.Models;

namespace DecoratorPatternCachingExample.Persistence.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        public Task<List<Student>> ListAsync()
        {
            return Task.FromResult(FakeStudentStore());
        }

        public List<Student> FakeStudentStore()
        {
            var result = new Faker<Student>()
                .RuleFor(o => o.Register, f => f.Random.Int(1010, 1999))
                .RuleFor(o => o.Name, f => f.Person.FullName)
                .Generate(30);

            return result;
        }
    }
}
