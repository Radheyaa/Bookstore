using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public class EmailConfirmModel
    {
        public string email { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool EmailSent { get; set; }

        public bool EmailVerified { get; set; }
    }
}
