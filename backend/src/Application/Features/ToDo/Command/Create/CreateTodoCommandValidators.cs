using FluentValidation;
using MediatR;

namespace Application.Features.ToDo.Command.Create;

public class CreateTodoCommandValidators : AbstractValidator<CreateTodoCommand>
{
    public CreateTodoCommandValidators()
    {
        RuleFor(p => p.Title)
            .NotNull()
            .NotEmpty()
            .WithMessage("Title is required.");
    }
}