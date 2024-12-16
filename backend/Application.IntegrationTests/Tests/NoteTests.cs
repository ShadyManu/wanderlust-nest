using System.Net.Http.Json;
using Application.Commons.Result;
using Application.Dto.Todo;
using Application.IntegrationTests.Utilities;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

[assembly: CollectionBehavior(DisableTestParallelization = false)]

namespace Application.IntegrationTests.Tests;

[Collection("Notes")]
public class NoteTests : BaseIntegrationTest
{
    private static readonly Guid UserId = Guid.NewGuid();
    private readonly List<NoteEntity> _noteEntitiesSeed =
    [
        new() { Text = "Test Todo 1", IsFavourite = false, CreatedBy = UserId, LastModifiedBy = UserId},
        new() { Text = "Test Todo 2", IsFavourite = false, CreatedBy = UserId, LastModifiedBy = UserId },
        new() { Text = "Test Todo 3", IsFavourite = false, CreatedBy = UserId, LastModifiedBy = UserId },
    ];

    private const string BaseEndpoint = "/api/notes";
    
    public NoteTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
        var scope = factory.Services.CreateScope();
        
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        PrepareTestData(context);
    }
    
    [Fact]
    public async Task GetAll_ShouldReturn_AllEntities()
    {
        // Arrange
        var accessToken = await AuthUtilities.GetTokenAsync(HttpClient);
        HttpClient.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
        
        var minLengthResult = _noteEntitiesSeed.Count;

        // Act
        var response = await HttpClient.GetAsync(BaseEndpoint);

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<Result<List<TodoDto>>>();
        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.Data.Count >= minLengthResult);
    }

    private void PrepareTestData(ApplicationDbContext context)
    {
        // Remove eventual existing datas
        context.Notes.RemoveRange(context.Notes);
        
        context.Notes.AddRange(_noteEntitiesSeed);
        context.SaveChanges();
    }
}