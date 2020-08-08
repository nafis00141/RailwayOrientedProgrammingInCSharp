namespace RailwayOrientedProgrammingInCSharpDomain.Models
{
  public class GenericResponse
  {
    public GenericResponse(string message)
    {
      Message = message;
    }

    public string Message { get; set; }

    public static GenericResponse CreateGenericResponse(string message) =>
      new GenericResponse(message);
  }
}
