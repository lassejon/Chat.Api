using System.ComponentModel.DataAnnotations;

namespace Chat.Application.Errors;

public class RequiredWithErrorMessageAttribute : RequiredAttribute
{
    public RequiredWithErrorMessageAttribute(string errorMessage)
    {
        ErrorMessage = $"{errorMessage} is required";
    }
}