using Moq;
using MemberService.Application.Common.Interfaces.Persistence;
using MemberService.Application.Common.Interfaces;
using MemberService.Domain.Entities;
using FluentAssertions;
using MemberService.Application.Members.Commands.CreateMember;
using MemberService.Application.Members.Common;

namespace MemberService.Tests.Members;

public class CreateMemberTests
{
    private readonly Mock<IMemberRepository> _memberRepositoryMock;
    private readonly CreateMemberCommandHandler _handler;

    public CreateMemberTests()
    {
        _memberRepositoryMock = new Mock<IMemberRepository>();
        
        var currentUserMock = new Mock<IOperatorService>();
        currentUserMock.Setup(x => x.UserId).Returns("operator-123");

        _handler = new CreateMemberCommandHandler(
            _memberRepositoryMock.Object,
            currentUserMock.Object
        );
    }

    [Fact]
    public async Task Handle_Should_Create_Member_When_Valid()
    {
        // Arrange
        var command = new CreateMemberCommand("John", "Doe", "john.doe@example.com", new DateTime(1990, 1, 1));
        _memberRepositoryMock
            .Setup(r => r.GetByEmailAsync(command.Email))
            .ReturnsAsync((MemberDto?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBe(Guid.Empty);
        
        _memberRepositoryMock.Verify(r => r.AddAsync(It.Is<Member>(m =>
            m.FirstName == command.FirstName &&
            m.LastName == command.LastName &&
            m.Email == command.Email &&
            m.BirthDate == command.BirthDate &&
            m.CreatedBy == "operator-123"
        )), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Throw_When_Email_Already_Exists()
    {
        // Arrange
        var command = new CreateMemberCommand("John", "Doe", "john.doe@example.com", new DateTime(1990, 1, 1));

        _memberRepositoryMock
            .Setup(r => r.GetByEmailAsync(command.Email))
            .ReturnsAsync(new MemberDto { Id = Guid.NewGuid(), Email = command.Email });

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Email already exists");
    }
}