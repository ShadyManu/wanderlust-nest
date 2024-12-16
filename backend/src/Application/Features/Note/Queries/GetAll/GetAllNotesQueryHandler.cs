using Application.Commons.Interfaces;
using Application.Commons.Interfaces.Repositories;
using Application.Commons.Result;
using Application.Dto.Note;
using Mapster;

namespace Application.Features.Note.Queries.GetAll;

public record GetAllNotesQuery : IQuery<List<NoteDto>>;

public class GetAllNotesQueryHandler(INoteRepository repository) : IQueryHandler<GetAllNotesQuery, List<NoteDto>>
{
    public async Task<Result<List<NoteDto>>> Handle(GetAllNotesQuery request, CancellationToken cancellationToken)
    {
        var entities = (await repository
                .GetAllAsync(cancellationToken))
            .OrderByDescending(e => e.LastModified)
            .ToList();
        
        return Result<List<NoteDto>>.Success(entities.Adapt<List<NoteDto>>());
    }
}