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
    public class AuthorController : ControllerBase
    {
        private readonly PublisherDb db;
        public AuthorController(PublisherDb publisher)
        {
            db = publisher;
        }
        
        /// <summary>
        /// Hamma avtorlani olib kelsin
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async ValueTask<IActionResult> GetAuthorsAllAsync()
        {
            var result = await db.Authors.ToListAsync();

            return Ok(result);
        }
        /// <summary>
        /// id ga teng bo'ganlani olib kesin
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async ValueTask<IActionResult> GetAuthorById(long Id)
        {
            var result = await db.Authors.FirstOrDefaultAsync(x => x.Id == Id);

            return Ok(result);
        }

        /// <summary>
        /// id ga teng bo'gan avtorni kitoblarini olib kesin
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async ValueTask<IActionResult> GetAuthorBooksByIdAsync(int Id)
        {
            var result = await db.Authors.Include(x => x.AuthorToBooks).ThenInclude(x => x.Book).FirstOrDefaultAsync(x => x.Id == Id);

            return Ok(result);
        }

        /// <summary>
        /// hamma avtorlani kitoblari bilan olib kesing
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async ValueTask<IActionResult> GetAuthorBooksAsync()
        {
            var result = await db.Authors.Include(x => x.AuthorToBooks).ThenInclude(x => x.Book).ToListAsync();

            return Ok(result);
        }

        [HttpPost]
        public async ValueTask<IActionResult> CreateAuthorAsync([FromForm] AuthorDto dto)

        {
            var result = new Author()
            {
                Name = dto.Name,
                Age = dto.Age,
            };

            await db.AddAsync(result);
            db.SaveChangesAsync();

            return Ok("Shep qo'shib qo'ydim");
        }

        [HttpPut]
        public async ValueTask<IActionResult> UpdateAuthorAsync(int Id, AuthorDto dto)
        {
            var result = await db.Authors.FirstOrDefaultAsync(x => x.Id == Id);
            result.Name = dto.Name;
            result.Age = dto.Age;

            db.Update(result);
            db.SaveChangesAsync();

            return Ok("Shep yangilab qo'ydim");
        }

        [HttpDelete]
        public async ValueTask<IActionResult> DeleteAuthorAsync(int Id)
        {
            var result = db.Authors.FirstOrDefaultAsync(x => x.Id == Id);

            db.Remove(result);

            db.SaveChangesAsync();
            return Ok("O'chirvordim aka");
        }
    }
}
