using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Task1_Marketplace.Models
{
    public class AddProductRequest
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public double Price { get; set; }
    }
}
