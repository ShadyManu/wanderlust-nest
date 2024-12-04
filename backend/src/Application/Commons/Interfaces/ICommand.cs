using Application.Commons.Result;
using MediatR;

namespace Application.Commons.Interfaces;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>;

public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, Result<TResult>> 
    where TCommand : ICommand<TResult>;