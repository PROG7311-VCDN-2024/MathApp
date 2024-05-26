using NuGet.ContentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MathApp.Tests.Utils
{
    public class CalculationsUtil
    {
        [Fact]
        public void AddTest()
        {
            int FirstNumber = 5; int SecondNumber = 10;
            decimal? Result = MathApp.Utils.CalculationsUtil.Add(FirstNumber, SecondNumber);

            Assert.Equal(15, Result);
            Assert.NotEqual(0, Result);
        }

        [Fact]
        public void SubtractTest()
        {
            int FirstNumber = 5; int SecondNumber = 10;
            decimal? Result = MathApp.Utils.CalculationsUtil.Subtract(FirstNumber, SecondNumber);

            Assert.Equal(-5, Result);
        }

        [Fact]
        public void MultiplyTest()
        {
            int FirstNumber = 5; int SecondNumber = 10;
            decimal? Result = MathApp.Utils.CalculationsUtil.Multiply(FirstNumber, SecondNumber);

            Assert.Equal(50, Result);
        }

        [Fact]
        public void DivideTest()
        {
            int FirstNumber = 50; int SecondNumber = 10;
            decimal? Result = MathApp.Utils.CalculationsUtil.Divide(FirstNumber, SecondNumber);

            Assert.Equal(5, Result);

            decimal? DivideByZeroResult = MathApp.Utils.CalculationsUtil.Divide(FirstNumber, 0);

            Assert.Equal(0, DivideByZeroResult);

        }
    }
}
