using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using Nexmo.Api.Pricing;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;
using OverPass;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text.Json;
using System.Web.Helpers;
using Telegram.Bot.Types;
using TestDiplom.Areas.AdminPanel.Data;
using TestDiplom.Areas.AdminPanel.Models;
using TestDiplom.Areas.Identity.Data;
using TestDiplom.Models;
using static TestDiplom.Areas.Dispatcher.Controllers.DispatchController;

namespace TestDiplom.Areas.AdminPanel.Controllers
{

    public class CorzinaController : Controller
    {


        CartLine dbt = new CartLine();
        ShopCarti db = new ShopCarti(CreateNewContextOptions4());




        public IActionResult CorzinaBook()
        {

            return View(db.lines.ToList());
        }

        public List<books> GetCart(int id)
        {
            ReaderBook book = new ReaderBook(CreateNewContextOptions1());
            var t = book.books.Where(r => r.book_id == id);
            return t.ToList();
        }

        private static DbContextOptions<ReaderBook> CreateNewContextOptions1()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.



            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<ReaderBook>();
            builder.UseSqlServer("Data Source=LAPTOP-PM4SK01C;Initial Catalog=Test4;Integrated Security=True;Connect Timeout=30;Encrypt=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;TrustServerCertificate=True;Trusted_Connection=True")
                .EnableSensitiveDataLogging(true);

            return builder.Options;
        }

        private static DbContextOptions<TestAuth> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.



            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<TestAuth>();
            builder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TestDiplom;Integrated Security=True;Connect Timeout=30;Encrypt=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;TrustServerCertificate=True;Trusted_Connection=True")
                .EnableSensitiveDataLogging(true);

            return builder.Options;
        }
        public ActionResult AddToCart(long id)
        {



            ReaderBook book = new ReaderBook(CreateNewContextOptions1());
            TestAuth dbq = new TestAuth(CreateNewContextOptions());
            
           var lg = User.Identity.GetUserId();
            var t = book.publi.Include(c => c.bookes).Where(r => r.book_id == id).Select(x => new CartLine
            {
                
                title_bok = x.bookes.title,
                Year_creating = x.bookes.year_create,
                Year_public = x.pub_year,
                cost_bo = x.cost_book,
                Quantity = 1,
                guid = lg,
               

            }).AsNoTracking();
            
            int rt = db.lines.Count();
            Random fe = new Random();
            int me = fe.Next(rt + 1, rt + 1); 
            
            


            foreach (var y in t) {
                int? f = db.lines.Where(b => b.title_bok == y.title_bok && b.Year_public == y.Year_public && b.Year_creating == y.Year_creating).AsNoTracking().Count();
                
                if (f != 0) {
                    var fq = db.lines.Where(b => b.title_bok == y.title_bok).AsNoTracking().First();
                    y.id = fq.id;
                y.Quantity = fq.Quantity + 1;
                   db.lines.Update(y);
                    db.SaveChanges();
                }
                else 
                {
                    y.id = Convert.ToInt32(me);
                    db.lines.Add(y);
                db.SaveChanges();
                }
            }
            return RedirectToAction("CorzinaBook");
        }
        public ActionResult DeleteToCart(long id)
        {
            var t = db.lines.Where(u => u.id == id).ToList();
            foreach (var k in t) {
                db.lines.Remove(k);
                db.SaveChanges();
            }
            return RedirectToAction("CorzinaBook"); ;
        }

        
        public IActionResult RegisterOrder()
        {
            string lg = User.Identity.GetUserId();

            TestAuth dbq = new TestAuth(CreateNewContextOptions());

            var t = db.lines.Where(u => u.guid == lg).ToList();
            var lgw = dbq.Users.Find(lg);
            ViewBag.Zakaz = t;
            ViewBag.Phon = lgw.PhoneNumber;
            Order to = new Order
            {
                Line1 = null,
                City = null,
                Country = null,
                Phone = lgw.PhoneNumber,
                Name = null,
                tir = JsonSerializer.Serialize(t)
            };

            return View(to);
        }

        [HttpPost]

        public IActionResult RegisterOrder(Order to, List<IFormFile> Image)
        {

            string lg = User.Identity.GetUserId();
            
            TestAuth dbq = new TestAuth(CreateNewContextOptions());
           
            var t = db.lines.Where(u => u.guid == lg).ToList();
            var lgw = dbq.Users.Find(lg);
            ViewBag.Zakaz = t;
            /// int y = 0;
            var target = new MemoryStream();
            foreach (var f in Image) { 
            //    y++;
              //  switch (y){ 
                   //     case 0:
                        
                     //   break;
                   // case 1:
                      //  { }
                      //  break;
                    //default:
                      //  { }
                       // break;
                       // }
                if (f != null)
                {
                    if (f.Length > 0)
                    {
                    

                        
                            f.CopyTo(target);
                        
                    

                            
                        
                    }
                    else { }
                }
                else { }
            }
            string k = JsonSerializer.Serialize(target.ToArray());
                    if (lgw.PhoneNumber != null )
                                        { 
                                        to = new Order
                                            {
                                                Line1 = to.Line1,
                                                City = to.City,
                                                Country = to.Country,
                                                Phone = lgw.PhoneNumber,
                                            Name = to.Name,
                                            Image =  target.ToArray() ,
                                                tir = JsonSerializer.Serialize(t)
                                            };
                                        }
                                        else
                                        {
                                            to = new Order
                                            {
                                                Line1 = to.Line1,
                                                City = to.City,
                                                Country = to.Country,
                                                Phone = to.Phone,
                                                Name = to.Name,
                                                Image = target.ToArray(),
                                                tir = JsonSerializer.Serialize(t)
                                            };

                                        }
            if (to.Line1 != null && to.Name != null && to.City != null && to.Country != null && to.Phone != null && to.tir != null && to.Phone.Length > 10 && to.Phone.StartsWith("+79")|| to.Phone.StartsWith("8"))
            {
                
                db.Orders.Add(to);
                // db.RemoveRange(t);
                db.SaveChanges();
                return View("CreateOrder");
            }
            else
            {
                return View();
            }

           
        }
        
        public ViewResult CreateOrder()
        {
           return View();

        }

    }
}
