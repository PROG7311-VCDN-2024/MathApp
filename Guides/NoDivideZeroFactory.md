## Ensuring no divide by 0 using a Factory Design Pattern

1. In `MathCalculation.cs` make the constructor private:

    ```
    private MathCalculation() { }
    ```

1. Add the following static function to the class. Notice it now has a field for FirebaseUuid:

    ```
    public static MathCalculation Create(decimal? firstNumber, decimal? secondNumber, int? operation, decimal? result, string? firebaseUuid)
    {
        if (operation == 4 && secondNumber == 0)
        {
            throw new ArgumentException("Cannot divide by zero.");
        }

        return new MathCalculation
        {
            FirstNumber = firstNumber,
            SecondNumber = secondNumber,
            Operation = operation,
            Result = result,
            FirebaseUuid = firebaseUuid
        };
    }
    ```