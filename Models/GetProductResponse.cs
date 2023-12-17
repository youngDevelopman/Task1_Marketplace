namespace Task1_Marketplace.Models
{
    public class GetProductResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public double Price { get; set; }
        public UserInfo AddedBy { get; set; }
    }

    public class UserInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}

