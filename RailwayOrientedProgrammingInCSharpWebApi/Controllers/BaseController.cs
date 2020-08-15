using Microsoft.AspNetCore.Mvc;
using RailwayOrientedProgrammingInCSharpDomain.Data;
using RailwayOrientedProgrammingInCSharpDomain.Models;
using RailwayOrientedProgrammingInCSharpDomain.Results;

namespace RailwayOrientedProgrammingInCSharp.Controllers
{
  public class BaseController : ControllerBase
  {
    protected IActionResult CustomBadRequest(ErrorType errorType) =>
      errorType
      .ToErrorString()
      .Then(GenericResponse.CreateGenericResponse)
      .Then(BadRequest);
  }
}