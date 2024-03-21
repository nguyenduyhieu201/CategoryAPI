using CategoryAPI.Model;

namespace CategoryAPI.Services
{
    public interface ICategoryService
    {
        public Task<Result<Category>> CreateCategory(string Name);
    }
}
