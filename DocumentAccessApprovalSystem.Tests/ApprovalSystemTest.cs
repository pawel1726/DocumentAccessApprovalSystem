using DocumentAccessApprovalSystemAPI.DbContexts;
using DocumentAccessApprovalSystemAPI.Services;
using DocumentAccessApprovalSystemAPI.Enums;
using DocumentAccessApprovalSystemAPI.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace DocumentAccessApprovalSystem.Tests
{
    public class ApprovalSystemTest
    {
        [Fact]
        public async Task AddAccessRequestAsync_ValidUser_AddsRequestToUser()
        {
            //arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>()
                .UseSqlite(connection);

            var dbContext = new MyDbContext(optionsBuilder.Options);

            dbContext.Database.Migrate();  //collection fixture?
            var repository = new ApprovalSystemRepository(dbContext);
            var user = new DocumentAccessApprovalSystemAPI.Entities.User
            {
                Id = 3,
                Name = "Test User",
                Email = "test@wp.pl",
                Role = Role.User

            };
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            var request = new AccessRequest
            {
               // Id = 8,
                DocumentId = 3,
                Status = DecisionStatus.Pending,
                UserId = user.Id,
                AccessType = RequestAccessType.Edit
            };

            //act
            await repository.AddAccessRequestAsync(user.Id, request);
            await dbContext.SaveChangesAsync();
            //assert
            var accessRequest = await repository.GetAccessRequestForUserAsync(user.Id, request.Id);
            Assert.NotNull(accessRequest);
            Assert.Equal(request.DocumentId, accessRequest.DocumentId);
            Assert.Equal(request.Status, accessRequest.Status);
            Assert.Equal(user.Id, accessRequest.UserId);

        }

    }
}