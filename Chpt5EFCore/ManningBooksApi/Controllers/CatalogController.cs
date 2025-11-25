using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static ManningBooksApi.CatalogContext;

namespace ManningBooksApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogContext _dbContext;

        public CatalogController(CatalogContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Authorize("AuthenticatedUsers")]
        public IAsyncEnumerable<Book> GetBooks(
            string? titleFilter = null,
            string? order = "title",
            int skip = 0,
            int take = 20)
        {
            IQueryable<Book> query = _dbContext.Books
              .Include(b => b.Ratings)
              .AsNoTracking();
            if (titleFilter != null)
            {
                query = query.Where(b => b.Title.ToLower().Contains(titleFilter.ToLower()));
            }

            query = order?.ToLower() switch
            {
                "title" => query.OrderBy(b => b.Title),
                "title_desc" => query.OrderByDescending(b => b.Title),
                "id" => query.OrderBy(b => b.Title),
                "id_desc" => query.OrderByDescending(b => b.Title),
                "rating" => query.OrderByDescending(b => b.Ratings.Average(r => r.Stars)),
                _ => query.OrderBy(b => b.Title)

            };

            query = query.Skip(skip).Take(take);

            return query.AsAsyncEnumerable();
        }

        [HttpGet("{id}")]
        [Authorize("AuthenticatedUsers")]
        public Task<Book> GetBook(int id)
        {
            return _dbContext.Books.FirstOrDefaultAsync(b => b.Id == id);
        }

        [HttpPost]
        [Authorize("OnlyMe")]
        public async Task<Book> CreateBookAsync(BookCreateCommand command, CancellationToken cancellationToken)
        {
            var book = new Book(command.Title, command.Description);

            var entity = _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity.Entity;
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize("OnlyMe")]
        public async Task<IActionResult> UpdateBookAsync(
            int id, BookUpdateCommand command, CancellationToken cancellationToken
        )
        {
            var book = await _dbContext.FindAsync<Book>(
                new object?[] { id }, cancellationToken);
            if (book == null)
            {
                return NotFound();
            }

            if (command.Title != null)
            {
                book.Title = command.Title;
            }

            if (command.Description != null)
            {
                book.Description = command.Description;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize("OnlyMe")]
        public async Task<IActionResult> DeleteBookAsync(int id, CancellationToken cancellationToken)
        {
            var book = await _dbContext.Books
               .Include(b => b.Ratings)
               .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
            if (book == null)
            {
                return NotFound();
            }

            _dbContext.Remove(book);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return NoContent();        }
    }
}
