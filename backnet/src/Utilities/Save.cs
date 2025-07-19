
public static class Save 
{
  public static bool SaveChanges(this DataContext dataContext)
  {
    try
    {
      return dataContext.SaveChanges() > 0;
    }
    catch (Exception)
    {
      return false;
    }
  }
}