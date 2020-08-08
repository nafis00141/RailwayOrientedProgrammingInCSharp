using System;

namespace RailwayOrientedProgrammingInCSharpDomain.Results
{
  public interface IResult<T>
  {
    TResult Match<TResult>(
      Func<T, TResult> onSuccess,
      Func<ErrorType, TResult> onError
    );

    IResult<TResult> Map<TResult>(Func<T, TResult> f);

    IResult<TResult> Bind<TResult>(Func<T, IResult<TResult>> f);
  }

  public class Success<T> : IResult<T>
  {
    private readonly T _results;

    public Success(T results)
    {
      _results = results;
    }

    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<ErrorType, TResult> _) =>
      onSuccess(_results);

    public IResult<TResult> Map<TResult>(Func<T, TResult> f) => new Success<TResult>(f(_results));

    public IResult<TResult> Bind<TResult>(Func<T, IResult<TResult>> f) => f(_results);
  }

  public class Success
  {
    public static IResult<T> Of<T>(T value) => new Success<T>(value);
  }

  public class Error<T> : IResult<T>
  {
    private readonly ErrorType _error;

    public Error(ErrorType error)
    {
      _error = error;
    }

    public TResult Match<TResult>(Func<T, TResult> _, Func<ErrorType, TResult> onError) =>
      onError(_error);

    public IResult<TResult> Map<TResult>(Func<T, TResult> _) => new Error<TResult>(_error);

    public IResult<TResult> Bind<TResult>(Func<T, IResult<TResult>> _) => new Error<TResult>(_error);
  }

  public class Error
  {
    public static IResult<T> Of<T>(ErrorType error) => new Error<T>(error);
  }

  public class Unit { }

  public static class Result
  {
    public static IResult<Unit> Ok => new Success<Unit>(new Unit());
  }
}
