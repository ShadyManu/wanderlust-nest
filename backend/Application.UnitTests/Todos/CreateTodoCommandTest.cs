using Application.Commons.Interfaces.Repositories;
using Application.Dto.Todo;
using Application.Features.ToDo.Commands.Create;
using Domain.Entities;
using NSubstitute;
using NSubstitute.Extensions;

namespace Application.UnitTests.Todos;

public class CreateTodoCommandTest
{
    private static readonly CreateTodoCommandValidators CommandValidators = new ();
    
    private readonly CreateTodoCommandHandler _handler;
    private readonly ITodoRepository _repository;

    public CreateTodoCommandTest()
    {
        _repository = Substitute.For<ITodoRepository>();
        _handler = new CreateTodoCommandHandler(_repository);
    }

    [Fact]
    public void Validator_ShouldReturnError_WhenGivenTodoHasNullTitle()
    {
        // Arrange
        var command = new CreateTodoCommand(CreateTodoDto: new CreateTodoDto { Title = null! });
        
        // Act
        var result = CommandValidators.Validate(command);
        var nullTitleError = 
            result.Errors.Find(e =>
                e.ErrorCode.Equals("NotNullValidator") && e.PropertyName.Equals("CreateTodoDto.Title"));
        
        // Assert
        Assert.NotNull(nullTitleError);
    }
    
    [Fact]
    public void Validator_ShouldReturnError_WhenGivenTodoHasEmptyTitle()
    {
        // Arrange
        var command = new CreateTodoCommand(CreateTodoDto: new CreateTodoDto { Title = string.Empty });
        
        // Act
        var result = CommandValidators.Validate(command);
        var emptyTitleError = 
            result.Errors.Find(e =>
                e.ErrorCode.Equals("NotEmptyValidator") && e.PropertyName.Equals("CreateTodoDto.Title"));
        
        // Assert
        Assert.NotNull(emptyTitleError);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenGivenTodoIsValid()
    {
        // Arrange
        var cancellationToken = new CancellationTokenSource().Token;
        var createToDoDto = new CreateTodoDto { Title = "Valid test title", Description = "Valid test description" };

        _repository
            .AddAsync(Arg.Any<TodoEntity>(), cancellationToken)
            .Returns(1);
        
        var command = new CreateTodoCommand(createToDoDto);
        
        // Act
        var result = await _handler.Handle(command, cancellationToken);
        
        // Assert
        Assert.NotNull(result.Data);
        Assert.Equal(result.Data.Title, createToDoDto.Title);
        Assert.Equal(result.Data.Description, createToDoDto.Description);
        Assert.True(Guid.TryParse(result.Data.Id.ToString(), out var id));
    }
}