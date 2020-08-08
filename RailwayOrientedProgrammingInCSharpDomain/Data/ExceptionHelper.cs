using System;

namespace RailwayOrientedProgrammingInCSharpDomain.Data
{
  public class ExceptionHelper
  {
    protected bool DoUnitOfWork(Action f)
    {
      try
      {
        f();
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    protected bool DoUnitOfWork(Func<bool> f)
    {
      try
      {
        return f();
      }
      catch (Exception)
      {
        return false;
      }
    }
  }
}
