using Microsoft.EntityFrameworkCore;

namespace TestDiplom.Models
{
    public class TeleMessages:DbContext
    {

        public TeleMessages(DbContextOptions<TeleMessages> options)
     : base(options)
        { }
        public DbSet<Message> Messages { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-PM4SK01C;Initial Catalog=Test4;Integrated Security=True;Connect Timeout=30;Encrypt=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;TrustServerCertificate=True;Trusted_Connection=True");
            }

            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { }

       
    }
    public class Message
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }

        public DateTime TimeSend { get; set; }
        
    }

}
