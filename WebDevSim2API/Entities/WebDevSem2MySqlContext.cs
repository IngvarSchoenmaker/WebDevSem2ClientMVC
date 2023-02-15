using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using WebDevSem2ClientMVC.Models;

namespace WebDevSim2API.Entities;

public partial class WebDevSem2MySqlContext : DbContext
{
    public WebDevSem2MySqlContext()
    {
    }

    public WebDevSem2MySqlContext(DbContextOptions<WebDevSem2MySqlContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<WebDevSem2ClientMVC.Models.ContactFormModel> ContactFormModel { get; set; } = default!;

    public DbSet<WebDevSem2ClientMVC.Models.DeveloperProfile> DeveloperProfile { get; set; } = default!;
}
