using System;

namespace RailwayOrientedProgrammingInCSharpDomain.Data
{
  public interface IOption<T>
  {
    bool IsSome { get; }
    bool IsNone { get; }
    TResult Match<TResult>(Func<T, TResult> onSome, Func<TResult> onNone);
    IOption<TResult> Bind<TResult>(Func<T, IOption<TResult>> f);
    IOption<TResult> Map<TResult>(Func<T, TResult> f);
    T Or(T aDefault);
    T Always();
  }

  public class Some
  {
    public static IOption<T> Of<T>(T value) => new Some<T>(value);
  }

  public class None
  {
    public static IOption<T> Of<T>() => new None<T>();
  }

  public class Option
  {
    public static IOption<T> FromNullable<T>(T? v) where T : struct =>
      v.HasValue ? Some.Of(v.Value) : new None<T>();

    public static IOption<T> FromMaybeNull<T>(T v) where T : class =>
      v != null ? Some.Of(v) : new None<T>();
  }

  public class Some<T> : IOption<T>
  {
    public T Value { get; }

    public Some(T value)
    {
      Value = value;
    }

    public bool IsSome => true;
    public bool IsNone => false;
    public TResult Match<TResult>(Func<T, TResult> onSome, Func<TResult> _) => onSome(Value);
    public IOption<TResult> Bind<TResult>(Func<T, IOption<TResult>> f) => f(Value);
    public IOption<TResult> Map<TResult>(Func<T, TResult> f) => new Some<TResult>(f(Value));
    public T Or(T _) => Value;
    public T Always() => Value;
  }

  public class None<T> : IOption<T>
  {
    public bool IsSome => false;
    public bool IsNone => true;
    public TResult Match<TResult>(Func<T, TResult> _, Func<TResult> onNone) => onNone();
    public IOption<TResult> Bind<TResult>(Func<T, IOption<TResult>> _) => new None<TResult>();
    public IOption<TResult> Map<TResult>(Func<T, TResult> _) => new None<TResult>();
    public T Or(T aDefault) => aDefault;
    public T Always() => default;
  }
}
