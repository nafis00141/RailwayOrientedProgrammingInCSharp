using RailwayOrientedProgrammingInCSharpDomain.Data;
using RailwayOrientedProgrammingInCSharpDomain.Results;

namespace RailwayOrientedProgrammingInCSharpDomain.Services
{
  public interface IEmailService
  {
    IResult<Unit> SendNotificationEmail(string email);
  }

  public class EmailService : ExceptionHelper, IEmailService
  {
    public IResult<Unit> SendNotificationEmail(string email) =>
      DoUnitOfWork(() => SendEmail(email))
      .Then(emailSent =>
        emailSent
        ? Result.Ok
        : Error.Of<Unit>(ErrorType.EmailNotSend)
      );

    private void SendEmail(string _)
    {
      // done send email
    }
  }
}
