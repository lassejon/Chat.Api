using Chat.Application.Results;

namespace Chat.Application.Extensions
{
    public static class ResultExtensions
    {
        public static T Match<T>(this Result result, Func<Result, T> onSuccess, Func<Result, T> onFailure)
        {
            return result.IsSuccess ? onSuccess(result) : onFailure(result);
        }
    }
}
