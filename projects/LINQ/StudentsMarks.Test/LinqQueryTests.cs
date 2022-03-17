namespace StudentsMarks.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using StudentsMarks;

    [TestFixture]
    public class LinqQueryTests
    {
        [Test]
        public void Execute_CalledExecuteAllConditionsAreFilled_RerurnsCorrectList()
        {
            // arrange
            string userRequest = "-name Ivan -minmark 1 -maxmark 5 -datefrom 1/4/2020 -dateto 30/1/2021 -test Math -sort mark asc";
            DateTime dateFrom = new DateTime(2020, 4, 1);
            DateTime dateTo = new DateTime(2021, 1, 30);
            var query = TestsList.Where(test => test.Name.Contains("Ivan")
                                              && test.Subject.Contains("Math")
                                              && test.Mark >= 1 && test.Mark <= 5
                                              && test.Date >= dateFrom && test.Date <= dateTo)
                                 .OrderBy(test => test.Mark);
            List<Test> resultTestsList = new List<Test>();

            // act
            resultTestsList = LinqQuery.Execute(TestsList, userRequest).ToList();
            List<Test> listToCompare = query.ToList();

            // assert
            Assert.AreEqual(resultTestsList, listToCompare);
        }

        [Test]
        public void Execute_CalledExecuteNameConditionsAndSordByDateDesc_RerurnsCorrectList()
        {
            // arrange
            string userRequest = "-name Ivan -sort date desc";
            var query = TestsList.Where(test => test.Name.Contains("Ivan"))
                                 .OrderByDescending(test => test.Date);
            List<Test> resultTestsList = new List<Test>();

            // act
            resultTestsList = LinqQuery.Execute(TestsList, userRequest).ToList();
            List<Test> listToCompare = query.ToList();

            // assert
            Assert.AreEqual(resultTestsList, listToCompare);
        }

        private static readonly List<Test> TestsList = new List<Test>
        {
            new Test("Vasiliy Mutik", "Math", "17/05/2020", 2),
            new Test("Ivan Deq", "Math", "01/05/2020", 2),
            new Test("Ivan Karyaka", "English Language", "30/11/2020", 5),
            new Test("Marina Malina", "English Language", "30/10/2020", 4),
            new Test("Vasiliy Mutik", "History", "15/12/2020", 4),
            new Test("Stepan Joska", "History", "15/01/2021", 3),
            new Test("Ivan Karyaka", "PE", "05/01/2021", 5),
            new Test("Ivan Deq", "PE", "01/12/2020", 3)
        };
    }
}