using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System;
using System.Collections.Generic;

namespace MathApp.Models;

public partial class MathCalculation
{
    private MathCalculation() { }

    public int CalculationId { get; set; }

    public decimal? FirstNumber { get; set; }

    public decimal? SecondNumber { get; set; }

    public int? Operation { get; set; }

    public decimal? Result { get; set; }

    public string? FirebaseUuid { get; set; }

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
}
