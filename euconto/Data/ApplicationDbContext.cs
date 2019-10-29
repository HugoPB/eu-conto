using EuConto.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EuConto.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUserModel>
    {
        public DbSet<SettingsDataModel> Settings { get; set; }
        public DbSet<StoryDataModel> Storys { get; set; }
        public DbSet<ChapterDataModel> Chapters { get; set; }
        public DbSet<SectionDataModel> Sections { get; set; }
        public DbSet<InteractionDataModel> Interactions { get; set; }
        public DbSet<LikesDataModel> Likes { get; set; }
        public DbSet<ComentaryDataModel> Comentaries { get; set; }
        public DbSet<FollowersDataModel> Followers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //_context.Database.Migrate();
            //var x = _context.Settings.Count();
            //if (x < 1)
            //{
            //    _context.Settings.Add(new SettingsDataModel
            //    {
            //        Name = "Teste",
            //        Value = "1"
            //    });

            //    _context.SaveChanges();
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
