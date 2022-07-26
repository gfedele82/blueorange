using Models;
using Models.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.DTOAdapters
{
    public static class EmailAdapter
    {
        public static Schema.DBEmail ToDBModel (this Email email)
        {
            if (email == null)
                return null;

            return new Schema.DBEmail
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                Status = EmailStatus.Registered.ToString(),
                Email = JsonConvert.SerializeObject(email)
            };

        }

        public static Email ToEmailDTO(this Schema.DBEmail dBEmail)
        {
            if (dBEmail == null || dBEmail.Email == null)
                return null;

            return JsonConvert.DeserializeObject<Email>(dBEmail.Email);
        }
    }
}
