using System.Text.Json.Serialization;

namespace VazifaManyToManyInEntity.Models
{
    public class AuthorToBook
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int BookId { get; set; }

        [JsonIgnore]
        public Author Author { get; set; }
        public Book Book { get; set; }
    }
}