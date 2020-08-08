namespace RailwayOrientedProgrammingInCSharpDomain.Results
{
  public enum ErrorType
  {
    NameCanNotBeBlank,
    EmailCanNotBeBlank,
    EmailNotValid,
    UserNotFound,
    DatabaseUpdateError,
    EmailNotSend
  }

  public static class ErrorToMessage
  {
    public static string ErrorTypesToErrorMessage(this ErrorType errorType)
    {
      switch (errorType)
      {
        case ErrorType.NameCanNotBeBlank: return "User Name Can Not Be Blank";
        case ErrorType.EmailCanNotBeBlank: return "User Email Can Not Be Blank";
        case ErrorType.EmailNotValid: return "User Email Not Valid";
        case ErrorType.UserNotFound: return "User Not Found";
        case ErrorType.DatabaseUpdateError: return "Error occured while updating entity";
        case ErrorType.EmailNotSend: return "Error occured while sending email";
        default: return "Unknown Server Error";
      }
    }
  }
}
