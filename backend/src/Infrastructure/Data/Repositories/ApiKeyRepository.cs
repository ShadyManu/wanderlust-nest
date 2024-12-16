using Application.Commons.Interfaces.Repositories;
using Domain.Entities;

namespace Infrastructure.Data.Repositories;

public class ApiKeyRepository(ApplicationDbContext context) : BaseRepository<ApiKeyEntity>(context), IApiKeyRepository;