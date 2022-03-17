namespace BuisnessLogic.Test
{
    using System;
    using System.Collections.Generic;
    using Domain;
    using BuisnessLogic;
    using Moq;
    using NUnit.Framework;
    using NLog;

    [TestFixture]
    public class AttendanceSupervisingServiceTests
    {
        [Test]
        public void NotifyIfEligible_StudentSkippedLecturesBelowLimit_StudentAntLectorReceiveEmail()
        {
            // Arrange
            var lectures = new List<LectureModel>()
            {
                new LectureModel { Id = Guid.NewGuid() },
                new LectureModel { Id = Guid.NewGuid() },
                new LectureModel { Id = Guid.NewGuid() },
                new LectureModel { Id = Guid.NewGuid() }
            };
            var lecturer = new LecturerModel() { Id = Guid.NewGuid(), Name = "TestLecturer", Email = "testlecturer@email.com" };
            var course = new CourseModel() { Id = Guid.NewGuid(), AppointedLecturesList = lectures, Lecturer = lecturer };
            var student = new StudentModel()
            {
                Id = Guid.NewGuid(),
                Name = "TestStudent",
                Course = course,
                LectureAttandance = new List<LectureModel>(),
                Email = "teststudent@email.com"
            };

            var emailSender = new Mock<IEmailSenderService>();

            var service = new AttendanceSupervisingService(emailSender.Object);

            // act
            service.NotifyIfEligible(student);

            // assert
            emailSender.Verify(s => s.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void NotifyIfEligible_StudentSkippedLecturesAtLimit_StudentAntLectorNotReceiveEmail()
        {
            // Arrange
            var lectures = new List<LectureModel>()
            {
                new LectureModel { Id = Guid.NewGuid() },
                new LectureModel { Id = Guid.NewGuid() },
                new LectureModel { Id = Guid.NewGuid() }
            };

            var lecturer = new LecturerModel() { Id = Guid.NewGuid(), Name = "TestLecturer", Email = "testlecturer@email.com" };
            var course = new CourseModel() { Id = Guid.NewGuid(), AppointedLecturesList = lectures, Lecturer = lecturer };
            var student = new StudentModel()
            {
                Id = Guid.NewGuid(),
                Name = "TestStudent",
                Course = course,
                LectureAttandance = new List<LectureModel>(),
                Email = "teststudent@email.com"
            };

            var emailSender = new Mock<IEmailSenderService>();
            var studentRepisitory = new Mock<IStudentRepository>();

            var service = new AttendanceSupervisingService(emailSender.Object);

            // act
            service.NotifyIfEligible(student);

            // assert
            emailSender.Verify(s => s.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}