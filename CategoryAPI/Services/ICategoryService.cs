using CategoryAPI.Model;

namespace CategoryAPI.Services
{
    public interface ICategoryService
    {
        public Task<Result<Category>> CreateCategory(string Name);
        public Task<Result<Category>> UpdateCategory(string Id, string Name);
        public Task<Result<Category>> GetById(string Id);
        public Task<Result<Category>> DeleteCategory(string Id);
        public Task<Result<List<CategoryListModel>>> List(string Name);
    }
}
