namespace FirstLibrary.Test
{
    using FirstLibrary;
    using NUnit.Framework;
    using S = System.Collections.Generic;

    [TestFixture]
    public class QueueTests
    {
        [Test]
        public void Constructor_ConstructorIsEpmpty_CountEqaulsZero()
        {
            // arrange, act
            Queue<int> testQueue = new Queue<int>();

            // assert
            Assert.That(testQueue.Count, Is.EqualTo(0));
        }

        [Test]
        public void Constructor_ConstructorFilledWithCollection_QueueFilledCorrectly()
        {
            // arrange
            List<int> testList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };

            // act
            Queue<int> testQueue = new Queue<int>(testList);

            // assert
            Assert.That(testQueue, Is.EqualTo(testList));
        }

        [Test]
        public void Clear_ClearIsCalled_QueueIsCleared()
        {
            // arrange
            List<int> testList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            Queue<int> testQueue = new Queue<int>(testList);
            S.Queue<int> testSystemQueue = new S.Queue<int>(testList);

            // act
            testQueue.Clear();
            testSystemQueue.Clear();

            // assert
            Assert.That(testSystemQueue, Is.EqualTo(testQueue));
        }

        [TestCase('a', true)]
        [TestCase('B', false)]
        [TestCase('c', true)]
        [TestCase('w', false)]
        public void Contains_ContainsIsCalled_ReturnsCorrectValue(char charToFind, bool isContainsExpected)
        {
            // arrange
            List<char> testList = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'q' };
            Queue<char> testQueue = new Queue<char>(testList);

            // act
            bool isContains = testQueue.Contains(charToFind);

            // assert
            Assert.That(isContains, Is.EqualTo(isContainsExpected));
        }

        [Test]
        public void Peek_QueueIsEmpty_ThrowsInvalidOperationExeption()
        {
            // arrange
            Queue<int> testQueue = new Queue<int>();
            string expectedMessage = "Queue is empty";
            string message = "";

            // act
            try
            {
                int result = testQueue.Peek();
            }
            catch (InvalidOperationException ex)
            {
                message = ex.Message;
            }

            // assert
            Assert.That(message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void Peek_PeekIsCalled_ReturnsCorrectValue()
        {
            // arrange
            List<string> testList = new List<string> { "a", "b", "c", "d", "e", "f", "g", "q" };
            Queue<string> testQueue = new Queue<string>(testList);
            string expectedString = "a";

            // act
            string result = testQueue.Peek();

            // assert
            Assert.That(result, Is.EqualTo(expectedString));
        }

        [Test]
        public void Dequeue_QueueIsEmpty_ThrowsInvalidOperationExeption()
        {
            // arrange
            Queue<int> testQueue = new Queue<int>();
            string expectedMessage = "Queue is empty";
            string message = "";

            // act
            try
            {
                int result = testQueue.Dequeue();
            }
            catch (InvalidOperationException ex)
            {
                message = ex.Message;
            }

            // assert
            Assert.That(message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void Dequeue_DequeueIsCalled_ReturnsCorrectValueAndRemoveIt()
        {
            // arrange
            List<string> testList = new List<string> { "a", "b", "c", "d", "e", "f", "g", "q" };
            Queue<string> testQueue = new Queue<string>(testList);
            S.Queue<string> expectedQueue = new S.Queue<string>(testList);

            // act
            string result = testQueue.Dequeue();
            string expectedResult = expectedQueue.Dequeue();

            // assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(expectedResult));
                Assert.That(testQueue, Is.EqualTo(expectedQueue));
            });
        }

        [Test]
        public void Enqueue_EnqueueIsCalled_CollectionFilledCorrectly()
        {
            // arrange
            Queue<string> testQueue = new Queue<string>();
            S.Queue<string> systemQueue = new S.Queue<string>();

            // act
            testQueue.Enqueue("string");
            testQueue.Enqueue("one");
            testQueue.Enqueue("two");
            systemQueue.Enqueue("string");
            systemQueue.Enqueue("one");
            systemQueue.Enqueue("two");

            // assert
            Assert.That(testQueue, Is.EqualTo(systemQueue));
        }
    }
}