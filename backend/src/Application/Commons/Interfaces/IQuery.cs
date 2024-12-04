using Application.Commons.Result;
using MediatR;

namespace Application.Commons.Interfaces;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;