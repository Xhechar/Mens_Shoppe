
public class RepositoryResult<T>
{
  public bool Success { get; set; }
  public string Message { get; set; }
  public string? Error { get; set; }
  public T? Data { get; set; }
  public ICollection<T>? DataList { get; set; }
  public string? Role { get; set; }
  public string? Token { get; set; }
}

public class RepositoryResponse
{
  public static RepositoryResult<T> Success<T>(string message, T data = default!, ICollection<T>? dataList = default)
  {
    return new RepositoryResult<T>
    {
      Success = true,
      Message = message,
      Data = data,
      DataList = dataList
    };
  }

  public static RepositoryResult<T> Failure<T>(string error, string message = "Request unsuccessful")
  {
    return new RepositoryResult<T>
    {
      Success = false,
      Message = message,
      Error = error
    };
  }

  public static RepositoryResult<T> Auth<T>(string message, string role, string token)
  {
    return new RepositoryResult<T>
    {
      Success = true,
      Message = message,
      Role = role,
      Token = token
    };
  }
}