using Chat.Application.Results;

namespace Chat.Application.Extensions
{
    public static class ResultExtensions
    {
        public static TResult Match<TResult>(this Result result, Func<Result, TResult> onSuccess, Func<Result, TResult> onFailure)
        {
            return result.IsSuccess ? onSuccess(result) : onFailure(result);
        }
    }
}
