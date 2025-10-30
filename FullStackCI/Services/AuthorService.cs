using FullStackCI.Dtos;
using FullStackCI.Models;
using FullStackCI.Repositories;

namespace FullStackCI.Services
{
    public class AuthorService : IAuthorService
    {
        //private readonly IAuthorRepository _authorRepository;
        private readonly IUnitOfWork _unitOfWorkRepository;

        public AuthorService(IUnitOfWork iUnitOfWork)
        {

            _unitOfWorkRepository = iUnitOfWork;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
        {
            var authors = await _unitOfWorkRepository._authorRepositor.GetAllAsync();
            return authors.Select(ConvertToDto);
        }

        public async Task<AuthorDto?> GetAuthorByIdAsync(int id)
        {
            var author = await _unitOfWorkRepository._authorRepositor.GetByIdAsync(id);
            return author == null ? null : ConvertToDto(author);
        }

        public async Task<AuthorDto> CreateAuthorAsync(CreateAuthorDto createAuthorDto)
        {
            var author = new Author
            {
                Name = createAuthorDto.Name,
                Nationality = createAuthorDto.Nationality,
                BirthYear = createAuthorDto.BirthYear
            };

            var createdAuthor = _unitOfWorkRepository._authorCommandRepositor.CreateAsync(author);

            _unitOfWorkRepository.SaveChangesAsync();

            return ConvertToDto(createdAuthor);
        }

       
        private AuthorDto ConvertToDto(Author author)
        {
            return new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Nationality = author.Nationality,
                BirthYear = author.BirthYear
            };
        }

        public async Task<bool> AuthorExistsAsync(int authorId)
        {
            // Implementación básica
            return await _unitOfWorkRepository._authorRepositor.ExistsAsync(authorId);
        }
    }
}
