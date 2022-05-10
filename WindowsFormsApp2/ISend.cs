using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tulpep.NotificationWindow;
using System.Net.Mail;
using System.Net;

namespace WindowsFormsApp2
{
    internal interface ISend
    {
        void SendMessage();
    }
    internal class SendPopit: ISend
    {
        PopupNotifier pop;
        Deal deal;
        public SendPopit(PopupNotifier pop, Deal deal)
        {
            this.pop = pop;
            this.deal = deal;
        }
        public void SendMessage()
        {
            pop.ContentText = "Истекает срок выполнения задачи: " + deal.Text + "!";
            pop.Popup();
        }
         
    }
    internal class SendMail : ISend
    {
        Deal deal;
        string email;
        public SendMail( Deal deal, string email)
        {
            this.deal = deal;
            this.email = email;
        }
        public void SendMessage()
        {
            try
            {
                string from = @"hludnev.1999@yandex.ru";
                string pass = "basta77732";
                MailMessage mess = new MailMessage();
                mess.To.Add(@email);
                mess.From = new MailAddress(from);
                mess.Subject = "ToDoList Оповещение";
                mess.SubjectEncoding = Encoding.UTF8;
                mess.Priority = MailPriority.High;
                mess.Body = "Истек срок выполнения задания " + deal.Text + "!";
                mess.BodyEncoding = Encoding.UTF8;
                
                
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.yandex.ru";
                client.Port = 25;
                client.EnableSsl = true;
                
                client.Credentials = new NetworkCredential("hludnev.1999", pass);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mess);
                mess.Dispose();
            }
            catch (Exception e)
            {
                throw new Exception("Mail.Send: " + e.Message);
            }
        }

    }

}
