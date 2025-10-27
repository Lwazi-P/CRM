using CRM_ManagementInterface.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace CRM_ManagementInterface.Data
{

    public class CRMContext : DbContext
    {
        public CRMContext(DbContextOptions<CRMContext> options) : base(options) { }

        public DbSet<Clients> Clients { get; set; }
        public DbSet<Title> Title { get; set; }
        public DbSet<ClientType> ClientTypes { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<LoginDetail> LoginDetails { get; set; }
        public DbSet<Employee> Employees { get; set; }

      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginDetail>()
                .HasNoKey();
            
            modelBuilder.Entity<Clients>()
                .HasOne(c => c.ClientType)
                .WithMany()
                .HasForeignKey(c => c.ClientTypeId)
                .OnDelete(DeleteBehavior.Restrict);

           
            modelBuilder.Entity<Clients>()
                .Ignore(c => c.ClientTypeName);

           
            modelBuilder.Entity<Clients>()
                .HasOne(c => c.Title)
                .WithMany()
                .HasForeignKey(c => c.TitleId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

    }

}
