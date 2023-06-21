using Microsoft.QualityTools.Testing.Fakes;

namespace Calculator.Tests
{
    public class CalcTests
    {
        [Fact]
        public void Sum_2_and_3_results_5()
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(2, 3);

            //Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void Sum_0_and_0_results_0()
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(0, 0);

            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Sum_MAX_and_1_throws_OverflowsException()
        {
            var calc = new Calc();

            Assert.Throws<OverflowException>(() => calc.Sum(int.MaxValue, 1));
        }


        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 1, 2)]
        [InlineData(-1, 2, 1)]
        [InlineData(-1, -2, -3)]
        public void Sum_with_results(int a, int b, int exp)
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(a, b);

            //Assert
            Assert.Equal(exp, result);
        }

        [Fact]
        public void IsWeekend()
        {
            var calc = new Calc();

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2023, 6, 19);
                Assert.False(calc.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2023, 6, 20);
                Assert.False(calc.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2023, 6, 21);
                Assert.False(calc.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2023, 6, 22);
                Assert.False(calc.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2023, 6, 23);
                Assert.False(calc.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2023, 6, 24);
                Assert.True(calc.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2023, 6, 25);
                Assert.True(calc.IsWeekend());
            }
        }
    }
}