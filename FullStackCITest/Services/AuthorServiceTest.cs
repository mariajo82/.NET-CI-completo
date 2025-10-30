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
    public class AuthorServiceTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly AuthorService _service;

        public AuthorServiceTest()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _service = new AuthorService(_unitOfWork.Object);
        }

        [Fact]
        public async Task GetAllAuthors_Success_ReturnAuthorDto()
        {
            // Arrange
            var authors = new List<Author>
            {
                new Author {
                    Id = 1,
                    Name = "Manuel",
                    Nationality = "Costarricense",
                    BirthYear = 1988,
                    Books = new List<Book>()
                },
                new Author {
                    Id = 2,
                    Name = "Ana",
                    Nationality = "Mexicana",
                    BirthYear = 1990,
                    Books = new List<Book>()
                }
            };

            _unitOfWork.Setup(u => u._authorRepositor.GetAllAsync())
                .ReturnsAsync(authors);
            //Act
            var result = await _service.GetAllAuthorsAsync();  

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());  
            Assert.Equal("Manuel", result.First().Name);  
        }

        [Fact]
        public void GetGetByIdAuthor_Succes_ReturnAuthorDto()
        {
            // Arrange
            var author = new Author
            {
                Id = 2,
                Name = "Ana",
                Nationality = "Mexicana",
                BirthYear = 1990,
                Books = new List<Book>()            
            };

        _unitOfWork.Setup(u => u._authorRepositor.GetByIdAsync(2))
                .ReturnsAsync(author);
            // Act
            var result = _service.GetAuthorByIdAsync(2);
            // Assert
            Assert.NotNull(result);
            Assert.Equal("Ana", result.Result.Name);
        }

        [Fact]
        public async Task AuthorExistsAsync_Success_ReturnTrue()
        {
            // Arrange
            var authorId = 1;

            _unitOfWork.Setup(u => u._authorRepositor.ExistsAsync(authorId))
                .ReturnsAsync(true);

            // Act
            var result = await _service.AuthorExistsAsync(authorId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task AuthorExistsAsync_NotFound_ReturnFalse()
        {
            // Arrange
            var authorId = 999; // ID que no existe

            _unitOfWork.Setup(u => u._authorRepositor.ExistsAsync(authorId))
                .ReturnsAsync(false);

            // Act
            var result = await _service.AuthorExistsAsync(authorId);

            // Assert
            Assert.False(result);
        }

    }
}
