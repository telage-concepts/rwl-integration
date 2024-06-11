
using Core.FinTech.Domain.ViewModels.Mail;

namespace Core.FinTech.Infrastructure.Services.Abstracts;

public interface IMailService
{
  bool SendMail(MailDataVM mailData);
  Task<bool> SendMailAsync(MailDataVM mailData);
}
