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
            if (!result.isSuccess)
            {
                return BadRequest(new {message = "Name exists"});
            }
            return Ok(result.Data);
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] string categoryId, [FromBody] string name)
        {
            var result = await _categoryService.UpdateCategory(categoryId, name);
            if (!result.isSuccess) return BadRequest(new {message = $"{result.Message}"});
            return Ok(result.Data);
        }
    }
}
