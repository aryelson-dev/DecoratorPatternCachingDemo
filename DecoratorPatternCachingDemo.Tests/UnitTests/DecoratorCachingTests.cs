using Bogus;
using DecoratorPatternCachingExample.Interfaces;
using DecoratorPatternCachingExample.Models;
using DecoratorPatternCachingExample.Persistence.Caching;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace DecoratorPatternCachingExample.Tests.UnitTests
{
    public class DecoratorCachingTests
    {
        private readonly Mock<IStudentRepository> _studentRepository;
        private readonly StudentCachingDecorator<IStudentRepository> _studentCachingDecorator;

        public DecoratorCachingTests()
        {
            _studentRepository = new Mock<IStudentRepository>();

            _studentCachingDecorator = new StudentCachingDecorator<IStudentRepository>(
                _studentRepository.Object,
                GetMemoryCache(),
                GetConfiguration());
        }

        [Fact]
        public void Returns_Count_1_From_List_DataSource_Change_List_And_Continue_Returning_Count_1_From_Cache()
        {
            // returns one element from data source
            _studentRepository.Setup(e => e.ListAsync()).Returns(FakeStudentList);
            Assert.True(_studentCachingDecorator.ListAsync().Result.Count == 1);

            // changes data source
            _studentRepository.Setup(e => e.ListAsync()).Returns(ChangedFakeStudentList);

            // returns from caching
            Assert.True(_studentCachingDecorator.ListAsync().Result.Count == 1);
            Assert.True(_studentCachingDecorator.ListAsync().Result.Count == 1);
            Assert.True(_studentCachingDecorator.ListAsync().Result.Count == 1);
        }

        #region Fake Data

        public static Task<List<Student>> FakeStudentList()
        {
            var result = new Faker<Student>()
                .RuleFor(o => o.Name, f => f.Person.FullName)
                .RuleFor(o => o.Register, f => f.Random.Int(1010, 1999))
                .Generate(1);

            return Task.FromResult(result);
        }

        public static Task<List<Student>> ChangedFakeStudentList()
        {
            var result = new Faker<Student>()
                .RuleFor(o => o.Name, f => f.Person.FullName)
                .RuleFor(o => o.Register, f => f.Random.Int(1010, 1999))
                .Generate(3);

            return Task.FromResult(result);
        }

        #endregion

        #region Configs

        public static IMemoryCache GetMemoryCache()
        {
            var services = new ServiceCollection().AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetService<IMemoryCache>();
        }

        public static IConfiguration GetConfiguration()
        {
            var inMemoryConfigurations = new Dictionary<string, string>()
            {
                { "CachedDataTimeExpirationInSeconds", "10" }
            };

            return new ConfigurationBuilder()
                .AddInMemoryCollection(inMemoryConfigurations)
                .Build();
        }

        #endregion
    }
}
