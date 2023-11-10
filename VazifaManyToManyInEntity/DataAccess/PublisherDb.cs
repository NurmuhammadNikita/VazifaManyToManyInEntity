using Microsoft.EntityFrameworkCore;
using VazifaManyToManyInEntity.Models;

namespace VazifaManyToManyInEntity.DataAccess
{
    public class PublisherDb:DbContext
    {
        public PublisherDb(DbContextOptions<PublisherDb> options): base(options)
        {
            
        }

       public DbSet<Book> Books { get; set; }
       public DbSet<Author> Authors { get; set; }
       public DbSet<AuthorToBook> AuthorToBooks { get; set;}
    }
}
