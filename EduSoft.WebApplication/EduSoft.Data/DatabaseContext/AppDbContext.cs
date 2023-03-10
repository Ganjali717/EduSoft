using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduSoft.Entities.Comments;
using Microsoft.EntityFrameworkCore;
using EduSoft.Entities.Cources;
using EduSoft.Entities.Discussions;
using EduSoft.Entities.Jobs;
using EduSoft.Entities.Security;
using EduSoft.Entities.Tutorials;

namespace EduSoft.Data.DatabaseContext
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Subchapter> Subchapters { get; set; }
        public DbSet<Tutorial> Tutorials { get; set; }
        public DbSet<SubChapterIntro> SubchIntros { get; set; }
    }
}
