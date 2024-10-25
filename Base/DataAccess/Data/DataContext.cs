using DataAccess.Configurations.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.DataModels;
using Models.IdentityModels;

namespace DataAccess.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategorySeed());
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<SampleForm> SampleForms { get; set; }
        public DbSet<FileForm> FileForms { get; set; }
        public DbSet<MultiFile> MultiFiles{ get; set; }
        public DbSet<MultiFileForm> MultiFileForms { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}