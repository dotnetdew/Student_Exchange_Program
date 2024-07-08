using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace StudentExchange.Wiut.Web.Services.EmailService;

public class MailRuEmailSender : IEmailSender
{
    private readonly string email;
    private readonly string password;

    public MailRuEmailSender(string email, string password)
    {
        this.email = email;
        this.password = password;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string messageBody)
    {
        MailMessage message = new MailMessage();
        message.From = new MailAddress(email, "Abdullokh Tolibjonov");
        message.To.Add(toEmail);
        message.Subject = subject;
        message.Body = messageBody;

        SmtpClient smtpClient = new SmtpClient();
        smtpClient.Host = "smtp.mail.ru";
        smtpClient.Port = 587;
        smtpClient.EnableSsl = true;
        smtpClient.Credentials = new NetworkCredential(email, password);
        smtpClient.Timeout = 30000;
        //password  mB4jaDR0pfmZvdfiPAV5 smtp.mail.ru

        await smtpClient.SendMailAsync(message);
    }
}
