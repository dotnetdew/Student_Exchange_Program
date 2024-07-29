using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;

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
        try
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(email, "Westminster International University in Tashkent");
            message.To.Add(toEmail);
            message.Subject = subject;
            message.Body = messageBody;
            message.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "klms.wiut.uz";
            smtpClient.Port = 27;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(email, password);
            smtpClient.Timeout = 30000;

            await smtpClient.SendMailAsync(message);
        }
        catch (SmtpException ex)
        {
            Console.WriteLine($"SMTP Exception: {ex.Message}");
            Console.WriteLine($"StatusCode: {ex.StatusCode}");
        }
        catch (SocketException ex)
        {
            Console.WriteLine($"Socket Exception: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General Exception: {ex.Message}");
        }
    }

}
