namespace ConsoleDemoApp
{
    using System;
    public class Countdown
    {
        public void Publish(int secondsToWait)
        {
            var start = DateTime.UtcNow;
            var end = start.AddSeconds(secondsToWait);
            bool IsWaiting = true;
            string message = " sucsessfully subscribed";
            while (IsWaiting)
            {
                TimeSpan remainingTime = end - DateTime.UtcNow;
                if (remainingTime < TimeSpan.Zero)
                {
                    Subscrubing?.Invoke(this, new SubscribingEventArgs(message));
                    IsWaiting = false;
                }
            }
        }

        public event EventHandler<SubscribingEventArgs>? Subscrubing;
    }

    public class SubscribingEventArgs : EventArgs
    {
        public SubscribingEventArgs(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
}