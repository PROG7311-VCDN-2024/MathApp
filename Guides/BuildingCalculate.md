## Building out the History Functionality

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

