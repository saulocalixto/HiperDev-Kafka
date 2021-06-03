using CalixtosStore.Email.Interfaces;
using CalixtosStore.Email.Settings;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CalixtosStore.Email
{
    public class AuthMessageSender : IEmailSender
    {
        public AuthMessageSender()
        {
            _emailSettings = new EmailSettings
            {
                PrimaryDomain = "smtp.gmail.com",
                PrimaryPort = 587,
                UsernameEmail = "hiperdev2021@gmail.com",
                UsernamePassword = "bláblá",
            };
        }

        public EmailSettings _emailSettings { get; }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            Execute(email, subject, message).Wait();
            return Task.FromResult(0);
        }

        public async Task Execute(string email, string subject, string message)
        {
            try
            {
                string toEmail = string.IsNullOrEmpty(email) ? _emailSettings.ToEmail : email;

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UsernameEmail, "HiperDev")
                };

                mail.To.Add(new MailAddress(toEmail));

                mail.Subject = "HiperDev - " + subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}