using FSD.Context.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FSD.Service
{
    public class UserDbContext : DbContext
    {
        public UserDbContext( DbContextOptions options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInformation>().HasKey(x => x.Id);
            modelBuilder.Entity<UserInformation>().Property(x => x.Id).HasDefaultValue("NEWID()");

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<UserInformation> UsersInformation { get; set; }
    }
}
