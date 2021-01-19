using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChuckNorrisAphorisms.Services
{
    public class EmailSenderOptions
    {
        public string SendGridUser { get; set; }
        public string SendGridEmail { get; set; }
        public string SendGridKey { get; set; }
        public string AdministratorEmail { get; set; }
    }
}
