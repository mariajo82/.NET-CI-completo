using FullStackCI.Models;

namespace FullStackCI.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        // Expone todos los repositorios
        ICategoryRepository _categoryRepositor { get; }
        IAuthorRepository _authorRepositor { get; }
        IBookRepository _bookRepositor { get; }

        ICategoryCommandRepository _categoryCommandRepositor { get; }
        IAuthorCommandRepository _authorCommandRepositor { get; }
        IBookCommandRepository _bookCommandRepositor { get; }

        // Método para confirmar los cambios
        void SaveChangesAsync();
       
    }
}
