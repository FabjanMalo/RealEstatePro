using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using RealEstatePro.Application.Mail;
using RealEstatePro.Domain.Users;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IEmailSender = RealEstatePro.Application.Mail.IEmailSender;

namespace RealEstatePro.Infrastructure.Mail;
public class EmailSender(
    IOptions<EmailSettings> settingOptions,
    IOptions<Email> emailOptions)
    : IEmailSender
{
    private readonly EmailSettings _emailSettings = settingOptions.Value;
    private readonly Email _email = emailOptions.Value;

    public async Task<bool> SendEmail(User user)
    {
        var client = new SendGridClient(_emailSettings.ApiKey);
        var to = new EmailAddress(user.Email);
        var from = new EmailAddress
        {
            Email = _emailSettings.FromAddress,
            Name = _emailSettings.FromName,
        };

        var body = _email.Body
            .Replace("{firstName}", user.FirstName)
            .Replace("{lastName}", user.LastName);
        

        var message = MailHelper.CreateSingleEmail(from, to, _email.Subject, body, body);

        var result = await client.SendEmailAsync(message);


        return result.StatusCode == System.Net.HttpStatusCode.OK ||
            result.StatusCode == System.Net.HttpStatusCode.Accepted;
    }
}
