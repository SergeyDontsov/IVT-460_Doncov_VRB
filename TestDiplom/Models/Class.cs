using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TestDiplom.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using TestDiplom.Areas.AdminPanel.Models;
using Microsoft.Extensions.Hosting;
using System.Data.Entity.Infrastructure;

namespace TestDiplom.Models
{
    public partial class ReaderBook : DbContext

    {
        public ReaderBook(DbContextOptions<ReaderBook> options)
    : base(options)
        { }

        public DbSet<author> authors { get; set; }

        public DbSet<author_in_books> author_in_books { get; set; }
        public DbSet<books> books { get; set; }

        public DbSet<book_storage> book_stor { get; set; }

        public DbSet<department> depart { get; set; }

        public DbSet<dict_cities> di_city { get; set; }

        public DbSet<dict_countries> di_country { get; set; }

        public DbSet<dict_currency> di_currency { get; set; }

        public DbSet<dict_lang> di_lang { get; set; }

        public DbSet<dict_regions> di_reg { get; set; }

        public DbSet<dict_storage_rooms> di_stor_room { get; set; }

        public DbSet<dict_thematics> di_them { get; set; }

        public DbSet<events> eventi { get; set; }

        public DbSet<issuance> issuances { get; set; }

        public DbSet<lines> list { get; set; }

        public DbSet<publication> publi { get; set; }

        public DbSet<publi_house> publi_h { get; set; }

        public DbSet<publish_house_places> publi_hou_pla { get; set; }

        public DbSet<reader_ticket> read_ticket { get; set; }

        public DbSet<staffer> staffers { get; set; }

        public DbSet<staff_eve> sta_eve { get; set; }

        public DbSet<work_days> work_day { get; set; }

        public DbSet<work_staff> work_staffs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-PM4SK01C;Initial Catalog=Test4;Integrated Security=True;Connect Timeout=30;Encrypt=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;TrustServerCertificate=True;Trusted_Connection=True").EnableSensitiveDataLogging();
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<publication>()
       .HasOne(e => e.yo1)
       .WithMany(e => e.test2)
       .HasForeignKey(e => e.currency_id)
       .IsRequired();
            modelBuilder.Entity<publication>()
       .HasOne(e => e.yo2)
       .WithMany(e => e.test3)
       .HasForeignKey(e => e.lang_id)
       .IsRequired();
            modelBuilder.Entity<publication>()
       .HasOne(e => e.yo3)
       .WithMany(e => e.test4)
       .HasForeignKey(e => e.country_id)
       .IsRequired();
            modelBuilder.Entity<publication>()
      .HasOne(e => e.bookes)
      .WithMany(e => e.connect3)
      .HasForeignKey(e => e.book_id)
      .IsRequired();
            modelBuilder.Entity<author_in_books>()
                .HasOne(b => b.s)
                .WithMany(ba => ba.connect)
                .HasForeignKey(bi => bi.BookId);

            modelBuilder.Entity<author_in_books>()
              .HasOne(b => b.c)
              .WithMany(ba => ba.connect2)
              .HasForeignKey(bi => bi.AuthorId);

            modelBuilder.Entity<author_in_books>()
              .HasIndex(bi => new { bi.BookId, bi.AuthorId }).IsUnique();

            modelBuilder.Entity<book_storage>()
                .HasOne(b => b.c)
                .WithMany(ba => ba.test5)
                .HasForeignKey(bi => bi.storage_rooms_id);

