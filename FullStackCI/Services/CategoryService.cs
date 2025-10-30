using FullStackCI.Dtos;
using FullStackCI.Models;
using FullStackCI.Repositories;

namespace FullStackCI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWorkRepository;

        public CategoryService(IUnitOfWork iUnitOfWork)
        {
            _unitOfWorkRepository = iUnitOfWork;
        }


        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWorkRepository._categoryRepositor.GetAllAsync();
            return categories.Select(ConvertToDto);
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWorkRepository._categoryRepositor.GetByIdAsync(id);
            return category == null ? null : ConvertToDto(category);
        }

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var category = new Category
            {
                Name = createCategoryDto.Name,
                Description = createCategoryDto.Description
            };

            var createdCategory = _unitOfWorkRepository._categoryCommandRepositor.CreateAsync(category);
            _unitOfWorkRepository.SaveChangesAsync();

            return ConvertToDto(createdCategory);
        }

        public async Task<CategoryDto?> UpdateCategoryAsync(int id, CreateCategoryDto updateCategoryDto)
        {
            var category = await _unitOfWorkRepository._categoryRepositor.GetByIdAsync(id);
            if (category == null) return null;

            category.Name = updateCategoryDto.Name;
            category.Description = updateCategoryDto.Description;

            await _unitOfWorkRepository._categoryCommandRepositor.UpdateAsync(category);
            _unitOfWorkRepository.SaveChangesAsync();

            return ConvertToDto(category);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            if (!await _unitOfWorkRepository._categoryRepositor.ExistsAsync(id))
                return false;

            await _unitOfWorkRepository._categoryCommandRepositor.DeleteAsync(id);
            _unitOfWorkRepository.SaveChangesAsync();

            return true;
        }

        private CategoryDto ConvertToDto(Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        //public async Task<bool> CategoryExistsAsync(int categoriaId)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<bool> CategoryExistsAsync(int categoriaId)
        {
            // Implementación básica
            return await _unitOfWorkRepository._categoryRepositor.ExistsAsync(categoriaId);
        }

        public async Task<object?> GetAllAuthorsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
