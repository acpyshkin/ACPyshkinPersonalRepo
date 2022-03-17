﻿namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IEmailSenderService
    {
        public void SendEmail(string email, string subject, string text);
    }
}