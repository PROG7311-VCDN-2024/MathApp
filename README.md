# Math App in MVC with SQL DB

The purpose of this repo is to outline the steps needed to build a dotnet app which interacts with a SQL DB for basic read/write functionality.

## Basic Features
* User can enter two numbers, select an option and click calculate. Once calculated, the result is to be shown to the user and written to the SQL DB
* User can review previous calculations stored in the DB (history)
* User can clear previous calculations stored in the DB

## Pre-Requisites
* VS or VS Code with Dotnet 8.0
* MS SQL Server installed
* A browser to run everything
* It is recommended that you update your Visual Studio using the VS Installer


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

## Setting up the MathController and Views

1.  Add a `MathController.cs` to your Controllers folder and include the following code (we will add more):
    ```
    public class MathController : Controller
    {
        private readonly MathDbContext _context;

        public MathController(MathDbContext context)
        {
            _context = context;
        }
    }
    ```

    This will allow the controller to interact with the DB using the MathDbContext object.

2. Add a subfolder called `Math` following folder to your Views folder:
    * Calculate - this view will have a form to get inputs and operation, and a label to output the result.
    * History - this view will have a list of operations and a button to clear the history.

## Building out the Calculator

1. Add in the following GET method to your Math Controller to render your view. 
   The method also creates a list of operations that the view will use to generate a dropdown box.

    ```
    public IActionResult Calculate()
    {
        List<SelectListItem> operations = new List<SelectListItem> {
            new SelectListItem { Value = "1", Text = "+" },
            new SelectListItem { Value = "2", Text = "-" },
            new SelectListItem { Value = "3", Text = "**" },
            new SelectListItem { Value = "4", Text = "/" },

            };

        ViewBag.Operations = operations;
    
        return View();
    }
    ```

2. Now we add in the view. In the `Views -> Math`, create a new view called `Create.cshtml`.
   Before you include the following code, make sure you know what it does.
   Where is the dropdown being added? How is the dropdown data passed from controller to the view?
    
    ```
    @model MathApp.Models.MathCalculation

    @{
        ViewData["Title"] = "Calculate";
    }


    <h4>Welcome to the Calculator</h4>

    <div class="row">
        <div class="col-md-4">
            <form asp-action="Calculate">
                <div asp-validation-summary="ModelOnly" class="text-danger"></div>
                <div class="form-group">
                    <label class="control-label">First Number</label>
                    <input asp-for="FirstNumber" class="form-control" />
                    <span asp-validation-for="FirstNumber" class="text-danger"></span>
                </div>
                <div class="form-group">
                    <label class="control-label">Second Number</label>
                    <input asp-for="SecondNumber" class="form-control" />
                    <span asp-validation-for="SecondNumber" class="text-danger"></span>
                </div>
                <div class="form-group">
                    <label class="control-label">Operation</label>
                    <select asp-for="Operation" asp-items="@ViewBag.Operations" class="form-select"></select>
                    <span asp-validation-for="Operation" class="text-danger"></span>
                </div>
            
                <div class="form-group">
                    <input type="submit" value="Calculate" class="btn btn-primary" style="margin-top:30px;" />
                </div>
            </form>

            <div class="form-group">
                @{
                    if (ViewBag.Result != null)
                    {
                        <p>Result is @ViewBag.Result</p>
                    }
                }
            
            </div>
        </div>
    </div>

    @section Scripts {
        @{await Html.RenderPartialAsync("_ValidationScriptsPartial");}
    }

    ```
3. Add in the following POST method to your Math Controller to process input from form submit, calculate, write DB and provide an output.
    ```
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Calculate(decimal? FirstNumber, decimal? SecondNumber,int Operation)
    {
        MathCalculation mathCalculation = new MathCalculation();
        mathCalculation.FirstNumber = FirstNumber;
        mathCalculation.SecondNumber = SecondNumber;
        mathCalculation.Operation = Operation;

        switch (Operation)
        {
            case 1:
                mathCalculation.Result = FirstNumber + SecondNumber;
                break;
            case 2:
                mathCalculation.Result = FirstNumber - SecondNumber;
                break;
            case 3:
                mathCalculation.Result = FirstNumber * SecondNumber;
                break;
            default:
                if (SecondNumber != 0)
                    mathCalculation.Result = FirstNumber / SecondNumber;
                break;
        }

        if (ModelState.IsValid)
        {
            _context.Add(mathCalculation);
            await _context.SaveChangesAsync();
                
        }
        ViewBag.Result = mathCalculation.Result;
        return View();
            
    }
    ```
4. Test your app and check the database to see if successfully written.



## Building out the History Functionality

1. Add the following method to your Math controller to pull all the history from the DB.
    ```
    public async Task<IActionResult> History()
    {
        return View(await _context.MathCalculations.ToListAsync());
    }
    ```
2. Add the following view under `Views -> Math` and name it `History.cstml`. This view shows history and also has a form that will process clearing. Its a single button form with no other input and it simply calls the relevant controller method (we will add this in next.
    ```
    @model IEnumerable<MathApp.Models.MathCalculation>

    @{
        ViewData["Title"] = "History";
    }

    <h4>History</h4>

    <form asp-action="Clear">
    
        <div class="form-group">
            <input type="submit" value="Clear" class="btn btn-primary" style="float:right;" />
        </div>
    </form>

    <table class="table">
        <thead>
            <tr>
                <th>
                    First Number
                </th>
                <th>
                    Operation
                </th>
                <th>
                    Second Number
                </th>
                <th>
                    Result
                </th>
            </tr>
        </thead>
        <tbody>
        @foreach (var item in Model) {
                <tr>
                    <td>
                        @Html.DisplayFor(modelItem => item.FirstNumber)
                    </td>
                    <td>
                        @{
                            switch (item.Operation)
                            {
                                case 1:
                                    <text>+</text>
                                    break;
                                case 2:
                                    <text>-</text>
                                    break;
                                case 3:
                                    <text>*</text>
                                    break;
                                default:
                                    <text>/</text>
                                    break;
                            }
                        }
                    </td>
                    <td>
                
                        @Html.DisplayFor(modelItem => item.SecondNumber)
                    </td>
                    <td>
                        @Html.DisplayFor(modelItem => item.Result)
                    </td>            
                </tr>
        }
        </tbody>
    </table>
    ```
3. Test your app and see that history loads.
4. Now add in this controller method to process clearing history.
    ```
    public IActionResult Clear()
    {
        _context.MathCalculations.RemoveRange(_context.MathCalculations);
        _context.SaveChangesAsync();

        return RedirectToAction("History");
    }
    ```
5. Test that the clear functionality works.
6. Clean up any unused controllers and views in your app.
