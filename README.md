# Math App in MVC with SQL DB

The purpose of this repo is to outline the steps needed to build a dotnet app which interacts with a SQL DB for basic read/write functionality.

## Basic Features
* User can enter two numbers, select an option and click calculate. Once calculated, the result is show to the user and written to the SQL DB
* User can review previous calculations stored in the DB
* User can clear previous calculations stored in the DB

## Pre-Requisites
* VS or VS Code with Dotnet 8.0
* MS SQL Server installed
* A browser to run everything
* Update your Visual Studio using the VS Installer


## SQL Queries used

After creating a DB called `Math_DB` in SQL Server, this query was used to create the table:
```
CREATE TABLE MathCalculations (
    CalculationID INT PRIMARY KEY IDENTITY(1,1),
    FirstNumber DECIMAL(18, 2),
    SecondNumber DECIMAL(18, 2),
    Operation INT,
    Result DECIMAL(18, 2)
);
```

For operation, the following indexes apply:
1. Addition
1. Subtraction
1. Multiplication
1. Division

#### Optional 
DB can be seeded with preliminary data to test all is ok.
```
INSERT INTO MathCalculations (FirstNumber, SecondNumber, Operation, Result)
VALUES (10, 5, 1, 15); -- 1 represents addition

SELECT * FROM MathCalculations
```

## Creating an MVC Web App
After creating a MVC Web App in .NET 8.0 (no auth, no docker), the app was run to install and accept certificates.
Double checked it runs ok.

## Connecting the App to the DB

Using the following commands in the Developer Command Prompt, connect the app to your DB.

1. Install dotnet-ef
```
dotnet tool install --global dotnet-ef
```

2. Install these packages:
```
dotnet add package Microsoft.EntityFrameworkCore.Design 
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

3. Run the scaffold command, after adjusting for your server and database:
```
dotnet ef dbcontext scaffold "Server=labVMH8OX\SQLEXPRESS;Database=Math_DB;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models
```

4. Once the model is created, you will need to move the connection string out of the context class into appsetting.json and will need to setup the service in Program.cs.
 
Remove the following from the DB context class:
```
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=labVMH8OX\\SQLEXPRESS;Database=Math_DB;Trusted_Connection=True;TrustServerCertificate=True;");
```


5. Add the following section to your appsettings.json (remember to update):
```
  "ConnectionStrings": {
    "Math_DB": "Server=labVMH8OX\\SQLEXPRESS;Database=Math_DB;Trusted_Connection=True;TrustServerCertificate=True;"
  },
```

6. Add the following code to your Program.cs class after `builder.Services.AddControllersWithViews();`:
```
builder.Services.AddDbContext<MathDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Math_DB")));
```







