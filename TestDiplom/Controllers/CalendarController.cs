using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestDiplom.Models;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Web.Helpers;
using Newtonsoft.Json;

namespace TestDiplom.Controllers
{

    public class CalendarController : Microsoft.AspNetCore.Mvc.Controller
    {
        private static DbContextOptions<ReaderBook> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.



            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<ReaderBook>();
            builder.UseSqlServer("Data Source=LAPTOP-PM4SK01C;Initial Catalog=Test4;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                .EnableSensitiveDataLogging(true);

            return builder.Options;
        }
        // GET: Calendar
        public ActionResult EventManage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetEvents()
        {
            using (ReaderBook dc = new ReaderBook(CreateNewContextOptions())) {
            
                var events = new List<events>();
                
                     events = dc.eventi.ToList();


                return new JsonResult(events);
             } ;
        }

        [HttpPost]
        public ActionResult SaveEvent(string json)
        {

            var status = false;
            using (ReaderBook dc = new ReaderBook(CreateNewContextOptions()))
            {


                var c = JsonConvert.DeserializeObject<events>(json);
                var va = dc.eventi.Where(a => a.EventID == c.EventID).Count(); 
                if (va > 0)
                {
                    //Update the event
                    var v = dc.eventi.Where(a => a.EventID == c.EventID).FirstOrDefault();
                    if (v != null)
                    {
                        v.Subject = c.Subject;
                        v.Start = c.Start;
                        v.End = c.End;
                        v.Description = c.Description;
                        v.IsFullDay = c.IsFullDay;
                        v.ThemeColor = c.ThemeColor;
                        dc.Update(v);
                        dc.SaveChanges();
                    }
                     
                }
                else if(c != null)
                {
                    dc.eventi.Add(c);
                    dc.SaveChanges();
                    status = true;
                }
                else { status = false; }


               
                

            }
            return new JsonResult (new { status = status });
        }


        public ActionResult _Run()
        {
            return PartialView("~/Views/Shared/_TestPartial.cshtml");
        }

        [HttpPost]
        public IActionResult DeleteEvent(int eventID)
        {
            var status = false;
            using (ReaderBook dc = new ReaderBook(CreateNewContextOptions()))
            {
                var v = dc.eventi.Where(a => a.EventID == eventID).FirstOrDefault();
                if (v != null)
                {
                    dc.eventi.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult (new { status = status });
        }
    }
}
