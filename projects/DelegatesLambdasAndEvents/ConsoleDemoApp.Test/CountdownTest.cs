namespace ConsoleDemoApp.Test
{
    using ConsoleDemoApp;
    using NUnit.Framework;

    [TestFixture]

    internal class CountdownTest
    {
        [Test]
        public void Publish_PublishIsCalled_SubscriptionSucsessfulDueEstablishedAmountOfTime()
        {
            // arrange
            string inTime = string.Empty;
            Countdown countdown = new Countdown();

            // act
            countdown.Subscrubing += (sender, e) => { inTime = e.Message; };

            countdown.Publish(3);
            Thread.Sleep(5000);

            // assert
            Assert.That(inTime, Is.EqualTo(" sucsessfully subscribed"));
        }

        [Test]
        public void Publish_PublishIsCalled_SubscribtionSFailedAfterEstablishedAmountOfTime()
        {
            // arrange
            string notInTime = string.Empty;
            Countdown countdown = new Countdown();

            // act
            countdown.Publish(3);
            Thread.Sleep(5000);

            countdown.Subscrubing += (sender, e) => { notInTime = e.Message; };

            // assert
            Assert.That(notInTime, Is.EqualTo(string.Empty));
        }
    }
}