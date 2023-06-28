using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using WebDevSem2ClientMVC.Areas.Identity.Data;
using WebDevSem2ClientMVC.Models;

namespace WebDevSem2ClientMVC.Areas.Identity.Data;

public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        Seed(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }
    private static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DeveloperProfile>().HasData(
            new
            {
                DeveloperProfileId = 1,
                Name = "Ingvar Schoenmaker",
                Skills = "Ik heb verschillende skills zoals c#, js en Python",
                Discription = "Mijn naam is Ingvar Schoenmaker en ik ben 23 jaar oud.\r\n Ik volg de opleiding HBO-ICT en heb de richting ontwikkeling gekozen.\r\n Momenteel zit ik in mijn laatste jaar maar heb voor een reparatiesimester gekozen.\r\n",
                PictureURL = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.youtube.com%2Fuser%2Fingvarschoenmaker&psig=AOvVaw2ky-X0nW2dZBTyBvFecmhq&ust=1687858638390000&source=images&cd=vfe&ved=0CBEQjRxqFwoTCPDm5MTR4P8CFQAAAAAdAAAAABAE",
                Email = "ingvar.schoenmaker@windesheim.nl"
            }
        );
    }

    public DbSet<WebDevSem2ClientMVC.Models.DeveloperProfile> DeveloperProfile { get; set; } = default!;
}
