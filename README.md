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

**Features and Requirements Compliance**

Data Structure: The application contains sSer and Product entities.
CRUD Functionality: Users can be registered and logged-in. Products can be added and viewed.
Angular Front-End: Angular framework is used for the frontend part. CRUD functionality is available via frontend(except user registration)
MongoDB Integration: Users and Products are stored in MongoDb in separate collections. Also, the product search bar feature was added that utilizes the compound text search feature of MongoDB.
GDPR Consideration: A Cookie banner was added to the frontend(click on the black rectangle on left bottom of the screen in order to show the GDRP banner). If accepted the amount of product page views will be tracked and stored in the cookies. Once disabled, the amount won't be tracked.


