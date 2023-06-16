using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;

using TestDiplom.Areas.Identity.Data;
using TestDiplom.Models;
using System.Xml;

using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;

using StatusCodeResult = Microsoft.AspNetCore.Mvc.StatusCodeResult;
using Microsoft.AspNetCore.Mvc.Filters;

using Microsoft.AspNetCore.Mvc.Rendering;
using WhatsAppApi;
using static System.Net.Mime.MediaTypeNames;
using Telegram.Bot;
using System.Threading;
using Telegram.Bot.Types;
using PagedList;
using Newtonsoft.Json;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using System;
using Message = TestDiplom.Models.Message;
using Microsoft.AspNetCore.Identity;

namespace TestDiplom.Controllers
{
    

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"Controller: {context.Controller.GetType().Name}");
            Console.WriteLine($"Action: {context.ActionDescriptor.DisplayName}");

            base.OnActionExecuting(context);
        }

        private readonly UserManager<IdentityUser> _userManager;

        

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, UserManager<IdentityUser> userManager, ReaderBook boo)
        {
            _logger = logger;
            Configuration = configuration;
            db = boo;
            _userManager = userManager;
        }
        public ActionResult Details(long? id)
        {
            
           var e =  _userManager.GetUsersInRoleAsync("myRole").Result;
            
            var che = db.author_in_books.Where(u => u.BookId == id).ToList();

            List<author> g = new List<author>();

            foreach(var t in che)
            {
                var n = db.authors.Where(u => u.creator_id == t.AuthorId).First();
                g.Add(n);

            }
            var cho = db.publi.Include(v => v.yo3).Include(l => l.yo2).Include(j => j.yo1).Where(u => u.book_id == id).ToList();
            var chi = db.books.Where(u => u.book_id == id).First();

            // Where(j => j.a.Contains(searching) || searching == null).ToList())
            ViewBag.Jim = chi;
            ViewBag.Jeo = cho;
            ViewBag.Jes = g;
            return View();
            
        }

        public ActionResult AddPublication(long? id)
        {
            var tea = db.books.Where(x => x.book_id == id).ToList();

            var teachers = db.di_currency.ToList();
            var teachers2 = db.di_lang.ToList();
            var teachers3 = db.di_country.ToList();

            ViewData["Teachers"] = new SelectList(teachers, "currency_id", "currency_name");
            ViewData["Teachers2"] = new SelectList(teachers2, "lang_id", "lang_name");
            ViewData["Teachers3"] = new SelectList(teachers3, "country_id", "country_name");
            ViewData["Tea"] = new SelectList(tea, "book_id", "title");
            
                return View(new publication());
            ;
            
        }

        [HttpPost]
        public ActionResult AddPublication([Bind("pub_num,pub_year,book_vol,circulation,book_id,cost_book,availability,currency_id,lang_id,country_id,book_id")] publication puba)
        {
            
                db.publi.Add(puba);
                db.SaveChanges();
           return RedirectToAction(nameof(ShowBooks));
            
        }


        public class PaginatedList<T> : List<T>
        {
            public int PageIndex { get; private set; }
            public int TotalPages { get; private set; }

            public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
            {
                PageIndex = pageIndex;
                TotalPages = (int)Math.Ceiling(count / (double)pageSize);

                this.AddRange(items);
            }

            public bool HasPreviousPage => PageIndex > 1;

            public bool HasNextPage => PageIndex < TotalPages;

            public static  PaginatedList<T> Create(List<T> source, int pageIndex, int pageSize)
            {
                var count = source.Count();
                var items =  source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return new PaginatedList<T>(items, count, pageIndex, pageSize);
            }
        }

        [Authorize(Roles = "admin,reader")]
        public ActionResult ShowBooks(string searching, long? searching1, string sortOrder, int? pageNumber)
        {
            int pageSize = 19;
            
            int par = (pageNumber ?? 1);
            ViewBag.Test = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            var result = db.books
     .Include(x => x.connect)
     .ThenInclude(x => x.c).ToList();

            switch (sortOrder)
            {
                case "Name_desc":
                    result = db.books
     .Include(x => x.connect)
     .ThenInclude(x => x.c).OrderByDescending(t => t.year_create).ToList();
                    break;
                default :
                    result = result.OrderBy(t => t.year_create).ToList();
                    break;
            }


            if (searching != null & searching1 == (long?)null) {
                return View(result.Where(j => j.title.Contains(searching)).ToPagedList(par, pageSize));
            }
            else if (searching == null & searching1 != (long?)null) {
                return View(result.Where(j => j.year_create == searching1).ToPagedList(par, pageSize));
            }
            else if (searching != null & searching1 != (long?)null){ 
                return View(result.Where(j => j.title.Contains(searching)).Where(j => j.year_create == searching1).ToPagedList(par, pageSize));
            }
            else { return View(PaginatedList<books>.Create(result, pageNumber ?? 1, pageSize)); }

        }
         public async Task<IActionResult> AddAuthor(long? id)
         {
            
            ViewBag.Chaos = id;
                    return View();
         }
                
        [HttpPost]     
        public async Task<IActionResult> AddAuthor([Bind("first_name_author,patronymic_author,surname_author,date_birth")] author transaction, long? id)
               
        {
           
            db.authors.Add(transaction);
            await db.SaveChangesAsync();
            var tn  = db.authors.OrderBy(t => t.creator_id).Last();
            author_in_books to = new author_in_books() { 
            AuthorId = tn.creator_id,
            BookId = Convert.ToInt64(id)
            };
            db.author_in_books.Add(to);
            await db.SaveChangesAsync();
            return View();
                
        }

        // GET: Transaction/AddOrEdit
        public IActionResult AddOrEdit(long? id)
        {
            
            if (id == null)
                return View(new author_in_books());
            else
                return View(db.author_in_books.Where(s => s.BookId == id).First());
        }

       

            // POST: Transaction/AddOrEdit
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("Id,BookId,AuthorId,c,s")] author_in_books transaction)
        {
         
            
           
            
                long? id = transaction.AuthorId;
                if (id != null && id != 0)
                {
                    db.authors.Update(transaction.c);
                    db.books.Update(transaction.s);
                    db.author_in_books.Update(transaction);
                    await db.SaveChangesAsync();
                return View(transaction);
            }
                else
                { 
                    db.authors.Add(transaction.c);
                    db.books.Add(transaction.s);
                await db.SaveChangesAsync();
             var e =   db.books.Count();
                var k = db.authors.Count();
                transaction.AuthorId = db.authors.Skip(e-1).Take(1).FirstOrDefault().creator_id;
                transaction.BookId = db.books.Skip(k - 1).Take(1).FirstOrDefault().book_id;
                    db.author_in_books.Add(transaction);
                    
                return RedirectToAction(nameof(ShowBooks));
            }
                
            
            
        }


        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
           var ij = db.books.Where(s => s.book_id == id);
            db.books.RemoveRange(ij);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(ShowBooks));
        }

        public IActionResult StartPage()
        {
            return View();
        }

        
        public IConfiguration Configuration { get; set; }
        public struct BotUpdate
        {
            public string text;
            public long id;
            public string? username;
            public string? firstname;
            public string? lastname;
            
        }
        public async Task<IActionResult> ChatList()
        { 
            var dbc = new TeleMessages(CreateNewContextOptions2());
            int? r = dbc.Messages.Count();
            ViewData["Count"] = r;
                        var c = dbc.Messages.ToList();
           
            return View(c);
        }

        

        [HttpPost]
        public async Task<IActionResult> ChatList(string destID, string text,string action)
        {
            if (action == "Отправить сообщение")
                {

            
                try
                {
                    
            var bot = new Telegram.Bot.TelegramBotClient("6002571377:AAEJdjdOsCSqTRXjHhmwc4-8VOGjBMs3Lw4");
          

                 var dba = await bot.SendTextMessageAsync(
                        chatId: 871681726,
                            text: "Диспетчер : " + text);
                  var  de = new TeleMessages(CreateNewContextOptions2());

                   Message dh = new Message() { 
                   Text = dba.Text.Remove(0, 12),
                   Username = "Dispatcher",
                   TimeSend = DateTime.Now,
                   };
                    de.Messages.Add(dh);
                    de.SaveChanges();


                }
                catch (Exception e)
                {
                    Console.WriteLine("err");
                }
            }
            var dbc = new TeleMessages(CreateNewContextOptions2());
            int? r = dbc.Messages.Count();
            ViewData["Count"] = r;
            var c = dbc.Messages.ToList();

            return RedirectToAction("StartPage");
            
        }

         public static Task ErrorHandler(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }

        public static async Task UpdateHandler(ITelegramBotClient bot, Update update, CancellationToken arg3)
        {
            using (var db = new TeleMessages(CreateNewContextOptions2()))
            {
                if (update.Type == UpdateType.Message)
                {
                    if (update.Message.Type == MessageType.Text)
                    {
                        //write an update
                        var _botUpdate = new Message
                        {
                            Text = update.Message.Text,
                            Username = update.Message.From.FirstName,
                           TimeSend = DateTime.Now
                            
                        };
                        await db.AddAsync(_botUpdate);
                        await db.SaveChangesAsync();
                    }
                }
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
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

        private static DbContextOptions<TeleMessages> CreateNewContextOptions2()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.



            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<TeleMessages>();
            builder.UseSqlServer("Data Source=LAPTOP-PM4SK01C;Initial Catalog=Test4;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                .EnableSensitiveDataLogging(true);

            return builder.Options;
        }

        private static DbContextOptions<TestAuth> CreateNewContextOptions3()
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

        private ReaderBook db = new ReaderBook(CreateNewContextOptions());



        
    }
}