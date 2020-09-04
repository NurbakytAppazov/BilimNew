using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bilim.DbFolder
{
    public class AppDb : IdentityDbContext<User>
    {
        public AppDb(DbContextOptions<AppDb> options)
            : base(options)
        {

        }

        public DbSet<Kurs> Kurs { get; set; }
        public DbSet<KursVideo> KursVideos { get; set; }
        public DbSet<UserKurs> UserKurs { get; set; }


        public DbSet<KursComment> KursComments { get; set; }
        public DbSet<VideoComment> VideoComments { get; set; }

        public DbSet<FreeVideo> FreeVideos { get; set; }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Category2> Categories2 { get; set; }
        public DbSet<Category3> Categories3 { get; set; }
    }
}
