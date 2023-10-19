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
        // this is only for testing
        string? apiKey = "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";

        // // you should use this instead
        // string? apiKey = Environment.GetEnvironmentVariable("TOPBOOKSTORE_API_KEY",
        //    EnvironmentVariableTarget.Machine);

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