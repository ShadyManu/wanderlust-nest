using Application.Commons.Result;
using MediatR;

namespace Application.Commons.Interfaces;

public interface IQuery<TResponse>;

public interface IQueryHandler<in TQuery, TResponse> : IRequest<TResponse>
    where TQuery : IQuery<TResponse>
{
    Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken);
}