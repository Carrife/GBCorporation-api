using GB_Corporation.Models;
using Microsoft.EntityFrameworkCore;

namespace GB_Corporation.Data
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }
        
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SuperDictionary> SuperDictionaries { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<TestCompetencies> TestCompetencies { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<ApplicantHiringData> ApplicantHiringDatas { get; set; }
        public DbSet<HiringData> HiringDatas { get; set; }
        public DbSet<ApplicantForeignLanguageTest> ApplicantForeignLanguageTests { get; set; }
        public DbSet<ApplicantLogicTest> ApplicantLogicTests { get; set; }
        public DbSet<ApplicantProgrammingTest> ApplicantProgrammingTests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(x => x.Email).IsUnique();
                entity.HasIndex(x => x.Login).IsUnique();
            });

            modelBuilder.Entity<Applicant>(entity =>
            {
                entity.HasIndex(x => x.Login).IsUnique();
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

    }
}
