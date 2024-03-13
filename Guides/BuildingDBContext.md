
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

### Adding the Connection String in Environment variables - OPTION 1 (in the code)

1. Add the connection string for your SQL Server inside a system-wide environment variable named `Math_DB`.

2. Add the following code to your Program.cs class after `builder.Services.AddControllersWithViews();`:
    ```
    builder.Services.AddDbContext<MathDbContext>(options =>
                    options.UseSqlServer(Environment.GetEnvironmentVariable("Math_DB")));
    ```

### Adding Connection String in appsettings.json - OPTION 2 (was in the code previously)

1. Add the following section to your appsettings.json (remember to update):
    ```
      "ConnectionStrings": {
        "Math_DB": "Server=labVMH8OX\\SQLEXPRESS;Database=Math_DB;Trusted_Connection=True;TrustServerCertificate=True;"
      },
    ```

1. Add the following code to your Program.cs class after `builder.Services.AddControllersWithViews();`:
    ```
    builder.Services.AddDbContext<MathDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("Math_DB")));
    ```

