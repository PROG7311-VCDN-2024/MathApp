ALTER TABLE MathCalculations
ADD FirebaseUUID VARCHAR(512);

dotnet ef dbcontext scaffold "Server=labVMH8OX\SQLEXPRESS;Database=Math_DB;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models --force

## Adding Auth to the MathApp
### Setting up a Firebase Project

1. Go to https://console.firebase.google.com/u/0/.
1. Create a new WebApp project called `MathApp`
1. Once created, copy the `apiKey` from the config section. 
1. Add in an system-wide environment variable in Windows named `FirebaseMathApp`. The value of it should be your apiKey. Save your new environment variable.
1. Reboot your VM/PC/laptop.

### Setting up Sessions for your app
1. You do not need to add the Nuget package if using dotnet 8.0.
1. Add in the following to `Program.cs` before `builder.Services.AddControllersWithViews();`:

    ```
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });
    ```
1. Add in the following to `Program.cs` before `app.UseHttpsRedirection();`:
    ```
    app.UseSession();
    ```

### Adding in Firebase Auth
1. Add the following Nuget package: `Firebase.Auth`.
1. Once added, create an `AuthController.cs` and add the following code which will initialize a FirebaseAuthProvider app using the environment variable:

    ```
    FirebaseAuthProvider auth;

    public AuthController()
    {
        auth = new FirebaseAuthProvider(new FirebaseConfig(Environment.GetEnvironmentVariable("FirebaseMathApp")));
    }
    ```

### Adding in Firebase Error Model class
1. When a request is made to Firebase Auth, we need to be able to read and display errrors.
1. Add in these classes under `Models` which we will use later:

    ```
    namespace MathApp.Models
    {
        public class FirebaseErrorModel
        {
            public Error error { get; set; }
        }
    
        public class Error
        {
            public int code { get; set; }
            public string message { get; set; }
            public List<Error> errors { get; set; }
        }
    }
    ```
