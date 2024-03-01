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