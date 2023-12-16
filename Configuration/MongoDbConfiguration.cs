namespace Task1_Marketplace.Configuration
{
    public class MongoDbConfiguration
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public CollectionsConfiguration Collections { get; set; }
    }

    public class CollectionsConfiguration
    {
        public string Users { get; set; }
        public string Products { get; set; }
    }

}
