using AutoMapper;
using DecoratorPatternCachingExample.Dtos;
using DecoratorPatternCachingExample.Interfaces;

namespace DecoratorPatternCachingExample.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentRepository,
            IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<StudentListDto> StudentListAsync()
        {
            var result = await _studentRepository.ListAsync();

            var students = result
                .Select(e => _mapper.Map<StudentDto>(e))
                .ToList();

            return new StudentListDto()
            {
                Students = students,
                Count = students.Count,
                IsDataFromCache = GlobalInfo.IsDataFromCache
            };
        }
    }
}
