using FullStackCI.Data;
using FullStackCI.Models;
using System;

namespace FullStackCI.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IBookRepository _bookRepositor { get; set; }
        public IAuthorRepository _authorRepositor { get; set; }
        public ICategoryRepository _categoryRepositor { get; set; }


        public ICategoryCommandRepository _categoryCommandRepositor { get; set; }
        public IAuthorCommandRepository _authorCommandRepositor { get; set; }
        public IBookCommandRepository _bookCommandRepositor { get; set; }


        public UnitOfWork(ApplicationDbContext context)
        {

            _context = context;

            _bookRepositor = new BookRepository(context);
            _authorRepositor = new AuthorRepository(context);
            _categoryRepositor = new CategoryRepository(context);

            _categoryCommandRepositor = new CategoryCommandRepository(context);
            _authorCommandRepositor = new AuthorCommandRepository(context);
            _bookCommandRepositor = new BookCommandRepository(context);
        }

        public void SaveChangesAsync()
        {
            _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
