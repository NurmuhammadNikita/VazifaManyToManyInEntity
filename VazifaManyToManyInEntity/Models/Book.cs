using System.Text.Json.Serialization;

namespace VazifaManyToManyInEntity.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }

        [JsonIgnore]
        public ICollection<AuthorToBook> AuthorToBooks { get; set; }
    }
}
