using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using VazifaManyToManyInEntity.DataAccess;
using VazifaManyToManyInEntity.Dtos;
using VazifaManyToManyInEntity.Models;

namespace VazifaManyToManyInEntity.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorToBookController : ControllerBase
    {
        private readonly PublisherDb db;
        public AuthorToBookController(PublisherDb publisher)
        {
            db = publisher;
        }


        [HttpGet]
        public async ValueTask<IActionResult> GetAuthorToBooksAllAsync()
        {
            var result = await db.AuthorToBooks.ToListAsync();

            return Ok(result);
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAuthorToBookById(long Id)
        {
            var result = await db.AuthorToBooks.FirstOrDefaultAsync(x => x.Id == Id);

            return Ok(result);
        }

        [HttpPost]
        public async ValueTask<IActionResult> CreateAuthorToBookAsync([FromForm] AuthorToBookDto dto)

        {
            var result = new AuthorToBook()
            {
                AuthorId = dto.AuthorId,
                BookId = dto.BookId,
            };

            await db.AddAsync(result);
            db.SaveChangesAsync();

            return Ok("Shep qo'shib qo'ydim");
        }

        [HttpPut]
        public async ValueTask<IActionResult> UpdateAuthorToBookAsync(int Id, AuthorToBookDto dto)
        {
            var result = await db.AuthorToBooks.FirstOrDefaultAsync(x => x.Id == Id);
            result.AuthorId = dto.AuthorId;
            result.BookId = dto.BookId;

            db.Update(result);
            db.SaveChangesAsync();

            return Ok("Shep yangilab qo'ydim");
        }

        [HttpDelete]
        public async ValueTask<IActionResult> DeleteAuthorToBookAsync(int Id)
        {
            var result = db.AuthorToBooks.FirstOrDefaultAsync(x => x.Id == Id);

            db.Remove(result);

            db.SaveChangesAsync();
            return Ok("O'chirvordim aka");
        }
    }
}
