using FakeItEasy;
using RailwayOrientedProgrammingInCSharpDomain.Models;
using RailwayOrientedProgrammingInCSharpDomain.Queries;
using RailwayOrientedProgrammingInCSharpDomain.Results;
using RailwayOrientedProgrammingInCSharpDomain.Services;
using Xunit;

namespace RailwayOrientedProgrammingInCSharpSpecs.ServiceSpecs
{
  public class UserServiceSpecs
  {
    private IEmailService _emailService;
    private IUserQuery _userQuery;
    private IUserService _userService;

    [Fact]
    public void it_should_return_success_message_for_proper_data_and_no_exception()
    {
      _emailService = new EmailService();
      _userQuery = new UserQuery();
      _userService = new UserService(_userQuery, _emailService);
      var result = _userService.UpdateUser(new UpdateUserDto { UserId = 1, Name = "New", Email = "new@example.com" }).Match(x=> x.Message, x=> x.ErrorTypesToErrorMessage());
      Assert.Equal("User Updated Successfully", result);
    }

    [Fact]
    public void it_should_return_error_message_user_not_found()
    {
      _emailService = new EmailService();
      _userQuery = new UserQuery();
      _userService = new UserService(_userQuery, _emailService);
      var result = _userService.UpdateUser(new UpdateUserDto { UserId = 4, Name = "New", Email = "new@example.com" }).Match(x => x.Message, x => x.ErrorTypesToErrorMessage());
      Assert.Equal(ErrorType.UserNotFound.ErrorTypesToErrorMessage(), result);
    }

    [Fact]
    public void it_should_return_error_message_when_name_is_empty()
    {
      _emailService = new EmailService();
      _userQuery = new UserQuery();
      _userService = new UserService(_userQuery, _emailService);
      var result = _userService.UpdateUser(new UpdateUserDto { UserId = 1, Name = "", Email = "new@example.com" }).Match(x => x.Message, x => x.ErrorTypesToErrorMessage());
      Assert.Equal(ErrorType.NameCanNotBeBlank.ErrorTypesToErrorMessage(), result);
    }

    [Fact]
    public void it_should_return_error_message_when_email_is_empty()
    {
      _emailService = new EmailService();
      _userQuery = new UserQuery();
      _userService = new UserService(_userQuery, _emailService);
      var result = _userService.UpdateUser(new UpdateUserDto { UserId = 1, Name = "Nafi", Email = "" }).Match(x => x.Message, x => x.ErrorTypesToErrorMessage());
      Assert.Equal(ErrorType.EmailCanNotBeBlank.ErrorTypesToErrorMessage(), result);
    }

    [Fact]
    public void it_should_return_error_message_when_email_not_valid()
    {
      _emailService = new EmailService();
      _userQuery = new UserQuery();
      _userService = new UserService(_userQuery, _emailService);
      var result = _userService.UpdateUser(new UpdateUserDto { UserId = 1, Name = "Nafi", Email = "nafisislam" }).Match(x => x.Message, x => x.ErrorTypesToErrorMessage());
      Assert.Equal(ErrorType.EmailNotValid.ErrorTypesToErrorMessage(), result);
    }

    [Fact]
    public void it_should_return_error_message_when_database_update_error()
    {
      _emailService = new EmailService();
      _userQuery = A.Fake<IUserQuery>();
      A.CallTo(() => _userQuery.GetUser(1)).Returns(new User() { Id = 1, Name = "Nafis Islam", Email = "nafis@example.com", Password = "12345" });
      A.CallTo(() => _userQuery.UpdateUser(A<User>.Ignored)).Returns(false);
      _userService = new UserService(_userQuery, _emailService);
      var result = _userService.UpdateUser(new UpdateUserDto { UserId = 1, Name = "Nafi", Email = "nafis@example.com" }).Match(x => x.Message, x => x.ErrorTypesToErrorMessage());
      Assert.Equal(ErrorType.DatabaseUpdateError.ErrorTypesToErrorMessage(), result);
    }

    [Fact]
    public void it_should_return_error_message_when_email_send_error()
    {
      _emailService = A.Fake<IEmailService>();
      A.CallTo(() => _emailService.SendNotificationEmail("nafis@example.com")).Returns(Error.Of<Unit>(ErrorType.EmailNotSend));

      _userQuery = new UserQuery();
     
      _userService = new UserService(_userQuery, _emailService);
      var result = _userService.UpdateUser(new UpdateUserDto { UserId = 1, Name = "Nafi", Email = "nafis@example.com" }).Match(x => x.Message, x => x.ErrorTypesToErrorMessage());
      Assert.Equal(ErrorType.EmailNotSend.ErrorTypesToErrorMessage(), result);
    }
  }
}
