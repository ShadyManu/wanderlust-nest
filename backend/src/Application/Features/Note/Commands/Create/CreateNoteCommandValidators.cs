using FluentValidation;

namespace Application.Features.Note.Commands.Create;

public class CreateNoteCommandValidators : AbstractValidator<CreateNoteCommand>
{
    public CreateNoteCommandValidators()
    {
        RuleFor(e => e.Note.Text)
            .NotEmpty()
            .WithMessage("Note text is required.");
    }
}