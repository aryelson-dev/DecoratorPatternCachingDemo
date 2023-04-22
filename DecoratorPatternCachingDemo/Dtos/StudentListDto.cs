namespace DecoratorPatternCachingExample.Dtos
{
    public class StudentListDto
    {
        public int Count { get; set; }
        public bool IsDataFromCache { get; set; }
        public List<StudentDto> Students { get; set; }
    }
}
