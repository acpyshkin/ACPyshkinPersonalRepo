namespace BuisnessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain;

    public class StudentPerformanceSupervision : IStudentPerformanceSupervision
    {
        private readonly ISmsSenderService _smsSender;

        private const double MarksLimit = 4;

        public StudentPerformanceSupervision(ISmsSenderService smsSender)
        {
            _smsSender = smsSender;
        }

        public void NotifyIfNecessary(StudentModel student)
        {
            if (student == null)
            {
                return;
            }

            var homeworks = student.AppointedHomeWorksList;
            double averageStudentMark = homeworks.Select(h => h.Mark).Average();
            if (averageStudentMark < MarksLimit)
            {
                Notify(student);
            }
        }

        public void NotifyIfNecessary(IReadOnlyCollection<StudentModel> students)
        {
            foreach (StudentModel student in students)
            {
                NotifyIfNecessary(student);
            }
        }

        public void NotifyIfNecessary(ICollection<StudentModel> students)
        {
            foreach (StudentModel student in students)
            {
                NotifyIfNecessary(student);
            }
        }

        private void Notify(StudentModel studentToNotify)
        {
            if (studentToNotify.PhoneNumber != default)
            {
                string message = $"Warning! Your avarage grade is lover than {MarksLimit}";
                _smsSender.SendSms(studentToNotify.PhoneNumber, message);
            }
        }
    }
}