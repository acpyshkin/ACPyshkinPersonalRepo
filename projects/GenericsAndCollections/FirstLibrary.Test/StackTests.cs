namespace FirstLibrary.Test
{
    using System;
    using NUnit.Framework;
    using S = System.Collections.Generic;

    public class StackTests
    {
        [Test]
        public void Constructor_ConstructorIsEpmpty_CountEqaulsZero()
        {
            // arrange, act
            Stack<int> testStack = new Stack<int>();

            // assert
            Assert.That(testStack.Count, Is.EqualTo(0));
        }

        [Test]
        public void Constructor_ConstructorFilledWithCollection_StackFilledCorrectly()
        {
            // arrange
            List<int> testList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };

            // act
            Stack<int> testStack = new Stack<int>(testList);
            S.Stack<int> testSystemStack = new S.Stack<int>(testList);

            // assert
            Assert.That(testStack, Is.EqualTo(testSystemStack));
        }

        [Test]
        public void Clear_ClearIsCalled_StackIsCleared()
        {
            // arrange
            List<int> testList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
            Stack<int> testStack = new Stack<int>(testList);
            S.Stack<int> testSystemStack = new S.Stack<int>(testList);

            // act
            testStack.Clear();
            testSystemStack.Clear();

            // assert
            Assert.That(testSystemStack, Is.EqualTo(testStack));
        }

        [TestCase('a', true)]
        [TestCase('B', false)]
        [TestCase('c', true)]
        [TestCase('w', false)]
        public void Contains_ContainsIsCalled_ReturnsCorrectValue(char charToFind, bool isContainsExpected)
        {
            // arrange
            List<char> testList = new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'q' };
            Stack<char> testStack = new Stack<char>(testList);

            // act
            bool isContains = testStack.Contains(charToFind);

            // assert
            Assert.That(isContains, Is.EqualTo(isContainsExpected));
        }

        [Test]
        public void Push_PushIsCalled_CollectionFilledCorrectly()
        {
            // arrange
            Stack<string> testStack = new Stack<string>();
            S.Stack<string> systemStack = new S.Stack<string>();

            // act
            testStack.Push("string");
            testStack.Push("one");
            testStack.Push("two");
            systemStack.Push("string");
            systemStack.Push("one");
            systemStack.Push("two");

            // assert
            Assert.That(testStack, Is.EqualTo(systemStack));
        }

        [Test]
        public void Pop_StackIsEmpty_ThrowsInvalidOperationExeption()
        {
            // arrange
            Stack<int> testStack = new Stack<int>();
            string expectedMessage = "Stack is empty!";
            string message = "";

            // act
            try
            {
                int result = testStack.Pop();
            }
            catch (InvalidOperationException ex)
            {
                message = ex.Message;
            }

            // assert
            Assert.That(message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void Pop_PopIsCalled_ReturnsCorrectValueAndRemoveIt()
        {
            // arrange
            List<string> testList = new List<string>() { "a", "b", "c", "d", "e", "f", "g", "q" };
            Stack<string> testStack = new Stack<string>(testList);
            S.Stack<string> expectedStack = new S.Stack<string>(testList);

            // act
            string result = testStack.Pop();
            string expectedResult = expectedStack.Pop();

            // assert
            Assert.That(result, Is.EqualTo(expectedResult));
            Assert.That(testStack, Is.EqualTo(expectedStack));
        }

        [Test]
        public void Peek_StackIsEmpty_ThrowsInvalidOperationExeption()
        {
            // arrange
            Stack<int> testStack = new Stack<int>();
            string expectedMessage = "Stack is empty!";
            string message = "";

            // act
            try
            {
                int result = testStack.Peek();
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
            List<string> testList = new List<string>() { "a", "b", "c", "d", "e", "f", "g", "q" };
            Stack<string> testStack = new Stack<string>(testList);
            string expectedString = "q";

            // act
            string result = testStack.Peek();

            // assert
            Assert.That(result, Is.EqualTo(expectedString));
        }
    }
}