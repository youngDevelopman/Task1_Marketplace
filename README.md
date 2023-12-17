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

