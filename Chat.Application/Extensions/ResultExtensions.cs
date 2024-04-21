using Chat.Application.Results;

namespace Chat.Application.Extensions
{
    public static class ResultExtensions
    {
        public static TReturnType Match<TReturnType>(this Result result, Func<Result, TReturnType> onSuccess, Func<Result, TReturnType> onFailure)
        {
            return result.IsSuccess ? onSuccess(result) : onFailure(result);
        }
    }
}
