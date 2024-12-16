using Application.Commons.Interfaces.Repositories;
using Application.Features.ToDo.Commands.Delete;
using Domain.Entities;
using NSubstitute;

namespace Application.UnitTests.Todos;

public class DeleteTodoCommandTest 
{
    private readonly DeleteTodoCommandHandler _handler;
    private readonly ITodoRepository _repository;

    public DeleteTodoCommandTest()
    {
        _repository = Substitute.For<ITodoRepository>();
        _handler = new DeleteTodoCommandHandler(_repository);
    }

    [Fact]
    public async Task Validate_ShouldReturnError_WhenTodoDoesNotExists()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var cancellationToken = new CancellationTokenSource().Token;
        
        _repository
            .DeleteAsync(guid, cancellationToken)
            .Returns(0);
        
        var command = new DeleteTodoCommand(guid);

        // Act
        var result = await _handler.Handle(command, cancellationToken);

        // Assert
        Assert.NotNull(result.Error);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenTodoExists()
    {
        // Arrange
        var cancellationToken = new CancellationTokenSource().Token;
        var userId = Guid.NewGuid();
        var entity = new TodoEntity
        {
            Title = "Test title",
            Description = "Test description",
            CreatedBy = userId,
            LastModifiedBy = userId
        };
     
        _repository
            .DeleteAsync(entity.Id, cancellationToken)
            .Returns(1);
        
        var command = new DeleteTodoCommand(entity.Id);

        // Act
        var result = await _handler.Handle(command, cancellationToken);

        // Assert
        Assert.True(result.Data);
    }
}