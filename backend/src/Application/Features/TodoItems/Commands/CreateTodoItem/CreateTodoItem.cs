using AutoMapper;
using CCTemplate.Application.Common.Interfaces;
using CCTemplate.Application.Dtos.TodoItem;
using CCTemplate.Domain.Entities;
using MediatR;

namespace CCTemplate.Application.Features.TodoItems.Commands.CreateTodoItem;

public record CreateTodoItemCommand : IRequest<TodoItemDto>
{
    public required string Title { get; init; }
    public required int Priority { get; set; }
}

public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, TodoItemDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateTodoItemCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TodoItemDto> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new TodoItemEntity
        {
            Title = request.Title,
            Priority = request.Priority
        };

        _context.TodoItems.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<TodoItemDto>(entity);
    }
}
