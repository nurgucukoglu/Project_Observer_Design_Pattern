using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_Observer_Design_Pattern.DAL;
using Project_Observer_Design_Pattern.Models;
using Project_Observer_Design_Pattern.ObserverDesignPattern;
using System.Threading.Tasks;

namespace Project_Observer_Design_Pattern.Controllers
{
    public class DefaultController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly UserObserverSubject _userObserver;

        public DefaultController(UserManager<AppUser> userManager, UserObserverSubject userObserver)
        {
            _userManager = userManager;
            _userObserver = userObserver;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserRegisterViewModel model)
        {
            var appUser = new AppUser()
            {
                UserName = model.Username,
                Email = model.Mail,
                Name = model.Name,
                Surname = model.Surname
            };
            var result = await _userManager.CreateAsync(appUser, model.Password);
            if (result.Succeeded)
            {
                _userObserver.NotifyObserver(appUser);//konsola log atması için notify metodunu
                //çağırıyoruz
                //kullanıcı başarıyla kayıt olursa loglama ile için notifyobserver'ı çalıştırır.
                //Böylecei çindeki konsola yazdırma mail gönderme işlemlerini yapacak

                ViewBag.message = "Üyelik Sistemi Başarılı Bir Şekilde Oluşturuldu.";
            }
            else
            {
                ViewBag.message = "Üyelik Kaydında Bir Hata Oluştu";
            }
            return View();
        }
    }
}
