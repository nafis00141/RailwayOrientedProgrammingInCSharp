using Microsoft.AspNetCore.Mvc;
using RailwayOrientedProgrammingInCSharpDomain.Models;
using RailwayOrientedProgrammingInCSharpDomain.Services;

namespace RailwayOrientedProgrammingInCSharp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : BaseController
  {
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
      _userService = userService;
    }

    [HttpPost("update")]
    public IActionResult Update(UpdateUserDto updateUserDto) =>
      _userService.UpdateUser(updateUserDto)
      .Match(
        Ok,
        CustomBadRequest
      );
  }
}