namespace Chat.Application.Results
{
    public class Result : Result<object>
    {
        public Result(bool isSuccess, string errorMessage) : base(isSuccess, errorMessage)
        {
        }
    }
    public class Result<T> where T : class
    {
        private Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None ||
                !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        public Result(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public T? Value { get; } = null;

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public string? Message { get; } = null;
        public Error? Error { get; } = null;

        public static Result<T> Success() => new(true, Error.None);

        public static Result<T> Failure(Error error) => new(false, error);
    }

    public sealed record Error(string Code, string Description)
    {
        public static readonly Error None = new(string.Empty, string.Empty);
    }
}
