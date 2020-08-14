# RailwayOrientedProgrammingInCSharp
Railway Oriented Programming My Implementation In CSharp


![SimpleUseCase](https://github.com/nafis00141/RailwayOrientedProgrammingInCSharp/blob/master/RailwayOrientedProgrammingInCSharpDomain/SimpleUseCase/SimpleUseCase.png)

Error handling using ROP

![alt text](https://github.com/nafis00141/RailwayOrientedProgrammingInCSharp/blob/master/RailwayOrientedProgrammingInCSharpDomain/SimpleUseCase/Recipe_Function_ErrorTrack.png)

## Implementation of Either Monad using Interface

```C#
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

  public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<ErrorType, TResult> _) => onSuccess(_results);

  public IResult<TResult> Map<TResult>(Func<T, TResult> f) => new Success<TResult>(f(_results));

  public IResult<TResult> Bind<TResult>(Func<T, IResult<TResult>> f) => f(_results);
}

public class Error<T> : IResult<T>
{
  private readonly ErrorType _error;

  public Error(ErrorType error)
  {
    _error = error;
  }

  public TResult Match<TResult>(Func<T, TResult> _, Func<ErrorType, TResult> onError) => onError(_error);

  public IResult<TResult> Map<TResult>(Func<T, TResult> _) => new Error<TResult>(_error);

  public IResult<TResult> Bind<TResult>(Func<T, IResult<TResult>> _) => new Error<TResult>(_error);
}
```

## Keeping all our errors in an single enum
```C#
public enum ErrorType
{
  NameCanNotBeBlank,
  EmailCanNotBeBlank,
  EmailNotValid,
  UserNotFound,
  DatabaseUpdateError,
  EmailNotSend
}
```

Example taken from [Railway Oriented Programming](https://fsharpforfunandprofit.com/rop/)
