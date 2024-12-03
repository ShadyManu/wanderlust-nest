namespace Application.Commons.Result;

public class Result<TResult>
{
    public TResult? Data { get; set; }
    public Exception? Error { get; set; }

    private Result(TResult? data)
    {
        Data = data;
        Error = null;
    }

    private Result(Exception error)
    {
        Data = default;
        Error = error;
    }
    
    public static Result<TResult> Success(TResult value) =>
        new(value);

    public static Result<TResult> Failure(Exception error) =>
        new(error);
}