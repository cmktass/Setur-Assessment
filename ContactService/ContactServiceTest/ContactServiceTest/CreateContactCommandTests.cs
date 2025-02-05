using Moq;
using ContactService.Application.Commands;
using ContactService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ContactService.Application;

namespace ContactServiceTest
{

    public class CreateContactCommandTests
    {
        private readonly CreateContactCommandValidator _validator;
        private readonly Mock<IContactServiceDbContext> _dbContextMock;
        private readonly CreateContactCommandHandler _handler;

        public CreateContactCommandTests()
        {
            _validator = new CreateContactCommandValidator();
            _dbContextMock = new Mock<IContactServiceDbContext>();
            _handler = new CreateContactCommandHandler(_dbContextMock.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldCreateContactAndReturnId()
        {
            // Arrange
            var command = new CreateContactCommand("John", "Doe", "Acme Inc.");
            var cancellationToken = CancellationToken.None;

            var dbSetMock = new Mock<DbSet<Contact>>();
            _dbContextMock.Setup(db => db.Contacts).Returns(dbSetMock.Object);

            _dbContextMock.Setup(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1); // Simulate successful save

            // Act
            var result = await _handler.Handle(command, cancellationToken);

            // Assert
            Assert.NotEqual(Guid.Empty, result);
            _dbContextMock.Verify(db => db.Contacts.Add(It.IsAny<Contact>()), Times.Once);
            _dbContextMock.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }

}