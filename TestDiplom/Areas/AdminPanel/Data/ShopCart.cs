using Microsoft.EntityFrameworkCore;
using System.Linq;
using TestDiplom.Areas.AdminPanel.Models;
using TestDiplom.Models;

namespace TestDiplom.Areas.AdminPanel.Data
{
    public class ShopCarti : DbContext

    {

        public ShopCarti(DbContextOptions<ShopCarti> options)
    : base(options)
        { }
        public DbSet<CartLine> lines { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Courier> cour { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.EnableSensitiveDataLogging().UseSqlServer("Data Source=LAPTOP-PM4SK01C;Initial Catalog=Test4;Integrated Security=True;Connect Timeout=30;Encrypt=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;TrustServerCertificate=True;Trusted_Connection=True").EnableSensitiveDataLogging();
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
              .HasOne(e => e.cour)
              .WithMany(e => e.Заказы)
              .HasForeignKey(e => e.t1)
              ;
        }
        /* public void AddItem(books game, int quantity, string gud)
        {

            CartLine line = lines
                .Where(g => g.bok.Contains(game));

            if (line == null)
            {
                lines.Add(new CartLine
                {
                   bok = new List<books>
                   { 
                   new books(game.cost_book = lines.bok,game.title,game.book_id)
                   },
                       
                      
                    Quantity = quantity,
                    guid = gud
                });
            /*}
            else
            {
                line.Quantity += quantity;
            }
        }*/

    }
}
