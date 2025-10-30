using FullStackCI.Dtos;
using FullStackCI.Models;
using FullStackCI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FullStackCI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IAuthorCommandService _authorCommandService;

        public AuthorsController(IAuthorService authorService, IAuthorCommandService authorCommandService)
        {
            _authorService = authorService;
            _authorCommandService = authorCommandService;
        }

        [HttpGet(Name = "ObtenerAutores")]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
        {
            var authors = await _authorService.GetAllAuthorsAsync();

            //// Agregar hipervínculos 
            //authors.Links.Add(new Link
            //{
            //    Href = Url.Link("ObtenerAutores"),
            //    Rel = "self",
            //    Method = "GET"
            //});


            return Ok(authors);
        }

        [HttpGet("{id}", Name = "ObtenerAutor")]
        public async Task<ActionResult<AuthorDto>> GetAuthor(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            // Agregar hipervínculos 
            author.Links.Add(new Link
            {
                Href = Url.Link("ObtenerAutor", new { id }),
                Rel = "self",
                Method = "GET"
            });

            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDto>> CreateAuthor(CreateAuthorDto createAuthorDto)
        {
            var author = await _authorCommandService.CreateAuthorAsync(createAuthorDto);
            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, CreateAuthorDto updateAuthorDto)
        {
            var author = await _authorCommandService.UpdateAuthorAsync(id, updateAuthorDto);
            if (author == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var result = await _authorCommandService.DeleteAuthorAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
