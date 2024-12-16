using Application.Commons.Interfaces;
using Application.Commons.Interfaces.Repositories;
using Application.Commons.Result;

namespace Application.Features.Note.Commands.Delete;

public record DeleteNoteCommand(Guid NoteId) : ICommand<bool>;

internal sealed class DeleteNoteCommandHandler(INoteRepository repository) : ICommandHandler<DeleteNoteCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var result = await repository.DeleteAsync(request.NoteId, cancellationToken);
        
        return result == 1 ? Result<bool>.Success(true) : Result<bool>.Failure("Failed to delete note");
    }
}