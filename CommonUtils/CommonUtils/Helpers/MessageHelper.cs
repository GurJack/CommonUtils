using System;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace CommonUtils.Helpers
{
    public static class MessageHelper
    {
        public static string SendMessage(string mailHostAddress, string fromAddress, string toAddress, string subject, string message, bool isBodyHtml)
        {
            try
            {
                using (var smpt = new SmtpClient(mailHostAddress))
                {
                    //var credential = new NetworkCredential(«username», «password»);
                    //smpt.Credentials = credential;

                    var mail = new MailMessage();
                    foreach (var str in toAddress.Split(';').Select(p => new MailAddress(p)).ToArray())
                        mail.To.Add(str);

                    mail.From = new MailAddress(fromAddress);

                    mail.IsBodyHtml = isBodyHtml;
                    //mail.AlternateViews = new AlternateView()
                    mail.Body = message;
                    mail.BodyEncoding = Encoding.UTF8;

                    mail.Subject = subject;
                    mail.SubjectEncoding = Encoding.UTF8;


                    smpt.Send(mail);
                    return "";
                }

            }
            catch (Exception exc)
            {

                return exc.Message;
            }
        }
    }
}