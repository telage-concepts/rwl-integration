using Core.EdTech.Domain.ViewModels.Mail;

namespace Core.EdTech.Infrastructure.Services.Abstracts;

public interface IMailService
{
  bool SendMail(MailDataVM mailData);
  Task<bool> SendMailAsync(MailDataVM mailData);
}
