using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Identity
{
    public interface IEmailSenderPaperCut
    {
        void SendEmail(string email, string body, string confirmationEmail);
    }
}
