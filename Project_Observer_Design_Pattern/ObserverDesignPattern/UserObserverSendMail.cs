using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MimeKit;
using Project_Observer_Design_Pattern.DAL;
using System;
using MailKit.Net.Smtp;

namespace Project_Observer_Design_Pattern.ObserverDesignPattern
{
    public class UserObserverSendMail : IUserObserver
    {
        private readonly IServiceProvider _serviceProvider;

        public UserObserverSendMail(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void CreateUser(AppUser appUser)
        {
            var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverSendMail>>();
            //mail gönderme
            try
            {
                MimeMessage mimeMessage = new MimeMessage();
                MailboxAddress mailboxAddressFrom = new MailboxAddress("Admin Observer", "enurgucuk@gmail.com");
                mimeMessage.From.Add(mailboxAddressFrom);  //mailin kimden gönderileceği

                MailboxAddress mailboxAddress = new MailboxAddress("User", appUser.Email);
                mimeMessage.To.Add(mailboxAddress);

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = "Observer Design Pattern Dersimizde Bu Adıma Gelebildiğiniz İçin Size İndirim Kodu Tanımladık, Kodunuz GIFT0001";

                mimeMessage.Body = bodyBuilder.ToMessageBody();
                mimeMessage.Subject = "Hoş Geldin İndirim Hediyesi";

                SmtpClient smtpClient = new SmtpClient();  //system.smtp değil mailkit ile çalışıyoruz onu ekledik.

                smtpClient.Connect("smtp.gmail.com", 587, false);
                smtpClient.Authenticate("enurgucuk@gmail.com", "mtnraojtesslkbre");
                smtpClient.Send(mimeMessage);
                smtpClient.Disconnect(true);

                logger.LogInformation($"{appUser.Name + " " + appUser.Surname} isimli kullanıcının {appUser.Email} adlı mail adresine indirim kodu maili başarıyla gönderildi.");

            }
            catch (Exception ex)
            {

                throw ex;
            }
          
      

        }
    }
}
//mtnraojtesslkbre