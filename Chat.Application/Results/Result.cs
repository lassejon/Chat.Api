namespace Chat.Application.Results
{
    public class Result(bool isSuccess, string errorMessage) : Result<object>(isSuccess, errorMessage);
    
    public class Result<TValue> where TValue : class
    {
        private Result(bool isSuccess, Error error, TValue? value = null)
        {
            if (isSuccess && error != Error.None ||
                !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }

            IsSuccess = isSuccess;
            Error = error;
            Value = value;
        }

        public Result(bool isSuccess, string message, TValue? value = null)
        {
            IsSuccess = isSuccess;
            Message = message;
            Value = value;
        }

        public TValue? Value { get; } = null;

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public string? Message { get; }
        public Error? Error { get; }

        public static Result<TValue> Success() => new(true, Error.None);

        public static Result<TValue> Failure(Error error) => new(false, error);
    }

    public sealed record Error(string Code, string Description)
    {
        public static readonly Error None = new(string.Empty, string.Empty);
    }
}
