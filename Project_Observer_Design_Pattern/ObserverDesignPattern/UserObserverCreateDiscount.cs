using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Project_Observer_Design_Pattern.DAL;
using System;

namespace Project_Observer_Design_Pattern.ObserverDesignPattern
{
    public class UserObserverCreateDiscount : IUserObserver
    {
        private readonly IServiceProvider _serviceProvider;

        public UserObserverCreateDiscount(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public void CreateUser(AppUser appUser)
        {
            var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverCreateDiscount>>();//Burada ILogger içine her zaman üzerinde çalışılan sınıfı alır
            var scoped = _serviceProvider.CreateScope();//Burada context üzerinden değer ataması gerçekleştirilecek.
            //Burada scoped kaydedilecek alanı yaratır. Context'e kayıt işlemi yapılacak
            var context = scoped.ServiceProvider.GetRequiredService<Context>();//Kaydedilecek yeri alıyor. Context sınıfından alır.

            //kayıt işlemi olunca gerçekleşecek indirim tanımı
            context.Discounts.Add(new Discount
            {
                Rate = 25,//indirim oranı
                UserID = appUser.Id//Kaydolanın id değeri

            });
            context.SaveChanges();
            logger.LogInformation($"Yeni kayıt olan kullanımız : {appUser.Name + "" + appUser.Surname} için % 25 oanında bir indirim kodu tanımlamdo....");
        }
    }
}
