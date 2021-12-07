using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Identity;
using System.Net.Mail;
using Dnp.Net;
using System.Net;

namespace Infrastructure.Identity.Services
{
    public class EmailSenderPaperCut : IEmailSenderPaperCut
    {
        public void SendEmail(string email, string body, string confirmationEmail)
        {

            MailMessage mailMessage = new MailMessage(email, "mihai.nicolae.simedrea@gmail.com", body, confirmationEmail);
            SmtpClient smtpClient = new SmtpClient("localhost");
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Send(mailMessage);
        }
    }
}
