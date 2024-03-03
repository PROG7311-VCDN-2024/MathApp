### Checking for Session variable
1. When login is successful, we ceated a session variable called `currentUser`.
1. Add this into any controller action that needs a user to be logged in (including `GET` and `POST` of the `Calculate`, `History` and `Clear` actions):

    ```
    var token = HttpContext.Session.GetString("currentUser");

    if (token == null)
    {
        return RedirectToAction("Login", "Auth");
    }
    ```