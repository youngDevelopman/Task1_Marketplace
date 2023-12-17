using Amazon.Runtime.Internal;
using MongoDB.Bson;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;
using Task1_Marketplace.Configuration;
using Task1_Marketplace.Domain;
using Task1_Marketplace.Helpers;

namespace Task1_Marketplace
{
    public static class SeedMongoDb
    {
        public static void Seed(IMongoClient client, MongoDbConfiguration configuration)
        {
            var database = client.GetDatabase(configuration.DatabaseName);

            CreateCollectionIfNotExists(database, configuration.Collections.Users);
            CreateCollectionIfNotExists(database, configuration.Collections.Products);

            var usersCollection = database.GetCollection<User>(configuration.Collections.Users);
            SeedUsersCollectionIfEmpty(usersCollection);

            var productCollection = database.GetCollection<Product>(configuration.Collections.Products);
            var user = usersCollection.AsQueryable().Where(x => x.Email == "user1@test.com").FirstOrDefault();
            SeedProductCollectionIfEmpty(productCollection, user);
            CreateProductsTextIndex(productCollection);
        }

        private static void CreateCollectionIfNotExists(IMongoDatabase database, string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);
            var collections = database.ListCollections(new ListCollectionsOptions { Filter = filter });
            var exists = collections.Any();

            if (!exists)
            {
                database.CreateCollection(collectionName);
            }
        }

        private static void SeedUsersCollectionIfEmpty(IMongoCollection<User> collection)
        {
            bool exist = collection.Find(p => true).Any();
            if (!exist)
            {
                var predefinedUsers = new List<(string email, string name, string password)>()
                {
                    new ("admin@test.com","admin", "pass1"),
                    new ("user1@test.com","user1", "userpass"),
                };

                var users = new List<User>();
                foreach (var predefinedUser in predefinedUsers)
                {
                    (string salt, string hash) = PasswordHasher.HashPassword(predefinedUser.password);
                    var user = new User()
                    {
                        Name = predefinedUser.name,
                        Email = predefinedUser.email,
                        PasswordHash = hash,
                        PasswordSalt = salt
                    };
                    users.Add(user);
                }
               
                collection.InsertMany(users);
            }
        }

        private static void SeedProductCollectionIfEmpty(IMongoCollection<Product> collection, User addedBy)
        {
            bool exist = collection.Find(p => true).Any();
            if (!exist)
            {
                var products = new List<Product>() 
                { 
                    new Product() 
                    { 
                        Name = "SolarBreeze Portable Solar Charger",
                        Description = "A compact, eco-friendly solar charger perfect for outdoor enthusiasts. Features high-efficiency solar panels and a durable, weather-resistant design.",
                        Image = "https://files.oaiusercontent.com/file-wSS30zGyisQtvF4ncLO4AhhH?se=2023-12-16T22%3A42%3A20Z&sp=r&sv=2021-08-06&sr=b&rscc=max-age%3D31536000%2C%20immutable&rscd=attachment%3B%20filename%3Db6e422d9-5714-4139-a1c1-15d9e4c81588.webp&sig=qQLNSNMm4S6USAIbmdzxpilK/MTKU8xmVkyI0MXZ7ME%3D",
                        Price = 49.99,
                        Rating = 5,
                        AddedBy = new UserInfo()
                        {
                            Id = addedBy.Id,
                            Name = addedBy.Name,
                        }
                    },
                    new Product()
                    {
                        Name = "AquaPure Smart Water Bottle",
                        Description = "A high-tech water bottle with built-in filtration and temperature control, ensuring your water is always clean and at the perfect temperature.",
                        Image = "https://m.media-amazon.com/images/I/61HN36q1jzS.jpg",
                        Price = 100,
                        Rating = 3,
                        AddedBy = new UserInfo()
                        {
                            Id = addedBy.Id,
                            Name = addedBy.Name,
                        }
                    }
                };

                collection.InsertMany(products);
            }
        }

        private static void CreateProductsTextIndex(IMongoCollection<Product> collection)
        {
            var indexName = "name_description_text"; 

            var existingIndexes = collection.Indexes.List().ToList();
            if (existingIndexes.Any(index => index["name"] == indexName))
            {
                return;
            }

            var indexKeys = Builders<Product>.IndexKeys.Text(p => p.Name)
                                               .Text(p => p.Description);

            var indexOptions = new CreateIndexOptions
            {
                Name = indexName,
                Weights = new BsonDocument
                {
                    { nameof(Product.Name), 10 },
                    { nameof(Product.Description), 5 }
                }
            };

            var indexModel = new CreateIndexModel<Product>(indexKeys, indexOptions);
            collection.Indexes.CreateOne(indexModel);
        }
    }
}

