using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.MailServices
{
    class SystemSupportMail:MasterMailServer
    {

        public SystemSupportMail()
        {
            senderMail = "jimmyrmzrdz@gmail.com";
            password = "Jamesroot8099";
            host = "smtp.gmail.com";
            port = 587;
            ssl = true;
            initializeSmtpClient();
        
        }
    }
}
