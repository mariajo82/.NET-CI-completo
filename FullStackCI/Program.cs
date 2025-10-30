// --- Archivo: Program.cs ---
//
// Configuración de la aplicación.
//
using Asp.Versioning;
using FullStackCI.Data;
using FullStackCI.Models;
using FullStackCI.Repositories;
using FullStackCI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la base de datos en memoria y precarga de datos
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("LibraryDb"));

// Register repositories
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

// Register services
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();

builder.Services.AddScoped<IAuthorCommandService, AuthorCommandService>();
builder.Services.AddScoped<IBookCommandService, BookCommandService>();

// Agrega servicios a la tubería (pipeline) de contenedores
builder.Services.AddControllers();

//11/09/2025
builder.Services.AddHttpClient();
builder.Services.AddScoped<HttpClientService, HttpClientService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Configura el versionado de la API (solo por URL)
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

// Configura Swagger para la documentación
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Cargar datos de prueba solo en desarrollo
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Asegurar que la base de datos se crea
        context.Database.EnsureCreated();

        // Los datos ya se cargan automáticamente a través del SeedData en OnModelCreating
        // Pero podemos verificar si hay datos y agregar más si es necesario
        if (!context.Books.Any())
        {
            // Si por alguna razón no hay datos, los agregamos manualmente
            SeedTestData(context);
        }
    }
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Método para asegurar la carga de datos de prueba
void SeedTestData(ApplicationDbContext context)
{
    try
    {
        // Categorías
        var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Ficción", Description = "Libros de ficción y novelas" },
                new Category { Id = 2, Name = "Ciencia", Description = "Libros científicos y técnicos" },
                new Category { Id = 3, Name = "Historia", Description = "Libros históricos" },
                new Category { Id = 4, Name = "Fantasy", Description = "Libros de fantasía" }
            };

        // Autores
        var authors = new List<Author>
            {
                new Author { Id = 1, Name = "Gabriel García Márquez", Nationality = "Colombiano", BirthYear = 2007},
                new Author { Id = 2, Name = "J.K. Rowling", Nationality = "Británica", BirthYear = 1965 },
                new Author { Id = 3, Name = "Stephen King", Nationality = "Estadounidense", BirthYear = 2000 },
                new Author { Id = 4, Name = "Isaac Asimov", Nationality = "Ruso", BirthYear =1987 },
                new Author { Id = 5, Name = "Yuval Noah Harari", Nationality = "Israelí", BirthYear = 2005 }
            };

        // Libros
        var books = new List<Book>
            {
                new Book {
                    Id = 1,
                    Title = "Cien años de soledad",
                    ISBN = "978-0307474728",
                    PublicationYear = 2010,
                    Pages = 417,
                    Description = "Una obra maestra de la literatura latinoamericana",
                    CategoryId = 1,
                    AuthorId = 1
                },
                new Book {
                    Id = 2,
                    Title = "Harry Potter y la piedra filosofal",
                    ISBN = "978-8478884452",
                    PublicationYear = 2020,
                    Pages = 320,
                    Description = "El primer libro de la serie Harry Potter",
                    CategoryId = 4,
                    AuthorId = 2
                },
                new Book {
                    Id = 3,
                    Title = "It",
                    ISBN = "978-1501142970",
                    PublicationYear = 1986,
                    Pages = 1138,
                    Description = "Una novela de terror",
                    CategoryId = 1,
                    AuthorId = 3
                },
                new Book {
                    Id = 4,
                    Title = "Fundación",
                    ISBN = "978-0553293357",
                    PublicationYear = 2025,
                    Pages = 255,
                    Description = "Primer libro de la serie Fundación",
                    CategoryId = 2,
                    AuthorId = 4
                },
                new Book {
                    Id = 5,
                    Title = "Sapiens: De animales a dioses",
                    ISBN = "978-8499926223",
                    PublicationYear = 2011,
                    Pages = 496,
                    Description = "Breve historia de la humanidad",
                    CategoryId = 3,
                    AuthorId = 5
                }
            };

        context.Categories.AddRange(categories);
context.Authors.AddRange(authors);
context.Books.AddRange(books);

context.SaveChanges();

Console.WriteLine("Datos de prueba cargados exitosamente.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al cargar datos de prueba: {ex.Message}");
    }
}
