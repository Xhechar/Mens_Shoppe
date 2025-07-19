
using MailKit.Net.Smtp;
using MimeKit;

public class MailService
{
    private readonly IConfiguration configuration;

    public MailService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public async Task SendEmail(string to, string subject, string body)
    {
      
      var email = new MimeMessage();
      
      email.From.Add(MailboxAddress.Parse(configuration["MailSettings:From"]));
      email.To.Add(MailboxAddress.Parse(to));
      email.Subject = subject;

      var builder = new BodyBuilder
      {
          HtmlBody = body
      };

      email.Body = builder.ToMessageBody();

      using var smtp = new SmtpClient();

      await smtp.ConnectAsync(configuration["MailSettings:Host"], int.Parse(configuration["MailSettings:Port"]!), MailKit.Security.SecureSocketOptions.StartTls);
      await smtp.AuthenticateAsync(configuration["MailSettings:Username"], configuration["MailSettings:Password"]);
      await smtp.SendAsync(email);
      await smtp.DisconnectAsync(true);
    }
}