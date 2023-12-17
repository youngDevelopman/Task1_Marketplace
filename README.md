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
When the application is started it will do the following:
1. Seed MongoDB Database(Add Users, Products and indexes)
2. Run ASP.NET Core instance
3. Run the Angular app as a proxy

This way, the configuration of the app is as easy as possible.

**Features and Requirements Compliance**

Data Structure: The application contains User and Product entities.

CRUD Functionality: Users can be registered and logged in. Products can be added and viewed.

Angular Front-End: Angular framework is used for the frontend part. CRUD functionality is available via frontend(except for user registration)

MongoDB Integration: Users and Products are stored in MongoDB in separate collections. Also, the product search bar feature was added that utilizes the compound text search feature of MongoDB.

GDPR Consideration: A Cookie banner was added to the frontend(click on the black rectangle on the left bottom of the screen in order to show the GDRP banner). If accepted the amount of product page views will be tracked and stored in the cookies. Once disabled, the amount won't be tracked.

Here are some of the images of the application:
![image](https://github.com/youngDevelopman/Task1_Marketplace/assets/31933374/39b003f2-a5f2-4778-b0cf-cb7f42279f82)
![image](https://github.com/youngDevelopman/Task1_Marketplace/assets/31933374/e951fa4a-11eb-42db-9911-204280d77f2f)
![image](https://github.com/youngDevelopman/Task1_Marketplace/assets/31933374/d9686d51-8896-49bc-ae87-4584a85339e4)
![image](https://github.com/youngDevelopman/Task1_Marketplace/assets/31933374/f5f8aa67-646b-4551-8f66-c14f07e7ecf9)
![image](https://github.com/youngDevelopman/Task1_Marketplace/assets/31933374/934e2264-e2f1-4be6-9437-b9329a769c2e)
![image](https://github.com/youngDevelopman/Task1_Marketplace/assets/31933374/c00c451d-16d1-44f3-9c81-c8483cb69519)
![image](https://github.com/youngDevelopman/Task1_Marketplace/assets/31933374/83c28145-da21-48f7-9a4e-6cc2c2cbbced)


