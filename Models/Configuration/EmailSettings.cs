using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Configuration
{
    public class EmailSettings
    {
        public const string KEY = "Email";
        public string Port { get; set; }
        public string Host { get; set; }

        public string From { get; set; }
    }
}
