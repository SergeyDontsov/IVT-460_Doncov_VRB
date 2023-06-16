
using System.ComponentModel.DataAnnotations;
using TestDiplom.Areas.AdminPanel.Data;
using TestDiplom.Models;
using Microsoft.AspNet.Identity;
using TestDiplom.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;
using static TestDiplom.Areas.Dispatcher.Controllers.DispatchController;
using JetBrains.Annotations;
using System.Diagnostics.CodeAnalysis;

namespace TestDiplom.Areas.AdminPanel.Models
{
    
    public class CartLine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        public string title_bok { get; set; }

        public long Year_creating { get; set; }

        public int Year_public { get; set; }
        public long cost_bo { get; set; }
        public int Quantity { get; set; }


       
        public string guid { get; set; }

        



    }
   

public class Order

    {
        [Key]
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long order_id { get; set; }

        [Required(ErrorMessage = "Укажите Ваше имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Вставьте адрес доставки")]
        [Display(Name = "Адрес")]
        public string Line1 { get; set; }



        [Required(ErrorMessage = "Укажите город")]
        [Display(Name = "Город")]
        public string City { get; set; }

        [Required(ErrorMessage = "Укажите страну")]
        [Display(Name = "Страна")]
        public string Country { get; set; }


        [RegularExpression(@"^((\+7|7|8)+([0-9]){10})$", ErrorMessage = "Неверный формат. Примеры: +7хххххххххх;8хххххххххх;7хххххххххх")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Ваша корзина пуста")]
        public string tir { get; set; }

        [MaybeNull]
        public string t1 { get; set; }

        
        
        public byte[] Image  { get; set; } 
        public Courier cour { get; set; }



    }

    public class Courier
    {
        [Key]

        public string Id { get; set; }

        public string Nickname { get; set; }

        
        public string phone { get; set; }

        public int KolvoZakaz { get; set; }

        public List<Order> Заказы { get; set; } = new List<Order>();
    }
    public class Cart
    {
        ShopCarti db = new ShopCarti(CreateNewContextOptions4());
        
        public void RemoveLine(books game)
        {
            var t = db.lines.Find(game.title);
            db.lines.Remove(t);

        }

        /*public decimal ComputeTotalValue()
        {
            return db.lines.Sum(e => e.bok.cost_book * e.Quantity/10);

        }*/
        public void Clear()
        {
            db.lines.RemoveRange(db.lines);
        }
        public IEnumerable<CartLine> Lines
        {
            get { return db.lines; }
        }
    }
}

