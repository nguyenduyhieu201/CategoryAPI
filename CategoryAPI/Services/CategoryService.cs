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
                    IsSuccess = false };
            }
            var category = new Category
            {
                Name = name,
                Id = Guid.NewGuid().ToString()
            };

            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            return new Result<Category> {
                Data = category,
                IsSuccess = true
            };

        }

        public async Task<Result<Category>> UpdateCategory(string Id, string Name)
        {
            var category = await GetById(Id);
            if (category.IsSuccess == false) return new Result<Category>
            {
                IsSuccess = false,
                Message = "Category not exists"
            };

            category.Data.Name = Name;
            await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Result<Category>> GetById(string Id)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(category => category.Id.Equals(Id));
            //throw new NotImplementedException();
            if (category == null)
            {
                return new Result<Category>
                {
                    IsSuccess = false,
                    Message = "Cannot find the category"
                };
            }

            return new Result<Category>
            {
                IsSuccess = true,
                Data = category,
            };
        }

        public async Task<Result<Category>> DeleteCategory(string Id)
        {
            var category = await GetById(Id);
            if (category.IsSuccess == false)
            {
                return new Result<Category>
                {
                    Message = "Cannot find category",
                    IsSuccess = false
                };
            }

            _dbContext.Categories.Remove(category.Data);
            await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Result<List<CategoryListModel>>> List(string name)
        {
            var categories = await _dbContext.Categories.Where (category =>  category.Name.Contains(name)).
                Select(category => new CategoryListModel { 
                    CategoryName = category.Name,
                }).ToListAsync();
            if (categories.Count == 0)
            {
                return new Result<List<CategoryListModel>>
                {
                    IsSuccess = false,
                    Message = "cannot find appropriate category"
                };
            }

            return new Result<List<CategoryListModel>>
            {
                IsSuccess = true,
                Data = categories,
                Message = "Success"
            };



        }
    }
}
