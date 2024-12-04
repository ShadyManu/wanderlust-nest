using Application.Commons.Interfaces;
using Application.Commons.Result;

namespace Application.Features.Note.Commands.Delete;

public record DeleteNoteCommand(Guid NoteId) : ICommand<bool>;

internal sealed class DeleteNoteCommandHandler(IApplicationDbContext context) : ICommandHandler<DeleteNoteCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var noteToDelete = await context.Notes.FindAsync(request.NoteId, cancellationToken);
        
        if (noteToDelete is null) return Result<bool>.Failure("Note not found");

        context.Notes.Remove(noteToDelete);
        var result = await context.SaveChangesAsync(cancellationToken);
        
        return result == 1 ? Result<bool>.Success(true) : Result<bool>.Failure("Failed to delete note");
    }
}