﻿using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using WebDevSem2ClientMVC.Models;

namespace WebDevSem2API.Entities;

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
        Seed(modelBuilder);
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
                PictureURL = "https://media.licdn.com/dms/image/C5603AQGU4RhjRZQnxg/profile-displayphoto-shrink_800_800/0/1517319432686?e=1681344000&v=beta&t=F6_63VvXX5m6Vu3q0UfMy89AnpCZuCnPyTM64UleLs8",
                Email = "ingvar.schoenmaker@windesheim.nl"
            }
        ); ;
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<WebDevSem2ClientMVC.Models.ContactFormModel> ContactFormModel { get; set; } = default!;

    public DbSet<WebDevSem2ClientMVC.Models.DeveloperProfile> DeveloperProfile { get; set; } = default!;
}
