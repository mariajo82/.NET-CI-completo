using FullStackCI.Models;
using FullStackCI.Repositories;
using FullStackCI.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackCITest.Services
{
    public class CategoryServiceTest
    {
        //private readonly Mock<ICategoryRepository> _categoryRepository = new Mock<ICategoryRepository>();
        //private readonly Mock<ICategoryCommandRepository> _categoryCommandRepository = new Mock<ICategoryCommandRepository>();
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly CategoryService _service;

        public CategoryServiceTest()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _service = new CategoryService(_unitOfWork.Object);
        }

        [Fact]
        public void GetAllCategoria_Succes_ReturnCategoriaDto()
        {
            // Arrange
            var categorias = new List<Category>
            {
                new Category { Id = 1, Name = "Categoria1", Description = "Descripcion1" },
                new Category { Id = 2, Name = "Categoria2", Description = "Descripcion2" }
            };
            _unitOfWork.Setup(u => u._categoryRepositor.GetAllAsync())
                .ReturnsAsync(categorias);
            // Act
            var result = _service.GetAllCategoriesAsync();
            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Result.Count());
            Assert.Equal("Categoria1", result.Result.First().Name);
        }

        [Fact]
        public void GetGetByIdCategoria_Succes_ReturnCategoriaDto()
        {
            // Arrange
            var categoria = new Category { Id = 1, Name = "Categoria1", Description = "Descripcion1" };

            _unitOfWork.Setup(u => u._categoryRepositor.GetByIdAsync(1))
                .ReturnsAsync(categoria);
            // Act
            var result = _service.GetCategoryByIdAsync(1);
            // Assert
            Assert.NotNull(result);           
            Assert.Equal("Categoria1", result.Result.Name);
        }

        [Fact]
        public async Task CategoryExistsAsync_Success_ReturnTrue()
        {
            // Arrange
            var categoriaId = 1;

            _unitOfWork.Setup(u => u._categoryRepositor.ExistsAsync(categoriaId))
                .ReturnsAsync(true);

            // Act
            var result = await _service.CategoryExistsAsync(categoriaId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CategoryExistsAsync_NotFound_ReturnFalse()
        {
            // Arrange
            var categoriaId = 999; // ID que no existe

            _unitOfWork.Setup(u => u._categoryRepositor.ExistsAsync(categoriaId))
                .ReturnsAsync(false);

            // Act
            var result = await _service.CategoryExistsAsync(categoriaId);

            // Assert
            Assert.False(result);
        }
    }
}
