using Application.Commons.Interfaces;
using Application.Commons.Interfaces.Repositories;
using Application.Features.Todo.Queries.GetAll;
using Domain.Entities;
using NSubstitute;

namespace Application.UnitTests.Todos;

public class GetAllTodosQueryTest
{
    private readonly ITodoRepository _context;
    private readonly GetAllTodosQueryHandler _handler;

    public GetAllTodosQueryTest()
    {
        _context = Substitute.For<ITodoRepository>();
        _handler = new GetAllTodosQueryHandler(_context);
    }

    [Fact]
    public async Task Handler_ShouldReturnEmptyList_WhenNoEntitiesInsideDatabase()
    {
        // Arrange
        List<TodoEntity> entities = [];
        var cancellationToken = new CancellationTokenSource().Token;
        
        _context.GetAllAsync(Arg.Is(cancellationToken))
            .Returns(entities);
        
        var query = new GetAllTodosQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result.Data);
        Assert.Empty(result.Data);
    }
}