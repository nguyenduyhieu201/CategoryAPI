using CategoryAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CategoryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpPost] 
        public async Task<IActionResult> CreateCategory([FromBody] string name)
        {
            var result = await _categoryService.CreateCategory(name);
            if (!result.IsSuccess)
            {
                return BadRequest(new {message = "Name exists"});
            }
            return Ok(result.Data);
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory(string categoryId, [FromBody] string name)
        {
            var result = await _categoryService.UpdateCategory(categoryId, name);
            if (!result.IsSuccess) return BadRequest(new {message = $"{result.Message}"});
            return Ok(result.Data);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategory(string categoryId)
        {
            var result = await _categoryService.GetById(categoryId);
            if (!result.IsSuccess) return BadRequest(new { message = $"{result.Message}" });
            return Ok(result.Data);
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(string categoryId)
        {
            var result = await _categoryService.DeleteCategory(categoryId);
            if (result.IsSuccess) return BadRequest(new { message = $"{result.Message}" });
            return Ok(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] string? name)
        {
            var result = await _categoryService.List(name);
            if (result.IsSuccess) return Ok(result.Data);
            return BadRequest(new { message = $"{result.Message}" });
        }

    }
}
