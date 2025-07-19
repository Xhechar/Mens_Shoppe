
using Microsoft.EntityFrameworkCore;

public class AuthRepository : IAuthRepository
{
  private readonly DataContext dataContext;
  private readonly ITokenService tokenService;
  private readonly MailService mailService;

  public AuthRepository(DataContext dataContext, ITokenService tokenService, MailService mailService)
  {
    this.dataContext = dataContext;
    this.tokenService = tokenService;
    this.mailService = mailService;
  }

  public RepositoryResult<object> Login(LoginDto loginDto)
  {
    
    User? user = this.dataContext.user
      .FirstOrDefault(u => u.Email == loginDto.Email);

    if (user == null)
    {
      return RepositoryResponse.Failure<object>("User not found.");
    }

    bool passwordMatch = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);

    if (!passwordMatch)
    {
      return RepositoryResponse.Failure<object>("Invalid password.");
    }

    TokenDetails tokenDetails = new TokenDetails
    {
      UserId = user.UserId,
      Email = user.Email,
      Role = user.Role
    };

    string token = tokenService.GenerateToken(tokenDetails);

    return RepositoryResponse.Auth<object>("Login successful", user.Role, token);
  }

  public RepositoryResult<object> ResetPassword(ForgotPasswordDto forgotPasswordDto)
  {
    Recovery? recovery = this.dataContext.recovery
      .Include(r => r.RecoveredUser)
      .FirstOrDefault(u => u.Email == forgotPasswordDto.Email);

    if (recovery == null)
    {
      return RepositoryResponse.Failure<object>("User not found.");
    }

    if (recovery.CreatedAt.AddHours(1) < DateTime.UtcNow)
    {
      return RepositoryResponse.Failure<object>("Recovery code has expired.");
    }

   User? user = this.dataContext.user
      .FirstOrDefault(u => u.UserId == recovery.RecoveredUser.UserId);

    if (user == null)
    {
      return RepositoryResponse.Failure<object>("User not found.");
    }

    if (forgotPasswordDto.RecoveryCode != recovery.RecoveryCode)
    {
      return RepositoryResponse.Failure<object>("Invalid recovery code.");
    }

    user.Password = BCrypt.Net.BCrypt.HashPassword(forgotPasswordDto.NewPassword);

    this.dataContext.user.Update(user);
    
    if (!Save.SaveChanges(this.dataContext))
    {
      return RepositoryResponse.Failure<object>("Unable to reset password at the moment.");
    }

    return RepositoryResponse.Success<object>("Password reset successfully.");
  }

  public async Task<RepositoryResult<object>> GetRecoveryCode(string email)
  {
    User? user = this.dataContext.user
      .FirstOrDefault(u => u.Email == email);

    if (user == null)
    {
      return RepositoryResponse.Failure<object>("Email not found.");
    }
    
    Recovery recovery = new Recovery
    {
      RecoveryId = Guid.NewGuid().ToString(),
      Email = email,
      RecoveryCode = new Random().Next(100000, 999999),
      UserId = user.UserId
    };

    this.dataContext.recovery.Add(recovery);

    if (!Save.SaveChanges(this.dataContext))
    {
      return RepositoryResponse.Failure<object>("Unable to generate recovery code at the moment.");
    }
    
    await mailService.SendEmail(recovery.Email, "Recovery Code", 
      $"Hello there {recovery.RecoveredUser.Name}, we have got your request for resetting your password. Here is your recovery code.\n Your recovery code is: {recovery.RecoveryCode}");

    return RepositoryResponse.Success<object>("Email verification successful. Check your email for recovery code.");
  }

  public RepositoryResult<object> Logout(string email)
  {
    // Implementation for user logout
    throw new NotImplementedException();
  }
}