Week 1: Meet and Greet
Week 2: Domain Models
Week 3: Data Models

Homework:

1. Create Data project



2. Install nuget packages in the data project

```cmd
dotnet add package Microsoft.EntityFrameworkCore.Relational
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

3. Create Entities and Models in the data project

4. Add EF and SQL Server support for Program.cs

5. Run commands to migrate database In the database project folder

```cmd
dotnet ef migrations add InitialCreate
dotnet ef database update
```

6. Add this to your domain project:

```xml
<ItemGroup>
	<InternalsVisibleTo Include="{Add your data project name here}" />
</ItemGroup>
```

7. Implement Repository


Week 4: Implement OData (CQRS Intro)

1. Install nuget package:
```cmd
dotnet add package Microsoft.AspNetCore.OData
```

2. Create Data\Queries folder in your Application project.

3. Create queries / models here.

4. Implement the Query in a concrete class in the Data project.

5. Register your query in the applicaiton services.

6. Create an OData Controller

7. Call your Query from the OData controller
	a. Add [EnableQuery] attribute to your endpoint.
	b. Add [Route("odata")] attribute to your controller

8. Add ODataConfiguration class to your API project.
```csharp
services.AddOData(opt =>
{
    opt
        .AddRouteComponents("odata", ODataConfiguration.GetEdmModel())
        .EnableQueryFeatures(100);
    opt.TimeZone = TimeZoneInfo.Utc;
})
```

Week 5:
1. Create Models\Dto Folder in Application project.

2. Create flattened models in this folder to abstract data layer.
