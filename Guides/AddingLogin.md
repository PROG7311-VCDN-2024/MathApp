## Building out Login

1. Add in the following to the `AuthController`:
    ```
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel login)
    {
        try
        {
            var fbAuthLink = await auth.SignInWithEmailAndPasswordAsync(login.Email, login.Password);
            string currentUserId = fbAuthLink.User.LocalId;
            
            if (currentUserId != null)
            {
                HttpContext.Session.SetString("currentUser", currentUserId);
                return RedirectToAction("Calculate","Math");
            }

        }
        catch (FirebaseAuthException ex)
        {
            var firebaseEx = JsonConvert.DeserializeObject<FirebaseErrorModel>(ex.ResponseData);
            ModelState.AddModelError(String.Empty, firebaseEx.error.message);
            Utils.AuthLogger.Instance.LogError(firebaseEx.error.message + " - User: " + login.Email + " - IP: " + HttpContext.Connection.RemoteIpAddress
                + " - Browser: " + Request.Headers.UserAgent);
            return View(login);
        }

        return View();
    }
    ```
1. Add in an `Auth` folder in Views and add a `Login.cshtml` view to the folder with the following code:
    ```
    @model MathApp.Models.LoginModel

    @{
        ViewData["Title"] = "Login";
    }

    <h4>Login</h4>

    <div class="row">
        <div class="col-md-4">
            <form asp-action="Login">
                <div asp-validation-summary="All" class="text-danger"></div>
                <div class="form-group">
                    <label asp-for="Email" class="control-label"></label>
                    <input asp-for="Email" class="form-control" />
                    <span asp-validation-for="Email" class="text-danger"></span>
                </div>
                <div class="form-group">
                    <label asp-for="Password" class="control-label"></label>
                    <input asp-for="Password" class="form-control" type="password" />
                    <span asp-validation-for="Password" class="text-danger"></span>
                </div>
                <div class="form-group">
                    <input type="submit" value="Login" class="btn btn-primary" style="margin-top:30px;" />
                </div>
            </form>
        </div>
    </div>

    <div>
        <a asp-action="Register">Register</a>
    </div>

    @section Scripts {
        @{await Html.RenderPartialAsync("_ValidationScriptsPartial");}
    }
    ```
1. Test out your login.

