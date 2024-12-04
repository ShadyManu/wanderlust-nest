using FluentValidation;

namespace Application.Features.Note.Commands.Update;

public class UpdateNoteCommandValidators : AbstractValidator<UpdateNoteCommand>
{
    public UpdateNoteCommandValidators()
    {
        RuleFor(e => e.UpdatedNote.Text).NotEmpty().WithMessage("Text cannot be empty");
        RuleFor(e => e.UpdatedNote.NoteId).NotEmpty().WithMessage("Note Id cannot be empty");
    }
}