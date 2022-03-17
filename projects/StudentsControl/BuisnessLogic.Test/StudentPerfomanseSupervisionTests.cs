using System;
using System.Collections.Generic;
using Domain;
using BuisnessLogic;
using Moq;
using NUnit.Framework;

namespace BuisnessLogic.Test
{
    [TestFixture]
    public class StudentPerfomanseSupervisionTests
    {
        [Test]
        public void NotifyIfNessesary_AverageMarkBelowLimit_StudentReceiveSms()
        {
            // Arrange
            var homeWorks = new List<HomeWorkModel>()
            {
                new HomeWorkModel() { Id = Guid.NewGuid(), Mark = 5 },
                new HomeWorkModel() { Id = Guid.NewGuid(), Mark = 4 },
                new HomeWorkModel() { Id = Guid.NewGuid(), Mark = 2 },
            };
            var testStudent = new StudentModel()
            {
                Id = Guid.NewGuid(),
                Name = "Test Name",
                PhoneNumber = 1112223344,
                AppointedHomeWorksList = homeWorks
            };
            var smsSender = new Mock<ISmsSenderService>();
            var service = new StudentPerformanceSupervision(smsSender.Object);

            // act
            service.NotifyIfNecessary(testStudent);

            // Assert
            smsSender.Verify(s => s.SendSms(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void NotifyIfNecessary_AvarageMarkAtLimitValue_SmsNotSent()
        {
            var homeWorks = new List<HomeWorkModel>()
            {
                new HomeWorkModel() { Id = Guid.NewGuid(), Mark = 5 },
                new HomeWorkModel() { Id = Guid.NewGuid(), Mark = 4 },
                new HomeWorkModel() { Id = Guid.NewGuid(), Mark = 3 },
            };
            var testStudent = new StudentModel()
            {
                Id = Guid.NewGuid(),
                Name = "Test Name",
                PhoneNumber = 1112223344,
                AppointedHomeWorksList = homeWorks
            };
            var smsSender = new Mock<ISmsSenderService>();
            var service = new StudentPerformanceSupervision(smsSender.Object);

            // act
            service.NotifyIfNecessary(testStudent);

            // Assert
            smsSender.Verify(s => s.SendSms(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
        }
    }
}