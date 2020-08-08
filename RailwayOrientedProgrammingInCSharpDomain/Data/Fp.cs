using System;

namespace RailwayOrientedProgrammingInCSharpDomain.Data
{
  public static class Fp
  {
    public static TResult Then<TValue, TResult>(this TValue value, Func<TValue, TResult> f) => f(value);

    public static void Then<TValue>(this TValue value, Action<TValue> f) => f(value);
  }
}
