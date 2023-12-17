# Task1_Marketplace

**Setup**

Provide connection string for MongoDb instance in the appsettings.Development.json file
```
"Database": {
  "ConnectionString": "*you connection string goes here*" // mongodb://localhost:27017 is a default connection string for MongoDB,
  "DatabaseName": "Task1_Marketplace",
  "Collections": {
    "Users": "Users",
    "Products":  "Products"
  }
}
```

Then, run ```dotnet run``` or ```ctrl + f5``` in Visual Studio(preferred)
When the application has started it does the following:
1. Seed MongoDB Database(Add Users, Products and indexes)
2. Run ASP.NET Core instance
3. Run the Angular app as a proxy

This way, the configuration of the app is as easy as possible.




