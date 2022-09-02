using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
           
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Blog>()
                .HasData(
                    new Blog
                    {
                        ArticleId = 1,
                        Title = "John Doe",
                        Description = "Totally",
                        PublishedDate = DateTime.Now
                    },
                    new Blog
                    {
                        ArticleId = 2,
                        Title = "John Silver",
                        Description = "Totally Brave",
                        PublishedDate = DateTime.Now
                    },

                     new Blog
                     {
                         ArticleId = 3,
                         Title = "Rita Silver",
                         Description = "Totally Awesome",
                         PublishedDate = DateTime.Now
                     }
                );
        }
        //entities
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}
