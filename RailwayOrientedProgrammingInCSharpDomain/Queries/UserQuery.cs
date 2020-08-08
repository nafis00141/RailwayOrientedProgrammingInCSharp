using RailwayOrientedProgrammingInCSharpDomain.Data;
using RailwayOrientedProgrammingInCSharpDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RailwayOrientedProgrammingInCSharpDomain.Queries
{
  public interface IUserQuery
  {
    User GetUser(long userId);
    bool UpdateUser(User user);
  }

  public class UserQuery : ExceptionHelper, IUserQuery
  {
    private IEnumerable<User> _users;

    public UserQuery()
    {
      // mocking a database
      _users = new User[]
      {
        new User() { Id = 1, Name = "Nafis Islam", Email = "nafis@example.com", Password = "12345"},
        new User() { Id = 2, Name = "Sajid Ahmed", Email = "sajid@example.com", Password = "12345"},
        new User() { Id = 3, Name = "Redone Ahmed", Email = "red1@example.com", Password = "12345"}
      };
    }

    public User GetUser(long userId) =>
      _users.FirstOrDefault(u => u.Id == userId);

    public bool UpdateUser(User user) =>
      DoUnitOfWork(() => UpdateUserData(user));

    private void UpdateUserData(User data)
    {
      // mock update db
      var user = _users.First(u => u.Id == data.Id);
      user.Name = data.Name;
      user.Email = data.Email;
    }
  }
}
