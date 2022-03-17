namespace BuisnessLogic
{
    using System;
    using System.Collections.Generic;
    using Domain;

    public class AttendanceSupervisingService : IAttendadanceSupervisingServise
    {
        private readonly IEmailSenderService _emailSenderService;

        private const int SkipsLimit = 3;

        public AttendanceSupervisingService(IEmailSenderService emailSender)
        {
            _emailSenderService = emailSender;
        }

        public void NotifyIfEligible(StudentModel student)
        {
            // TODO add bool flag at LectureEntity(Model and DTO) which will show whether the lecture passed or not.It would help to build full schedule in advance for entire course.
            // TODO add bool flag at StudentEntity(Madel and DTO) which will show whether student has been already notified, or time limitation between messages. It will prevent miltiple letters sent when editing database.
            var attendedLectures = student.LectureAttandance;
            var appointedLectures = student.Course.AppointedLecturesList;

            int skippedLections = appointedLectures.Count - attendedLectures.Count;
            if (skippedLections > SkipsLimit)
            {
                Notify(student);
            }
        }

        public void NotifyIfEligible(IReadOnlyCollection<StudentModel> students)
        {
            foreach (var student in students)
            {
                NotifyIfEligible(student);
            }
        }

        public void NotifyIfEligible(ICollection<StudentModel> students)
        {
            foreach (var student in students)
            {
                NotifyIfEligible(student);
            }
        }

        private void Notify(StudentModel studentToNotify)
        {
            if (studentToNotify.Email != null)
            {
                string subject = "Attendace notification";
                string message = $"{studentToNotify.Name}, you have skipped more than {SkipsLimit} lectures.";
                _emailSenderService.SendEmail(studentToNotify.Email, subject, message);
            }

            var lecturer = studentToNotify.Course.Lecturer;
            if (lecturer.Email != null)
            {
                string subject = $"Student {studentToNotify.Name} attendace notification";
                string message = $"{studentToNotify.Name} have skipped more than {SkipsLimit} lectures.";
                _emailSenderService.SendEmail(studentToNotify.Email, subject, message);
            }
        }
    }
}