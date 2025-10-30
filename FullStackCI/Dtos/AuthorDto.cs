using FullStackCI.Controllers;

namespace FullStackCI.Dtos
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public int BirthYear { get; set; }

        public List<Link> Links { get; set; } = new();
    }
}
