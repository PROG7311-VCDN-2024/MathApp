## Amending DB to Accept the UUID

1. We need to add in a field to our DB to accommodate this change for our schema:


```
ALTER TABLE MathCalculations
ADD FirebaseUUID VARCHAR(512);
```

1. Then force a rebuild of the model using `--force`

```
dotnet ef dbcontext scaffold "Server=labVMH8OX\SQLEXPRESS;Database=Math_DB;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models --force
```

1. Remember to remove `OnConfiguring()` like we did before!