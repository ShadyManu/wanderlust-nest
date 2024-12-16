using System.Net.Http.Json;
using Application.Commons.Result;
using Application.Dto.Todo;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IntegrationTests.Tests;

[Collection("Todos")]
public class TodoTests : BaseIntegrationTest
{
    private static readonly Guid UserId = Guid.NewGuid();
    private readonly List<TodoEntity> _todoEntitiesSeed =
    [
        new() { Title = "Test Todo 1", Description = "Description 1", CreatedBy = UserId, LastModifiedBy = UserId },
        new() { Title = "Test Todo 2", Description = "Description 2", CreatedBy = UserId, LastModifiedBy = UserId },
        new() { Title = "Test Todo 3", Description = "Description 3", CreatedBy = UserId, LastModifiedBy = UserId },
    ];

    private const string BaseEndpoint = "/api/todo";
    
    public TodoTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
        var scope = factory.Services.CreateScope();
        
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        PrepareTestData(context);
    }
    
    [Fact]
    public async Task GetAll_ShouldReturn_AllEntities()
    {
        // Arrange
        var minLengthResult = _todoEntitiesSeed.Count;

        // Act
        var response = await HttpClient.GetAsync(BaseEndpoint);

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<Result<List<TodoDto>>>();
        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.Data.Count >= minLengthResult);
    }

    [Fact]
    public async Task Get_ShouldReturn_OneEntity()
    {
        // Arrange
        var entity = _todoEntitiesSeed.First();
        
        // Act
        var response = await HttpClient.GetAsync($"{BaseEndpoint}/{entity!.Id}");

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<Result<TodoDto>>();
        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.Equal(entity.Id, result.Data.Id);
        Assert.Equal(entity.Title, result.Data.Title);
        Assert.Equal(entity.Description, result.Data.Description);
    }

    [Fact]
    public async Task Create_ShouldReturn_CreatedEntity()
    {
        // Arrange
        var entityToCreate = new CreateTodoDto
        {
            Title = "New entity created title",
            Description = "New entity created description"
        };

        // Act
        var response = await HttpClient.PostAsJsonAsync($"{BaseEndpoint}", entityToCreate);

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<Result<TodoDto>>();
        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(Guid.TryParse(result.Data.Id.ToString(), out _));
        Assert.Equal(entityToCreate.Title, result.Data.Title);
        Assert.Equal(entityToCreate.Description, result.Data.Description);
    }

    [Fact]
    public async Task Patch_ShouldReturn_UpdatedEntity()
    {
        // Arrange
        var entityToUpdate = _todoEntitiesSeed.First();
        var updatedEntity = new UpdateTodoDto
        {
            Id = entityToUpdate.Id,
            Title = "Updated title",
            Description = "Updated description"
        };

        // Act
        var response = await HttpClient.PatchAsJsonAsync($"{BaseEndpoint}", updatedEntity);

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<Result<TodoDto>>();
        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.Equal(updatedEntity.Id, result.Data.Id);
        Assert.Equal(updatedEntity.Title, result.Data.Title);
        Assert.Equal(updatedEntity.Description, result.Data.Description);
    }

    [Fact]
    public async Task Delete_ShouldReturn_True()
    {
        // Arrange
        var entityToDelete = _todoEntitiesSeed.Last();

        // Act
        var response = await HttpClient.DeleteAsync($"{BaseEndpoint}/{entityToDelete.Id}");

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<Result<bool>>();
        Assert.NotNull(result);
        Assert.True(result.Data);
    }

    [Fact]
    public async Task DeleteAll_ShouldReturn_True()
    {
        // Act
        var response = await HttpClient.DeleteAsync(BaseEndpoint);

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<Result<bool>>();
        Assert.NotNull(result);
        Assert.True(result.Data);
    }
    
    private void PrepareTestData(ApplicationDbContext context)
    {
        // Remove eventual existing datas
        context.Todos.RemoveRange(context.Todos);
        
        context.Todos.AddRange(_todoEntitiesSeed);
        context.SaveChanges();
    }
}