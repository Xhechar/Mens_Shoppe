
public interface IAuthRepository
{
  public RepositoryResult<object> Login(LoginDto loginDto);
  public RepositoryResult<object> ResetPassword(ForgotPasswordDto forgotPasswordDto);
  public Task<RepositoryResult<object>> GetRecoveryCode(string email);
  public RepositoryResult<object> Logout(string email);
}