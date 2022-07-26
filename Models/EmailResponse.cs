using Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class EmailResponse
    {
        public Guid Id { get; set; }

        public Email Email { get; set; }

        public EmailStatus Status { get; set; }
    }
}
