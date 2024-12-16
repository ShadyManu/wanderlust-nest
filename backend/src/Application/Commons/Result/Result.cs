using System.Text.Json.Serialization;

namespace Application.Commons.Result;

public class Result<TResult>
{
    public TResult? Data { get; set; }
    public ResultError? Error { get; set; }
        
    // This constructor is used just for Application.IntegrationTests
    [JsonConstructor]
    private Result() {}

    private Result(TResult? data)
    {
        Data = data;
        Error = null;
    }

    private Result(ResultError error)
    {
        Data = default;
        Error = error;
    }
    
    public static Result<TResult> Success(TResult value) =>
        new(value);

    public static Result<TResult> Failure(ResultError error) =>
        new(error);
    public static Result<TResult> Failure(string error) =>
        new(new ResultError(error));
}

public record ResultError(string Message, string? InnerException = null);