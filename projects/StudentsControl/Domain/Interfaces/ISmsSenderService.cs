namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ISmsSenderService
    {
        public void SendSms(int phonenumber, string text);
    }
}