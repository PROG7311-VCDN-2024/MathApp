## Logging auth errors with Singleton Design Patter

1. Add in an `AuthLogger.cs` in a new folder called `Utils` in the project.
1. Add the following code into the `AuthLogger.cs`:

    ```
    namespace MathApp.Utils
    {
        public sealed class AuthLogger
        {
            private static readonly AuthLogger instance = new AuthLogger();
            private readonly StreamWriter fileWriter;

            static AuthLogger() { }

            private AuthLogger()
            {
                fileWriter = new StreamWriter("auth_errors.log", true);
                fileWriter.AutoFlush = true;
            }

            public static AuthLogger Instance
            {
                get
                {
                    return instance;
                }
            }

            public void LogError(string errorMessage)
            {
                string logMessage = $"{DateTime.Now} - ERROR: {errorMessage}";
                fileWriter.WriteLine(logMessage);
            }
        }
    }
    ```
1. Add in the following to the `AuthController` under the Login POST action before the `return View(login);`:

    ```    
    Utils.AuthLogger.Instance.LogError(firebaseEx.error.message + " - User: " + login.Email + " - IP: " + HttpContext.Connection.RemoteIpAddress
        + " - Browser: " + Request.Headers.UserAgent);
    ```
