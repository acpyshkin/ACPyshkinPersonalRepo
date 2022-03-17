namespace StudentsMarks
{
    using System;
    using System.Text.Json;

    public class Program
    {
        public static void Main()
        {
            var tests = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tests.json"));

            if (tests == null)
            {
                throw new ArgumentNullException(nameof(tests));
            }

            List<Test> testsList = new List<Test>();
            try
            {
                testsList = JsonSerializer.Deserialize<List<Test>>(tests) ?? throw new ArgumentNullException("Null at Deserialaze");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (JsonException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (testsList == null)
            {
                throw new ArgumentNullException("Test list is empty");
            }

            string request = "-math -sort mark desc";
            List<Test> resultTestsList = new List<Test>();
            try
            {
                resultTestsList = LinqQuery.Execute(testsList, request).ToList();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Student Test DateMark");
            foreach (Test test in resultTestsList)
            {
                Console.WriteLine(test.ToString());
            }

            Console.ReadKey(true);
        }
    }
}