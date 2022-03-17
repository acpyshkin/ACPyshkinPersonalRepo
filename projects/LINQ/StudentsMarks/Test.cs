namespace StudentsMarks
{
    using System.Text.Json.Serialization;
    public class Test
    {
        [JsonConstructor]
        public Test(string name, string subject, DateTime date, int mark)
        {
            if (name != null && name != string.Empty)
            {
                Name = name;
            }
            else
            {
                throw new ArgumentNullException("name is null or empty");
            }

            if (name != null && name != string.Empty)
            {
                Subject = subject;
            }
            else
            {
                throw new ArgumentNullException("subject is null or epmty");
            }

            Date = date;
            if (mark > 0 && mark <= 5)
            {
                Mark = mark;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Mark value is out of range (1-5)");
            }
        }

        public Test(string name, string subject, string date, int mark)
        {
            Name = name;
            Subject = subject;
            Date = DateTime.ParseExact(date, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            Mark = mark;
        }

        public string Name { get; }

        public string Subject { get; }

        public DateTime Date { get; }

        public int Mark { get; }
        public override string ToString()
        {
            return $"{Name} {Subject} {Date.ToString("d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)} {Mark}";
        }
    }
}