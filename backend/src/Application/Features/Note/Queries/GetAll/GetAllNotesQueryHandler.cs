using Application.Commons.Interfaces;
using Application.Commons.Result;
using Application.Dto.Note;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Note.Queries.GetAll;

public record GetAllNotesQuery : IQuery<List<NoteDto>>;

public class GetAllNotesQueryHandler(IApplicationDbContext context) : IQueryHandler<GetAllNotesQuery, List<NoteDto>>
{
    public async Task<Result<List<NoteDto>>> Handle(GetAllNotesQuery request, CancellationToken cancellationToken)
    {
        var entities = await context.Notes
            .OrderByDescending(e => e.LastModified)
            .ToListAsync(cancellationToken);
        
        return Result<List<NoteDto>>.Success(entities.Adapt<List<NoteDto>>());
    }
}