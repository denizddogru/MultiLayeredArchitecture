---

So you have cloned this project to your local but cannot make requests using postman?

DB : Sql Server Management Studio 19

1) Firstly, you need to configure the appsettings in the project to connect to the database instance working on your local computer.
   - Go to visual studio -> view -> sql server object explorer -> choose a database instance
   - Right click on Databases -> Select A Database ( if there are none, you should go to step 2 ) -> Right-Click Properties -> Copy Connection String and replace it with the connection string in appsettings of the   
     application
     
2) Fire up SQL Server Management studio
   - Connect to the server which you gave path in the connection string
   - if no db instance is selected in step 1 ,create a database
  
   - Now your application knows the path to the database and you can use EF Migrations
  
3) Back to Visual Studio to the project ( we want to use EntityFrame to create a migration and add our tables to the newly created Database )
   - Delete the migrations folder
   - Tools -> Package Manager Console
   - In the pop-up screen below, select from Default Project: NLayer-Repository

     Add-Migration InitialMigration ( if this step is successfull the migrations folder will be created in the repository folder)
     Update-Database ( This command will generate Up,Down methods)


     If you have managed to complete these steps, you have successfully connected to your local database and now able to  start making requests from Postman
     
   
   
