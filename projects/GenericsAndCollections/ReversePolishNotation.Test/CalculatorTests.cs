namespace ReversePolishNotation.Test
{
    using NUnit.Framework;
    using ReversePolishNotation;
    [TestFixture]
    public class CalculastorTests
    {
        [Test]
        public void Calculate_WrongExpression_ThrowsArgumentExeption()
        {
            // arrange
            string expression = "String";
            string expectedMessage = "";

            // act
            try
            {
                decimal result = RPNCalculator.Calculate(expression);
            }
            catch (ArgumentException ex)
            {
                expectedMessage = ex.Message;
            }

            // assert
            Assert.That(expectedMessage, Is.EqualTo("Wrong request"));
        }

        [Test]
        public void Calculate_WrongSymbolExpression_ThrowsArgumentExeption()
        {
            // arrange
            string expression = "5 4  wrong";
            string expectedMessage = "";

            // act
            try
            {
                decimal result = RPNCalculator.Calculate(expression);
            }
            catch (ArgumentException ex)
            {
                expectedMessage = ex.Message;
            }

            // assert
            Assert.That(expectedMessage, Is.EqualTo("Wrong operand or operator"));
        }

        [Test]
        public void Calculate_DivideByZero_ThrowsDivideByZeroExeption()
        {
            // arrange
            string expression = "5 1 2 + 4 * + 0 /";
            string expectedMessage = "";

            // act
            try
            {
                decimal result = RPNCalculator.Calculate(expression);
            }
            catch (DivideByZeroException ex)
            {
                expectedMessage = ex.Message;
            }

            // assert
            Assert.That(expectedMessage, Is.EqualTo("Cannot divide by zero"));
        }

        [TestCase("5 1 2 + 4 * + 3 -", 14)]
        [TestCase("3 6 - 2 1 + *", -9)]
        public void Calculate_CalculateIsCalled_ReturnsCorrectValue(string expression, decimal expectedResult)
        {
            // arrange, act
            decimal result = RPNCalculator.Calculate(expression);

            // assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}