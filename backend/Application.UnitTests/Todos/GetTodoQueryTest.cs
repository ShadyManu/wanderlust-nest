using Application.Commons.Interfaces.Repositories;
using Application.Features.ToDo.Queries.Get;
using Domain.Entities;
using NSubstitute;

namespace Application.UnitTests.Todos;

public class GetTodoQueryTest
{
    private readonly ITodoRepository _repository;
    private readonly GetTodoQueryHandler _handler;

    public GetTodoQueryTest()
    {
        _repository = Substitute.For<ITodoRepository>();
        _handler = new GetTodoQueryHandler(_repository);
    }

    [Fact]
    public async Task Handler_ShouldReturnError_WhenTodoDoesNotExistsInsideDatabase()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var cancellationToken = new CancellationTokenSource().Token;

        _repository.GetByIdAsNoTrackingAsync(Arg.Is(guid), Arg.Is(cancellationToken))
            .Returns((TodoEntity)null!);

        var query = new GetTodoQuery(guid);

        // Act
        var result = await _handler.Handle(query, cancellationToken);

        // Assert
        Assert.NotNull(result.Error);
    }

    [Fact]
    public async Task Handler_ShouldReturnSuccess_WhenTodoExists()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var cancellationToken = new CancellationTokenSource().Token;
        var userId = Guid.NewGuid();
        var entityFound = new TodoEntity
        {
            Id = guid,
            Title = "Test Title",
            Description = "Test description",
            CreatedBy = userId,
            LastModifiedBy = userId
        };

        _repository.GetByIdAsNoTrackingAsync(Arg.Is(guid), Arg.Is(cancellationToken))
            .Returns(entityFound);

        var query = new GetTodoQuery(guid);

        // Act
        var result = await _handler.Handle(query, cancellationToken);

        // Assert
        Assert.NotNull(result.Data);
        Assert.Equal(entityFound.Id, result.Data.Id);
        Assert.Equal(entityFound.Title, result.Data.Title);
        Assert.Equal(entityFound.Description, result.Data.Description);
    }
}