            modelBuilder.Entity<book_storage>()
                .HasOne(b => b.s)
                .WithMany(ba => ba.test1)
                .HasForeignKey(bi => bi.book_storage_code);
        }

    }


    public class books
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long book_id { get; set; }

        [Required]
        [MaxLength(150)]
        public string title { get; set; }

        public long year_create { get; set; }

        

        public List<publication> connect3 { get; set; } = new List<publication>();
        public List<author_in_books> connect { get; set; } = new List<author_in_books>();

    }

    
    public class BookVM
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long book_id { get; set; }

        [Required]
        public string title { get; set; }

        public long year_create { get; set; }

        public long cost_book { get; set; }

        public short? book_storage_id { get; set; }

        public short? currency_id { get; set; }

        public short? lang_id { get; set; }

        public short? country_id { get; set; }


        public List<long> AuthorIds { get; set; }

    }

    public class BookWithAuthorsVM
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long book_id { get; set; }

        [Required]
        public string title { get; set; }

        public long year_create { get; set; }

        public long cost_book { get; set; }

        public short? book_storage_id { get; set; }

        public short? currency_id { get; set; }

        public short? lang_id { get; set; }

        public short? country_id { get; set; }


        public List<string> AuthorNames { get; set; }

    }

    public partial class author
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long creator_id { get; set; }

    [Required]
    [StringLength(50)]
    public string surname_author { get; set; }

    [Required]
    [StringLength(50)]
    public string first_name_author { get; set; }

    [Required]
    [StringLength(50)]
    public string patronymic_author { get; set; }

    public DateTime date_birth { get; set; }

    public short? country_id { get; set; }

        
        public List<author_in_books> connect2 { get; set; } = new List<author_in_books>();
}
public partial class author_in_books
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
       public long BookId { get; set; }
        public long AuthorId { get; set; }
        public books s { get; set; }

        
        public author c { get; set; }

    }

    public partial class book_storage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long book_storage_id { get; set; }

        [Required]
        public long book_storage_code { get; set; }

        public long storage_rooms_id { get; set; }

        public publication s { get; set; }


        public dict_storage_rooms c { get; set; }


    }

    public partial class department
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short department_id { get; set; }

        [Required]
        [StringLength(50)]
        public string appellation { get; set; }

        [Required]
        [StringLength(50)]
        public string duties_description { get; set; }

    }

    public partial class dict_countries
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short country_id { get; set; }

        [StringLength(50)]
        public string country_name { get; set; }

        public List<publication> test4 { get; set; } = new List<publication>();


    }

    public partial class dict_lang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short lang_id { get; set; }

        [Required]
        public string lang_name { get; set; }

        public List<publication> test3 { get; set; } = new List<publication>();
    }

    public partial class dict_currency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short currency_id { get; set; }

        [Required]
        [MaxLength(50)]
        public string currency_name { get; set; }

        public List<publication> test2 { get; set; } = new List<publication>();
    }

    public partial class staffer
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short staff_id { get; set; }

        [Required]
        [MaxLength(50)]
        public string surname_staffer { get; set; }

        [Required]
        [MaxLength(50)]
        public string first_name_staffer { get; set; }

        [Required]
        [MaxLength(50)]
        public string patronymic_staffer { get; set; }

        [Required]
        public string work_telephone { get; set; }

        [Required]
        [MaxLength(50)]
        public string position { get; set; }

        public short? department_id { get; set; }



    }

    public partial class dict_thematics
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short theme_id { get; set; }

        [Required]
        [MaxLength(50)]
        public string theme_name { get; set; }

        [Required]
        [MaxLength(50)]
        public string theme_description { get; set; }


    }

    public partial class work_days
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short work_day_id { get; set; }

        public DateTime be_time { get; set; }

        public DateTime ov_time { get; set; }

        [Required]
        [MaxLength(50)]
        public string days { get; set; }
        [MaxLength(50)]
        public string note_time { get; set; }

    }

    public partial class publication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long publication_id { get; set; }

        public short pub_num { get; set; }

        public short pub_year { get; set; }

        public short book_vol { get; set; }

        public long сirculation { get; set; }

        public long cost_book { get; set; }

        public bool availability { get; set; }
        public short? book_storage_id { get; set; }
        public List<book_storage> test1 { get; set; } = new List<book_storage>();
        public short? currency_id { get; set; }
        public dict_currency yo1 { get; set; }

        public short? lang_id { get; set; }

        public dict_lang yo2 { get; set; }


        public short? country_id { get; set; }

        public dict_countries yo3 { get; set; }
        public books bookes { get; set; }
        public long? book_id { get; set; }

        public short? publisher_id { get; set; }


    }

    public partial class lines
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [StringLength(450)]
        public string guid { get; set; }
        [MaxLength(50)]
        public string title_bok { get; set; }

        public long? cost_bo { get; set; }

        public int? Quantity { get; set; }
    }

    public partial class events
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventID { get; set; }
        [MaxLength(50)]
        public string Subject { get; set; }
        [MaxLength(50)]
        public string Description { get; set; }
            public System.DateTime Start { get; set; }
            public Nullable<System.DateTime> End { get; set; }
        [MaxLength(50)]
        public string ThemeColor { get; set; }
            public bool IsFullDay { get; set; }
        




    }



    public partial class dict_regions
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short region_id { get; set; }

        [Required]
        [MaxLength(50)]
        public string region_name { get; set; }


    }

    public partial class dict_storage_rooms
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long stor_rooms_id { get; set; }


        public long storage_rooms_id { get; set; }
        public short room_code { get; set; }

        public List<book_storage> test5 { get; set; } = new List<book_storage>();

    }

    public partial class publish_house_places
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short publish_house_places_id { get; set; }

        [Required]
        [MaxLength(150)]
        public string addr_publihouse { get; set; }

        public short? publisher_id { get; set; }

        public short? city_id { get; set; }

        public short? region_id { get; set; }

        public short? country_id { get; set; }


    }

    public partial class reader_ticket
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short readerticket_id { get; set; }

        [Required]
        public string surname_vistor { get; set; }

        [Required]
        public string first_name_visitor { get; set; }

        [Required]
        public string patronymic_author { get; set; }

        [Required]
        public string home_phone { get; set; }

        [Required]
        public string vis_address { get; set; }

        public DateTime ticket_issue_date { get; set; }

        public short? staff_id { get; set; }



    }

    public partial class work_staff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short work_staff_id { get; set; }

        public short? staff_id { get; set; }

        public short? work_day_id { get; set; }


    }

    public partial class issuance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long issuance_id { get; set; }

        public DateTime? date_is_Book { get; set; }

        public DateTime? date_re_Book { get; set; }

        public DateTime? date_reser { get; set; }

        public short? readerticket_id { get; set; }

        public long? book_id { get; set; }


    }

    public partial class publi_house
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short publisher_id { get; set; }

        [Required]
        [MaxLength(50)]
        public string publisher_name { get; set; }

        [Required]
        [MaxLength(50)]
        public string publisher_phone { get; set; }


    }

    public partial class dict_cities
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short city_id { get; set; }

        [Required]
        [MaxLength(50)]
        public string city_name { get; set; }


    }

    public partial class staff_eve
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short staff_eve_id { get; set; }

        public short? event_id { get; set; }

        public short? staff_id { get; set; }


    }
    public class ChangeRoleViewModel
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        public ChangeRoleViewModel()
        {
            AllRoles = new List<IdentityRole>();
            UserRoles = new List<string>();
        }
    }



   




    public class BookDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long year_creat { get; set; }
        public long cost { get; set; }



    }


    public class BookDetailDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long year_creat { get; set; }

        public long cost { get; set; }
    }

}