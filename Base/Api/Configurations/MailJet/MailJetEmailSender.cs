using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Api.Configurations.MailJet
{
    public class MailJetEmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MailJetEmailSender> _logger;

        public MailJetOptions _mailJetOptions;
        public MailJetEmailSender(IConfiguration configuration, ILogger<MailJetEmailSender> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            _mailJetOptions = _configuration.GetSection("MailJet").Get<MailJetOptions>();

            MailjetClient client = new MailjetClient(_mailJetOptions.ApiKey, _mailJetOptions.SecretKey);
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
            .Property(Send.FromEmail, "lakeboy7824@protonmail.com")
            .Property(Send.FromName, "MailJet Sender")
            .Property(Send.To, email)
            .Property(Send.Subject, subject)
            .Property(Send.HtmlPart, htmlMessage);

            await client.PostAsync(request);
            //if (response.IsSuccessStatusCode)
            //{
            //    _logger.LogInformation(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
            //    //_logger.LogInformation(response.GetData());
            //}
            //else
            //{
            //    _logger.LogError(string.Format("StatusCode: {0}\n", response.StatusCode));
            //    _logger.LogError(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
            //    _logger.LogError(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
            //    _logger.LogError(response.ToString());
            //}
        }
    }
}