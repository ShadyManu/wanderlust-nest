using Application.Commons.Result;

namespace Application.Commons.Interfaces;

public interface ICommandBase;

public interface ICommand<TResponse> : ICommandBase;

public interface ICommandHandler<in TCommand, TResult>
{
    Task<Result<TResult>> Handle(TCommand command, CancellationToken cancellationToken);
}