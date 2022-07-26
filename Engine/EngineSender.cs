using Contracts.Engine;
using DataAccess.Interfaces;
using DataAccess.Schema;
using DataAccess.DTOAdapters;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Models;
using Models.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;


namespace Engine
{
    public class EngineSender : IEngineSender
    {
        private readonly IEmailRepository repository;
        private readonly ILogger<EngineSender> logger;
        private readonly IValidator<Email> validator;
        private readonly EmailSettings emailSettings;

        public EngineSender(
            IEmailRepository _repository,
            ILogger<EngineSender> _logger,
            IValidator<Email> _validator,
            EmailSettings _emailSettings)
        {
            repository = _repository;
            logger = _logger;
            validator = _validator;
            emailSettings = _emailSettings;
        }
        public async Task<EmailResponse> ProcessSender(Email email)
        {
            await validator.ValidateAsync(email).ConfigureAwait(false);
            EmailResponse resonse = new EmailResponse();

            SmtpClient smtp = SetConfiguration();

            MailMessage mail = SetMail(email);

            DBEmail dbemail = email.ToDBModel();

            dbemail = await repository.SaveOrUpdateAsync(dbemail);


            if (mail == null)
            {
                logger.LogError($"Error to send email {email.Subject} to {email.To}");
                resonse.Status = Models.Enum.EmailStatus.Failed;
            }
            else
            {
                smtp.Send(mail);
                resonse.Status = Models.Enum.EmailStatus.Sent;

            }

            dbemail = await repository.SaveOrUpdateAsync(dbemail);
            resonse.Id = dbemail.Id;
            resonse.Email = dbemail.ToEmailDTO();

            return resonse;
        }

        private SmtpClient SetConfiguration()
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = emailSettings.Host;
            smtp.Port = int.Parse(emailSettings.Port);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = true;

            return smtp;
        }

        private MailMessage SetMail(Email email)
        {
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(emailSettings.From);
            var to = email.To.Split(",");

            if (to?.Length > 0)
            {
                foreach (string _to in to)
                {
                    mail.To.Add(_to);
                }
            }
            else
            {
                return null;
            }
            mail.Subject = email.Subject;
            mail.IsBodyHtml = true;
            mail.Body = email.Content;
            return mail;
        }
    }
}
