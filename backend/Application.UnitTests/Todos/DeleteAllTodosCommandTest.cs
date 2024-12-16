using Application.Commons.Interfaces;
using Application.Commons.Interfaces.Repositories;
using Application.Features.ToDo.Commands.Delete;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace Application.UnitTests.Todos;

public class DeleteAllTodosCommandTest
{
    private readonly DeleteAllTodosCommandHandler _handler;
    private readonly ITodoRepository _repository;

    public DeleteAllTodosCommandTest()
    {
        _repository = Substitute.For<ITodoRepository>();
        _handler = new DeleteAllTodosCommandHandler(_repository);
    }

    // [Fact]
    // public async Task Handle_ShouldReturnSuccess_WhenTodoExists()
    // {
    //     // Arrange
    //     // List<TodoEntity> entities =
    //     // [
    //     //     new() { Title = "Test title" },
    //     //     new() { Title = "Test title2" }
    //     // ];
    //     // var queryable = entities.AsQueryable();
    //     // _contextMock.Todos.Returns(queryable);
    //     // var command = new DeleteAllTodosCommand();
    //     //
    //     // // Act
    //     // var result = await _handler.Handle(command, default);
    //     //
    //     // // Assert
    //     // Assert.True(result.Data);
    //     
    //     
    //     
    //     // Arrange
    //     var cancellationToken = new CancellationTokenSource().Token;
    //     var entities = new List<TodoEntity>
    //     {
    //         new() { Title = "Test title", Description = "Test description" },
    //         new() { Title = "Test title2", Description = "Test description2" }
    //     };
    //
    //     // Crea un IQueryable mock
    //     var mockQueryable = Substitute.For<IQueryable<TodoEntity>>();
    //     mockQueryable.Provider.Returns(entities.AsQueryable().Provider);
    //     mockQueryable.Expression.Returns(entities.AsQueryable().Expression);
    //     mockQueryable.ElementType.Returns(entities.AsQueryable().ElementType);
    //     mockQueryable.GetEnumerator().Returns(entities.AsQueryable().GetEnumerator());
    //
    //     // Configura il mock del contesto per restituire l'IQueryable mock
    //     _repository.Todos.Returns(mockQueryable);
    //
    //     // Configura SaveChangesAsync per restituire 1
    //     _contextMock.SaveChangesAsync(Arg.Any<CancellationToken>())
    //         .Returns(1);
    //
    //     var command = new DeleteAllTodosCommand();
    //
    //     // Act
    //     var result = await _handler.Handle(command, cancellationToken);
    //
    //     // Assert
    //     Assert.True(result.Data);
    //
    //     // Verifica che RemoveRange sia stato chiamato con le entità corrette
    //     _contextMock.Received(1).Todos.RemoveRange(Arg.Is<List<TodoEntity>>(
    //         x => x.Count == entities.Count && 
    //              x.All(e => entities.Contains(e))
    //     ));
    // }
}