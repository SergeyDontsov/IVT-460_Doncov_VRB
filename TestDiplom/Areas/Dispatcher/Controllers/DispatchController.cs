using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TestDiplom.Areas.AdminPanel.Data;
using TestDiplom.Areas.AdminPanel.Models;

using TestDiplom.Controllers;
using TestDiplom.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestDiplom.Areas.Dispatcher.Controllers
{
    public class DispatchController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly ILogger<DispatchController> _logger;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"Controller: {context.Controller.GetType().Name}");
            Console.WriteLine($"Action: {context.ActionDescriptor.DisplayName}");

            base.OnActionExecuting(context);
        }


        public DispatchController(ILogger<DispatchController> logger, UserManager<IdentityUser> userManager )
        {
            _logger = logger;
            
            _userManager = userManager;
        }
        public IActionResult ShowOrder()
        {
            ShopCarti db = new ShopCarti(CreateNewContextOptions4());
            
            JsonSerializerOptions t = null;
           var data = db.Orders.Include(f => f.cour).Select(tera => new
            {
              id = tera.order_id,
               Zakaz = JsonSerializer.Deserialize<List<CartLine>>(tera.tir,t),
               Адрес = tera.Line1,
               Получатель = tera.Name,
               Image = tera.Image,
               Телефон_Получателя = tera.Phone,
               Телефон_Курьера = tera.cour.phone,
               Имя_Курьера = tera.cour.Nickname,

           });

            var chi = db.Orders.Where(c => c.t1 != null).ToList();
            var chie = db.Orders.Where(c => c.t1 != null).Count();
            // Where(j => j.a.Contains(searching) || searching == null).ToList())

            ViewBag.Jimq = chie;

            ViewBag.Jim = chi;

            ViewBag.Dis = data;
            return View();
        }

        public IActionResult DeleteCourier(long id)
        {
            ShopCarti db1 = new ShopCarti(CreateNewContextOptions5());
            var da = db1.Orders.Where(c=> c.order_id == id).FirstOrDefault();
            var h = db1.cour.Where(p => p.Id == da.t1).FirstOrDefault();
            h.KolvoZakaz -= 1;
            da.t1 = null;
            db1.Update(da);
            db1.SaveChanges();
            db1.Update(h);
            db1.SaveChanges();
            return RedirectToAction(nameof(ShowOrder));
        }

            public IActionResult AddCourier(long id)
        {

            
            var e = _userManager.GetUsersInRoleAsync("courier").Result.Select(d => new { 
            id = d.Id,
                Имя = d.UserName, 
            Телефон = d.PhoneNumber,
            
            });
            ShopCarti db1 = new ShopCarti(CreateNewContextOptions5());
            var hj = db1.Orders.Where(h => h.order_id == id).FirstOrDefault();
            

            foreach (var item in e)
            { 
                var qhj = db1.Orders.Where(h => h.t1 == item.id).AsNoTracking().Count();
                Courier u = new Courier()
                {
                    Id = item.id,
                    Nickname = item.Имя,
                    phone = item.Телефон,
                    KolvoZakaz = qhj,
                    Заказы = new List<Order>()

                };
                var jd = db1.cour.Where(k => k.Id == u.Id).AsNoTracking().FirstOrDefault();
                if (jd == null)
                {
                    db1.cour.Add(u);
                    db1.SaveChanges();

                }
                else 
                { 
                    db1.cour.Update(u);
                    db1.SaveChanges(); 
                }
            
            var ju = db1.cour.Where(k => k.KolvoZakaz < 3).AsNoTracking().FirstOrDefault();
            hj.t1 = ju.Id;
           u.KolvoZakaz = qhj+1;
            db1.Update(hj);
                
                u.Заказы = new List<Order>() { hj };
                db1.Update(u);
            db1.SaveChangesAsync();
            }
            

            return RedirectToAction(nameof(ShowOrder));
        }
        public static DbContextOptions<ShopCarti> CreateNewContextOptions4()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.



            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<ShopCarti>();
            builder.UseSqlServer("Data Source=LAPTOP-PM4SK01C;Initial Catalog=Test4;Integrated Security=True;Connect Timeout=30;Encrypt=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;TrustServerCertificate=True;Trusted_Connection=True")
                .EnableSensitiveDataLogging(true);

            return builder.Options;
        }
        public static DbContextOptions<ShopCarti> CreateNewContextOptions5()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.



            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<ShopCarti>();
            builder.UseSqlServer("Data Source=LAPTOP-PM4SK01C;Initial Catalog=Test4;Integrated Security=True;Connect Timeout=30;Encrypt=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;TrustServerCertificate=True;Trusted_Connection=True")
                .EnableSensitiveDataLogging(true);

            return builder.Options;
        }
    }
}
