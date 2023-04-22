using DecoratorPatternCachingExample.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DecoratorPatternCachingExample.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> ListAsync()
        {
            var list = await _studentService.StudentListAsync();
            return Ok(list);
        }
    }
}
