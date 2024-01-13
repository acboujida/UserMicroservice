using Microsoft.EntityFrameworkCore;
using UserMicroservice.Models;

namespace UserMicroservice.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Livre> Livres { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        //public DbSet<UserUser> UserUsers { get; set; }
        public DbSet<Annonce> Annonces { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<LivreUser> LivreUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<UserUser>().HasKey(uu => new {uu.FriendId, uu.UserId});
            modelBuilder.Entity<UserUser>().HasOne(uu => uu.User).WithMany(u => u.Friends).HasForeignKey(uu => uu.UserId);
            modelBuilder.Entity<UserUser>().HasOne(uu => uu.Friend).WithMany(u => u.Friends).HasForeignKey(uu => uu.FriendId);*/
            modelBuilder.Entity<LivreUser>().HasKey(lu => new { lu.LivreId, lu.UserId });
            modelBuilder.Entity<LivreUser>().HasOne(lu => lu.User).WithMany(u => u.LivreUsers).HasForeignKey(lu => lu.UserId);
            modelBuilder.Entity<LivreUser>().HasOne(lu => lu.Livre).WithMany(l => l.LivreUsers).HasForeignKey(lu => lu.LivreId);
        }
    }
}
