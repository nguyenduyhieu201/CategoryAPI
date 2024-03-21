using CategoryAPI.Infrastructure;
using CategoryAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CategoryAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryDbContext _dbContext;

        public CategoryService(CategoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        private List<Category> _categories;

        public async Task<Result<Category>> CreateCategory(string name)
        {
            if (_dbContext.Categories.Any(category => category.Name.Equals(name))) {
                return new Result<Category> {
                    Data = null,
                    isSuccess = false };
            }
            var category = new Category
            {
                Name = name,
                Id = Guid.NewGuid()
            };

            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            return new Result<Category> {
                Data = category,
                isSuccess = true
            };

        }
    }
}
