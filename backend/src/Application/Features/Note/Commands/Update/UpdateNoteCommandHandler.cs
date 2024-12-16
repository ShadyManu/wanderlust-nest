using Application.Commons.Interfaces;
using Application.Commons.Interfaces.Repositories;
using Application.Commons.Result;
using Application.Dto.Note;
using Mapster;

namespace Application.Features.Note.Commands.Update;

public record UpdateNoteCommand(UpdateNoteDto UpdatedNote) : ICommand<NoteDto>;

public class UpdateNoteCommandHandler(INoteRepository repository) : ICommandHandler<UpdateNoteCommand, NoteDto>
{
    public async Task<Result<NoteDto>> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var noteToEdit = await repository.GetByIdAsync(request.UpdatedNote.NoteId, cancellationToken);
        if (noteToEdit is null) return Result<NoteDto>.Failure("Note not found");
        
        request.UpdatedNote.Adapt(noteToEdit);
        var result = await repository.SaveChangesAsync(cancellationToken);
        
        return result > 0 ? Result<NoteDto>.Success(noteToEdit.Adapt<NoteDto>()) : Result<NoteDto>.Failure("Error");
    }
}