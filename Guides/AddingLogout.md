## Building Logout
1. Add in the following to the `AuthController`:
    ```
    [HttpGet]
    public IActionResult LogOut()
    {
        HttpContext.Session.Remove("currentUser");
        return RedirectToAction("Login");
    }
    ```
1. Notice how we do not have a view for this, but we just send the user back to the login page.