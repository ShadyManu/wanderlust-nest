using Application.Commons.Interfaces.Repositories;
using Domain.Entities;

namespace Infrastructure.Data.Repositories;

public class TodoRepository(ApplicationDbContext context) : BaseRepository<TodoEntity>(context), ITodoRepository;