using FluentValidation;

namespace Application.Features.ToDo.Commands.Update;

public class UpdateTodoCommandValidators : AbstractValidator<UpdateTodoCommand>
{
    public UpdateTodoCommandValidators()
    {
        RuleFor(p => p.UpdatedEntry.Title)
            .NotNull()
            .NotEmpty()
            .WithMessage("Title is required.");
    }
}