using RailwayOrientedProgrammingInCSharpDomain.Data;
using RailwayOrientedProgrammingInCSharpDomain.Models;
using RailwayOrientedProgrammingInCSharpDomain.Queries;
using RailwayOrientedProgrammingInCSharpDomain.Results;

namespace RailwayOrientedProgrammingInCSharpDomain.Services
{
  public interface IUserService
  {
    IResult<GenericResponse> UpdateUser(UpdateUserDto updateUserDto);
  }

  public class UserService : ExceptionHelper, IUserService
  {
    private readonly IUserQuery _userQuery;
    private readonly IEmailService _emailService;

    public UserService(IUserQuery userQuery, IEmailService emailService)
    {
      _userQuery = userQuery;
      _emailService = emailService;
    }

    public IResult<GenericResponse> UpdateUser(UpdateUserDto updateUserDto) =>
      ValidateUserData(updateUserDto)
        .Bind(_ => UpdateUserDataInDB(updateUserDto))
        .Bind(_ => _emailService.SendNotificationEmail(updateUserDto.Email))
        .Bind(_ => Success.Of(GenericResponse.CreateGenericResponse("User Updated Successfully")));

    private IResult<Unit> ValidateUserData(UpdateUserDto updateUserDto)
    {
      if (string.IsNullOrEmpty(updateUserDto.Name))
        return Error.Of<Unit>(ErrorType.NameCanNotBeBlank);

      if (string.IsNullOrEmpty(updateUserDto.Email))
        return Error.Of<Unit>(ErrorType.EmailCanNotBeBlank);

      if (!IsValidEmail(updateUserDto.Email))
        return Error.Of<Unit>(ErrorType.EmailNotValid);

      return Result.Ok;
    }

    private IResult<Unit> UpdateUserDataInDB(UpdateUserDto updateUserDto) =>
      GetUserBasedOnId(updateUserDto.UserId)
      .Map(user =>
      {
        user.Name = updateUserDto.Name;
        user.Email = updateUserDto.Email;
        return user;
      })
      .Map(_userQuery.UpdateUser)
      .Bind(userUpdated =>
        userUpdated
        ? Result.Ok
        : Error.Of<Unit>(ErrorType.DatabaseUpdateError)
      );

    private IResult<User> GetUserBasedOnId(long userId) =>
      _userQuery
      .GetUser(userId)
      .Then(Option.FromMaybeNull)
      .Then(userOption =>
        userOption.IsSome
        ? Success.Of(userOption.Always())
        : Error.Of<User>(ErrorType.UserNotFound)
      );

    private bool IsValidEmail(string email) =>
      DoUnitOfWork(() => new System.Net.Mail.MailAddress(email).Address == email);

  }
}
