using MathApp.Models;

namespace MathApp.Utils
{
    public static class CalculationsUtil
    {
        public static decimal? Add(decimal? FirstNumber, decimal? SecondNumber)
        {
            return FirstNumber + SecondNumber;
        }

        public static decimal? Subtract(decimal? FirstNumber, decimal? SecondNumber)
        {
            return FirstNumber - SecondNumber;
        }

        public static decimal? Multiply(decimal? FirstNumber, decimal? SecondNumber)
        {
            return FirstNumber * SecondNumber;
        }

        public static decimal? Divide(decimal? FirstNumber, decimal? SecondNumber)
        {
            if (SecondNumber == 0)
            {
                return 0;
            }
            return FirstNumber / SecondNumber;
        }
    }
}
