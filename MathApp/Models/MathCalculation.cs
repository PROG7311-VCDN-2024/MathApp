using System;
using System.Collections.Generic;

namespace MathApp.Models;

public partial class MathCalculation
{
    public int CalculationId { get; set; }

    public decimal? FirstNumber { get; set; }

    public decimal? SecondNumber { get; set; }

    public int? Operation { get; set; }

    public decimal? Result { get; set; }
}
