using Azure.Core;
using Blaze.Components.Account.Pages.Manage;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Models.ViewModels;
using Newtonsoft.Json.Linq;

namespace Blaze.Configurations.MailJet
{
    public class MailJetEmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MailJetEmailSender> _logger;
        private readonly string FROM_EMAIL = "lakeboy7824@protonmail.com";
        private readonly string ADMIN_EMAIL = "jerell@smithovision.com";
        private readonly string ADMIN_FROM_NAME = "Smithovision Emailer";
        private readonly string ADMIN_SUBJECT = "New Query";
        private readonly string NOREPLY_SUBJECT = "NO REPLY: Thank you for contacting Smithovision!";
        private readonly string NOREPLY_FROM_NAME = "NO REPLY: SMITHOVISION";
        private readonly string EMAIL_SUBJECT = "lakeboy7824@protonmail.com";


        public MailJetOptions _mailJetOptions;
        public MailJetEmailSender(IConfiguration configuration, ILogger<MailJetEmailSender> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await SendNoReply(email, subject);
            await NotifyAdmin(htmlMessage);
        }

        private async Task SendNoReply(string email, string htmlMessage)
        {
            _mailJetOptions = _configuration.GetSection("MailJet").Get<MailJetOptions>();

            MailjetClient client = new MailjetClient(_mailJetOptions.ApiKey, _mailJetOptions.SecretKey);
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
            .Property(Send.FromEmail, FROM_EMAIL)
            .Property(Send.FromName, NOREPLY_FROM_NAME)
            .Property(Send.To, email)
            .Property(Send.Subject, NOREPLY_SUBJECT)
            .Property(Send.HtmlPart, htmlMessage);

            await client.PostAsync(request);
        }

        private async Task NotifyAdmin(string html)
        {
            string htmlMessage = html;
            _mailJetOptions = _configuration.GetSection("MailJet").Get<MailJetOptions>();

            MailjetClient client = new MailjetClient(_mailJetOptions.ApiKey, _mailJetOptions.SecretKey);
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
            .Property(Send.FromEmail, FROM_EMAIL)
            .Property(Send.FromName, ADMIN_FROM_NAME)
            .Property(Send.To, ADMIN_EMAIL)
            .Property(Send.Subject, ADMIN_SUBJECT)
            .Property(Send.HtmlPart, htmlMessage);

            await client.PostAsync(request);
        }
    }
}