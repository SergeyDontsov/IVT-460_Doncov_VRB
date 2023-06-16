using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestDiplom.Areas.AdminPanel.Models;
using TestDiplom.Areas.Identity.Data;
using TestDiplom.Models;


namespace TestDiplom.Areas.Identity.Data;

public class TestAuth : IdentityDbContext<IdentityUser>
{
    public TestAuth(DbContextOptions<TestAuth> options)
        : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TestDiplom;Integrated Security=True;Connect Timeout=30;Encrypt=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;TrustServerCertificate=True;Trusted_Connection=True");
        }
    }

    
}


