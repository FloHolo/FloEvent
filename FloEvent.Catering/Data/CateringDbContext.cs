using Microsoft.EntityFrameworkCore;

namespace FloEvent.Catering.Data
{
    public class CateringDbContext :DbContext
    {

        public DbSet<Menu> Menu { get; set; }
        public DbSet<FoodItem> FoodItem { get; set; }
        public DbSet<MenuFoodItems> MenuFoodItems { get; set; }
        public DbSet<FoodBooking> FoodBooking { get; set; }

        private string DbPath { get; }

        public CateringDbContext()
        {
            var folder = Environment.SpecialFolder.MyDocuments;
            var path = Environment.GetFolderPath(folder);

            DbPath = Path.Join(path, "FloEvent.Catering.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Composite key for MenuFoodItem 
            modelBuilder.Entity<MenuFoodItems>()
                .HasKey(ts => new { ts.MenuId, ts.FoodItemId });

            //Define relationships for junciton  table
            modelBuilder.Entity<Menu>()
                .HasMany(m => m.MenuFoodItems)
                .WithOne(c => c.Menu)
                .HasForeignKey(mfi => mfi.MenuId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FoodItem>()
                .HasMany(m => m.MenuFoodItems)
                .WithOne(c => c.FoodItem)
                .HasForeignKey(mfi => mfi.FoodItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
