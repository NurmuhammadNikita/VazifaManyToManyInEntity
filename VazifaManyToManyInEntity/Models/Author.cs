using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace VazifaManyToManyInEntity.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public ICollection<AuthorToBook> AuthorToBooks { get; set; }
    }
}
