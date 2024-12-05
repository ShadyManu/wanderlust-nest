using Application.Commons.Interfaces;
using Application.Commons.Result;
using Application.Dto.Note;
using Domain.Entities;
using Mapster;

namespace Application.Features.Note.Commands.Create;

public record CreateNoteCommand(CreateNoteDto Note) : ICommand<NoteDto>;

internal sealed class CreateNoteCommandHandler(IApplicationDbContext context, IUser User) : ICommandHandler<CreateNoteCommand, NoteDto>
{
    public async Task<Result<NoteDto>> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var entity = request.Note.Adapt<NoteEntity>();
        
        await context.Notes.AddAsync(entity, cancellationToken);
        var result = await context.SaveChangesAsync(cancellationToken);
        
        return result > 0 
            ? Result<NoteDto>.Success(entity.Adapt<NoteDto>()) 
            : Result<NoteDto>.Failure("Failed to create note"); 
    }
}