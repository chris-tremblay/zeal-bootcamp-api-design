Week 1: Meet and Greet
Week 2: Domain Models
Week 3: Data Models

Homework:

1. Create Data project



2. Install nuget packages in the data project

```cmd
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

3. Create Entities and Models in the data project

4. Add EF and SQL Lite support for Program.cs

5. Run commands to migrate database In the database project folder

```cmd
dotnet ef migrations add InitialCreate
dotnet ef database update
```

6. Implement Repository