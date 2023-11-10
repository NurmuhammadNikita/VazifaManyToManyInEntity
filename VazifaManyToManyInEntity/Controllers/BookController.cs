using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VazifaManyToManyInEntity.DataAccess;
using VazifaManyToManyInEntity.Dtos;
using VazifaManyToManyInEntity.Models;

namespace VazifaManyToManyInEntity.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly PublisherDb db;
        public BookController(PublisherDb publisher)
        {
            db = publisher;
        }


        [HttpGet]
        public async ValueTask<IActionResult> GetBooksAllAsync()
        {
            var result = await db.Books.ToListAsync();

            return Ok(result);
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetBookById(long Id)
        {
            var result = await db.Books.FirstOrDefaultAsync(x => x.Id == Id);

            return Ok(result);
        }

        [HttpPost]
        public async ValueTask<IActionResult> CreateBookAsync([FromForm] BookDto dto)

        {
            var result = new Book()
            {
                Name = dto.Name,
                Publisher = dto.Publisher
            };

            await db.AddAsync(result);
            await db.SaveChangesAsync();

            return Ok("Shep qo'shib qo'ydim");
        }

        [HttpPut]
        public async ValueTask<IActionResult> UpdateBookAsync(int Id, BookDto dto)
        {
            var result = await db.Books.FirstOrDefaultAsync(x => x.Id == Id);
            result.Name = dto.Name;
            result.Publisher = dto.Publisher;

            db.Update(result);
            db.SaveChangesAsync();

            return Ok("Shep yangilab qo'ydim");
        }

        [HttpDelete]
        public async ValueTask<IActionResult> DeleteBookAsync(int Id)
        {
            var result = db.Books.FirstOrDefaultAsync(x => x.Id == Id);

            db.Remove(result);

            db.SaveChangesAsync();
            return Ok("O'chirvordim aka");
        }
    }
}
