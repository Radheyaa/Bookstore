using Bookstore.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Services
{
    public class EmailService : IEmailService
    {
        public const string templatepath = @"EmailTemplate/{0}.html";
        private readonly SMTPConfigModel _smtpconfig;


        public async Task SendTestEmail(UserEmailOptions userEmailOptions)
        {

            userEmailOptions.Subject = updatePlaceHolder("Hello {{username}} , This is test email from book store App",userEmailOptions.PlaceHolder);
            userEmailOptions.Body = updatePlaceHolder(GetmailBody("TestEmail"), userEmailOptions.PlaceHolder) ;

            await SendEmail(userEmailOptions);
        }

        public async Task SendEmailforEmailConfirmation(UserEmailOptions userEmailOptions)
        {

            userEmailOptions.Subject = updatePlaceHolder("Hello {{username}} , Confirm your email id", userEmailOptions.PlaceHolder);
            userEmailOptions.Body = updatePlaceHolder(GetmailBody("EmailConfirm"), userEmailOptions.PlaceHolder);

            await SendEmail(userEmailOptions);
        }


        public EmailService(IOptions<SMTPConfigModel> smtpconfig)
        {
            _smtpconfig = smtpconfig.Value;
        }

        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            MailMessage mail = new MailMessage()
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_smtpconfig.SenderAddress, _smtpconfig.SenderDisplayName),
                IsBodyHtml = _smtpconfig.IsBodyHTML

            };

            foreach (var toEmail in userEmailOptions.ToEmail)
            {
                mail.To.Add(toEmail);
            }

            mail.BodyEncoding = Encoding.Default;

            NetworkCredential networkCredential = new NetworkCredential(_smtpconfig.UserName, _smtpconfig.Password);

            SmtpClient smtpClient = new SmtpClient()
            {
                Host = _smtpconfig.host,
                Port = _smtpconfig.port,
                EnableSsl = _smtpconfig.EnableSSL,
                UseDefaultCredentials = _smtpconfig.UseDefaultCredentials,
                Credentials = networkCredential
            };

            await smtpClient.SendMailAsync(mail);
        }

        public string GetmailBody(string templateName)
        {
            var body = File.ReadAllText(string.Format(templatepath, templateName));
            return body;
        }


        public string updatePlaceHolder(string text, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if(!string.IsNullOrEmpty(text) && keyValuePairs != null) 
            {
                foreach (var placeholder in keyValuePairs)
                {
                    if (text.Contains(placeholder.Key)) 
                    {
                        text = text.Replace(placeholder.Key, placeholder.Value);
                    }
                }
            }

            return text;
        }

    }
}
