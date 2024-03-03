## Amending the code to cater for calculations per user

1. Change the `Calculate` action to accommodate the change:
    ```
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Calculate(decimal? FirstNumber, decimal? SecondNumber,int Operation)
    {
        var token = HttpContext.Session.GetString("currentUser");

        if (token == null)
        {
            return RedirectToAction("Login", "Auth");
        }

        decimal? Result = 0;
        MathCalculation mathCalculation;

        try
        {
            mathCalculation = MathCalculation.Create(FirstNumber, SecondNumber, Operation, Result, token);
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View();
            throw;
        }
        

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

        // return RedirectToAction("Calculate");
        
    }
    ```
1. Change the `History` action to show history for that user:

    ```
    public async Task<IActionResult> History()
    {
        var token = HttpContext.Session.GetString("currentUser");

        if (token == null)
        {
            return RedirectToAction("Login", "Auth");
        }

        return View(await _context.MathCalculations.Where(m => m.FirebaseUuid.Equals(token)).ToListAsync());
    }
    ```

1. Change the `Clear` action to only clear history for that user:
    ```
    public IActionResult Clear()
    {
        var token = HttpContext.Session.GetString("currentUser");

        if (token == null)
        {
            return RedirectToAction("Login", "Auth");
        }

        _context.MathCalculations.RemoveRange(_context.MathCalculations.Where(m => m.FirebaseUuid.Equals(token)));
        _context.SaveChangesAsync();

        return RedirectToAction("History");
    }
    ```