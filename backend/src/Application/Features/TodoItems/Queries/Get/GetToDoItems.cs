using AutoMapper;
using CCTemplate.Application.Common.Interfaces;
using CCTemplate.Application.Dtos.TodoItem;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CCTemplate.Application.Features.TodoItems.Queries.Get;
public record GetToDoItemsQuery : IRequest<List<TodoItemDto>>;

public class GetTodoItemsQueryHandler : IRequestHandler<GetToDoItemsQuery, List<TodoItemDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTodoItemsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TodoItemDto>> Handle(GetToDoItemsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.TodoItems.ToListAsync(cancellationToken);
        return _mapper.Map<List<TodoItemDto>>(entities);
    }
}
