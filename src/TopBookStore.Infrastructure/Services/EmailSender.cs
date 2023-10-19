using Microsoft.AspNetCore.Identity.UI.Services;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;

namespace TopBookStore.Infrastructure.Services;

public class EmailSender : IEmailSender
{
    public async System.Threading.Tasks.Task SendEmailAsync(
        string email, string subject, string htmlMessage)
    {
        string? apiKey = Environment.GetEnvironmentVariable("TOPBOOKSTORE_API_KEY",
           EnvironmentVariableTarget.User);

        if (string.IsNullOrEmpty(apiKey))
        {
            Console.WriteLine("Not found API Key.");
        }

        Configuration.Default.ApiKey.Add("api-key", apiKey);

        TransactionalEmailsApi apiInstance = new();

        string senderName = "Dong";
        string senderEmail = "admin@gmail.com";
        SendSmtpEmailSender smtpEmailFrom = new(senderName, senderEmail);

        string ToEmail = email;
        string ToName = "End User";
        SendSmtpEmailTo smtpEmailTo = new(ToEmail, ToName);
        List<SendSmtpEmailTo> To = new()
        {
            smtpEmailTo
        }; // To.Add(smtpEmailTo)

        string textContent = "Hello there!";

        SendSmtpEmail sendSmtpEmail = new(smtpEmailFrom, To, bcc: null, cc: null,
            htmlContent: htmlMessage, textContent: textContent, subject: subject);

        CreateSmtpEmail result = await apiInstance.SendTransacEmailAsync(sendSmtpEmail);

        Console.WriteLine("Brevo response: " + result.ToJson());
    }
}