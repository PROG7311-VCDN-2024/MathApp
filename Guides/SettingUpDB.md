## Setting up the DB
### SQL Queries used

After creating a DB called `Math_DB` in SQL Server, this query was used to create the table:
```
CREATE TABLE MathCalculations (
    CalculationID INT PRIMARY KEY IDENTITY(1,1),
    FirstNumber DECIMAL(18, 2),
    SecondNumber DECIMAL(18, 2),
    Operation INT,
    Result DECIMAL(18, 2)
);
```

For operation, the following indexes apply:
1. Addition
1. Subtraction
1. Multiplication
1. Division

#### Optional 
DB can be seeded with preliminary data to test all is ok.
```
INSERT INTO MathCalculations (FirstNumber, SecondNumber, Operation, Result)
VALUES (10, 5, 1, 15); -- 1 represents addition

SELECT * FROM MathCalculations
```