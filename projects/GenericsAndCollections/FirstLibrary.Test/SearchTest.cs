namespace FirstLibrary.Test
{
    using FirstLibrary;
    using NUnit.Framework;

    [TestFixture]
    public class SearchTest
    {
        [TestCase(31, -1)]
        [TestCase(-1, -1)]
        [TestCase(0, -1)]
        [TestCase(1, 0)]
        [TestCase(20, 19)]
        public void BinarySearch_ArrayFilledWithValueType_ReturnsCorrectValue(int elementToFind, int expectedResult)
        {
            // arrange
            int[] testArray = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };

            // act
            int result = Search.BinarySearch(testArray, elementToFind);

            // assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("A", -1)]
        [TestCase("b", 1)]
        [TestCase("m", 12)]
        [TestCase("zz", -1)]
        public void BinarySearch_ArrayFilledWhithRefType_ReturnsCorrectValue(string elementToFind, int expectedResult)
        {
            // arrange
            string[] testArray = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "g", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

            // act
            int result = Search.BinarySearch(testArray, elementToFind);

            // assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void BinarySearch_ArrayIsEpmty_ThrowsArgumetsExeption()
        {
            // arrange
            string[] testArray2 = { };
            string message = "";

            // act
            try
            {
                int result = Search.BinarySearch(testArray2, "a");
            }
            catch (ArgumentException ex)
            {
                message = ex.Message;
            }

            // assert
            Assert.That(message, Is.EqualTo("Array is empty"));
        }
    }
}