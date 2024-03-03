## Amending customising the menu buttons

1. Ensure that your menu code in `Views` -> `Shared` -> `_Layout.cshtml` matches the below:

    ```
    <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
        <ul class="navbar-nav flex-grow-1">
            <li class="nav-item">
                <a class="nav-link text-dark" asp-area="" asp-controller="Math" asp-action="Calculate">Calculate</a>
            </li>
            <li class="nav-item">
                <a class="nav-link text-dark" asp-area="" asp-controller="Math" asp-action="History">History</a>
            </li>
        </ul>
        <ul class="navbar-nav">
            @{
                if (Context.Session.GetString("currentUser") == null)
                {
                    <li class="nav-item">
                        <a class="nav-link text-dark" asp-area="" asp-controller="Auth" asp-action="Login">Login</a>
                    </li>
                } else
                {
                    <li class="nav-item">
                        <a class="nav-link text-dark" asp-area="" asp-controller="Auth" asp-action="Logout">Logout</a>
                    </li>                                
                }
            }
        </ul>
    </div>
    ```