using Application.Commons.Interfaces.Repositories;
using Application.Dto.Todo;
using Application.Features.ToDo.Commands.Update;
using Domain.Entities;
using NSubstitute;

namespace Application.UnitTests.Todos;

public class UpdateTodoCommandTest
{
    private static readonly UpdateTodoCommandValidators CommandValidators = new ();
    
    private readonly UpdateTodoCommandHandler _handler;
    private readonly ITodoRepository _repository;

    public UpdateTodoCommandTest()
    {
        _repository = Substitute.For<ITodoRepository>();
        _handler = new UpdateTodoCommandHandler(_repository);
    }

    [Fact]
    public void Validator_ShouldReturnError_WhenGivenUpdatedTodoHasNullTitle()
    {
        // Arrange
        var command = new UpdateTodoCommand(UpdatedEntry: new UpdateTodoDto { Title = null! });
        
        // Act
        var result = CommandValidators.Validate(command);
        var nullTitleError = 
            result.Errors.Find(e =>
                e.ErrorCode.Equals("NotNullValidator") && e.PropertyName.Equals("UpdatedEntry.Title"));
        
        // Assert
        Assert.NotNull(nullTitleError);
    }
    
    [Fact]
    public void Validator_ShouldReturnError_WhenGivenUpdatedTodoHasEmptyTitle()
    {
        // Arrange
        var command = new UpdateTodoCommand(UpdatedEntry: new UpdateTodoDto { Title = string.Empty });
        
        // Act
        var result = CommandValidators.Validate(command);
        var emptyTitleError = 
            result.Errors.Find(e =>
                e.ErrorCode.Equals("NotEmptyValidator") && e.PropertyName.Equals("UpdatedEntry.Title"));
        
        // Assert
        Assert.NotNull(emptyTitleError);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenUpdateTodoDoesNotExistsInsideDatabase()
    {
        // Arrange
        var updatedEntity = new UpdateTodoDto
        {
            Id = Guid.NewGuid(),
            Title = "Updated title",
            Description = "Updated description"
        };
        var cancellationToken = new CancellationTokenSource().Token;

        _repository
            .GetByIdAsync(updatedEntity.Id, cancellationToken)
            .Returns((TodoEntity)null!);
        
        var command = new UpdateTodoCommand(updatedEntity);

        // Act
        var result = await _handler.Handle(command, cancellationToken);

        // Assert
        Assert.NotNull(result.Error);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenGivenUpdatedTodoIsValid()
    {
        // Arrange
        var userid = Guid.NewGuid();
        var oldEntity = new TodoEntity
        {
            Id = Guid.NewGuid(),
            Title = "Old entity title",
            Description = "Old entity description",
            CreatedBy = userid,
            LastModifiedBy = userid
        };
        var updatedEntity = new UpdateTodoDto
        {
            Id = oldEntity.Id,
            Title = "Updated title",
            Description = "Updated description"
        };
        var cancellationToken = new CancellationTokenSource().Token;
        
        _repository
            .GetByIdAsync(oldEntity.Id, cancellationToken)
            .Returns(oldEntity);
        
        var command = new UpdateTodoCommand(updatedEntity);

        // Act
        var result = await _handler.Handle(command, cancellationToken);

        // Assert
        Assert.NotNull(result.Data);
        Assert.Equal(updatedEntity.Id, result.Data.Id);
        Assert.Equal(updatedEntity.Title, result.Data.Title);
        Assert.Equal(updatedEntity.Description, result.Data.Description);
    }
}