using DocumentAccessApprovalSystemAPI.Entities;
using DocumentAccessApprovalSystemAPI.Enums;
using Microsoft.EntityFrameworkCore;

namespace DocumentAccessApprovalSystemAPI.DbContexts
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        public DbSet<Document> Documents { get; set; }
        public DbSet<AccessRequest> AccessRequests { get; set; }
        public DbSet<Decision> Decisions { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>().HasData(
                new Document()
                {
                    Id = 1,
                    FileName = "File1.pdf"
                },
                new Document()
                {
                    Id = 2,
                    FileName = "File2.pdf"
                },
                new Document()
                {
                    Id = 3,
                    FileName = "File3.pdf"
                });

            modelBuilder.Entity<AccessRequest>().ToTable("AccessRequests");
            modelBuilder.Entity<Decision>().ToTable("Decisions");
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
                    Name = "Pawel_user",
                    Role = Role.User,
                    Email = "Pawel_user@wp.pl"
                },
                new User()
                {
                    Id = 2,
                    Name = "Gawel_approver",
                    Role = Role.Approver,
                    Email = "gawel_approver@wp.pl"
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
