using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserMicroservice.Models;

namespace UserMicroservice.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }
        public DbSet<OwnedBook> OwnedBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<IdentityRole> roleList = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roleList);

            modelBuilder.Entity<BorrowedBook>().HasOne(bb => bb.Borrower).WithMany(u => u.BorrowedBooks).HasForeignKey(bb => bb.BorrowerId);
            modelBuilder.Entity<OwnedBook>().HasOne(ob => ob.Owner).WithMany(u => u.OwnedBooks).HasForeignKey(ob => ob.OwnerId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
