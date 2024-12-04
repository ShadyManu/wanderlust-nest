using Application.Commons.Interfaces;
using Application.Commons.Result;
using Application.Dto.Note;
using Mapster;

namespace Application.Features.Note.Commands.Update;

public record UpdateNoteCommand(UpdateNoteDto UpdatedNote) : ICommand<bool>;

public class UpdateNoteCommandHandler(IApplicationDbContext context) : ICommandHandler<UpdateNoteCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var noteToEdit = await context.Notes.FindAsync(request.UpdatedNote.NoteId);
        if (noteToEdit is null) return Result<bool>.Failure("Note not found");
        
        request.UpdatedNote.Adapt(noteToEdit);
        
        var result = await context.SaveChangesAsync(cancellationToken);
        
        return result > 0 ? Result<bool>.Success(true) : Result<bool>.Failure("Error");
    }
}