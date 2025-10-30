using FullStackCI.Data;
using FullStackCI.Models;
using FullStackCI.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackCITest.Repository
{
    public class BookRepositoryTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly BookRepository _repository;

        public BookRepositoryTests()
        {
            // Usar base de datos en memoria para tests
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);

            // Inicializar la base de datos
            _context.Database.EnsureCreated();

            _repository = new BookRepository(_context);
        }

        public void Dispose()
        {
            _context?.Database?.EnsureDeleted();
            _context?.Dispose();
            Console.WriteLine("=== TEST FINALIZADO ===");
        }

        [Fact]
        public async Task GetByIdAsync_ExistingBook_ReturnsBook()
        {
            // Arrange - Crear un libro SIN las relaciones para el test
            var book = new Book
            {
                Id = 9,
                Title = "Test Book",
                // Si las propiedades de navegación son required, inicialízalas como null
                Category = null,
                Author = null
            };

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();

            // IMPORTANTE: Limpiar el tracking del contexto
            _context.ChangeTracker.Clear();

            // Act
            var result = await _repository.GetByIdAsync(book.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(book.Id, result.Id);
            Assert.Equal("Test Book", result.Title);
        }

        [Fact]
        public async Task GetByIdAsync_NonExistingBook_ReturnsNull()
        {
            // Act
            var result = await _repository.GetByIdAsync(999); // ID que no existe

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllBooks()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { Id = 2, Title = "Book 10", Category = null, Author = null },
                new Book { Id = 3, Title = "Book 11", Category = null, Author = null }
            };

            await _context.Books.AddRangeAsync(books);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task ExistsAsync_ExistingBook_ReturnsTrue()
        {
            // Arrange
            var book = new Book { Id = 9, Title = "Test Book", Category = null, Author = null };
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            // Act
            var result = await _repository.ExistsAsync(book.Id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ExistsAsync_NonExistingBook_ReturnsFalse()
        {
            // Act
            var result = await _repository.ExistsAsync(999);

            // Assert
            Assert.False(result);
        }
    }
}