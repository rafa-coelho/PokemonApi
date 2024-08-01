namespace Pokedex.AspNetCore.Exceptions;

public class ApiException : Exception
{
    public int StatusCode { get; }

    public ApiException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }

    public ApiException(int statusCode, string message, Exception innerException) : base(message, innerException)
    {
        StatusCode = statusCode;
    }

    public static ApiException BadRequest(string message) => new ApiException(400, message);
    public static ApiException Unauthorized(string message) => new ApiException(401, message);
    public static ApiException NotFound(string message) => new ApiException(404, message);
}
