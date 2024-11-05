using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using System.Net;
using MySocialApp.Domain;

namespace MySocialApp.Api.AuthOverrides;

public class EmailSender : IEmailSender<User>
{
    public Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
    {
        throw new NotImplementedException();
    }

    public Task SendEmailAsync(User user, string subject, string htmlMessage)
    {
        // Example of sending an email (make sure to configure SMTP settings)
        var smtpClient = new SmtpClient("smtp.example.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("your-email@example.com", "your-email-password"),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress("your-email@example.com"),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true,
        };
        mailMessage.To.Add(user.Email);

        return smtpClient.SendMailAsync(mailMessage);
    }

    public Task SendPasswordResetCodeAsync(User user, string email, string resetCode)
    {
        throw new NotImplementedException();
    }

    public Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
    {
        throw new NotImplementedException();
    }
}
