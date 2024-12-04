using FluentValidation;

namespace Application.Features.ToDo.Commands.Create;

public class CreateTodoCommandValidators : AbstractValidator<CreateTodoCommand>
{
    public CreateTodoCommandValidators()
    {
        RuleFor(p => p.CreateTodoDto.Title)
            .NotNull()
            .NotEmpty()
            .WithMessage("Title is required.");
    }
}