namespace FirstLibrary.Test
{
    using FirstLibrary;
    using NUnit.Framework;

    [TestFixture]

    public class FibonacciTests
    {
        [Test]
        public void Generate_GenerateCalled_IEnumerableGanerated()
        {
            // arrange
            List<int> expectedCollection = new List<int> { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34 };

            // act
            var testCollection = Fibonacci.Generate().Take(10);

            // assert
            Assert.That(testCollection, Is.EqualTo(expectedCollection));
        }
    }
}