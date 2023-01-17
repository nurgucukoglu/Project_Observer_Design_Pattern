using Project_Observer_Design_Pattern.DAL;
using System.Collections.Generic;

namespace Project_Observer_Design_Pattern.ObserverDesignPattern
{
    public class UserObserverSubject
    {
        //Burada observer'ları silip kaydeddebileceğim.
        //Veya mesela observer bütün adımları yapsın sadece mail atmasın gibi
        //observer yönetimlerini burada yapacağım
        private readonly List<IUserObserver> _userObservers; //Burada IUserObserver'ı liste olarak tanımlayarak, kullanıcı oluşturma yazdırma silme gibi
        //kullanıcının yaptığı işlemleri listede tutacak

        public UserObserverSubject()
        {
            _userObservers = new List<IUserObserver>();
        }


        //metotları tanımlayalım
        public void RegisterObserver(IUserObserver userObserver)
        {
            _userObservers.Add(userObserver);//observer'ı ekle
            //çalıştırılacak metotlar yani observer edilecek IUserObserver'da bulunan metotları alır. bu observers listesine ekler.

        }
        //observerda kaldırma yapsın
        public void RemoveObserver(IUserObserver userObserver)
        {
            _userObservers.Remove(userObserver);
        }

        //notify
        public void NotifyObserver(AppUser appUser)
        {
            //bütün adımlar için her bir observer sınıfında içindeki işlemi uygular.
            //kullanıcı kaydolduğunda
            _userObservers.ForEach(x =>
            {
                x.CreateUser(appUser);//kullanıcı kaydolduğunda her bir observer sınıfındaki işlem için uygula. notify'ı çalıştır
            });

        }

    }
}

//sıralamayı startuptan dönücez çünkü constructor döndük burda hepsini.
