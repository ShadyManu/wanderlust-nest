using Application.Commons.Interfaces.Repositories;
using Domain.Entities;

namespace Infrastructure.Data.Repositories;

public class NoteRepository(ApplicationDbContext context) : BaseRepository<NoteEntity>(context), INoteRepository;