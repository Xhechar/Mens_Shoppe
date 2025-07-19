using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository {

  private readonly DataContext dataContext;

  public UserRepository (DataContext dataContext) {
    this.dataContext = dataContext;
  }

  public bool SaveChanges() => this.dataContext.SaveChanges() == 1;

  public RepositoryResult<User> Register(CreateUserDto userDto)
  {
    User? emailExists = this.dataContext.user.FirstOrDefault(u => u.Email == userDto.Email);

    if(emailExists != null) {
      return RepositoryResponse.Failure<User>("Email provided exists.");
    }

    User? phoneExists = this.dataContext.user.FirstOrDefault(u => u.PhoneNumber == userDto.PhoneNumber);

    if(phoneExists != null) {
      return RepositoryResponse.Failure<User>("Phone number provided exists.");
    }

    User createUser = new User
    {
        UserId = Guid.NewGuid().ToString(),
        Country = userDto.Country,
        Email = userDto.Email,
        Name = userDto.Name,
        Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
        PhoneNumber = userDto.PhoneNumber
    };

    this.dataContext.user.Add(createUser);

    if(!this.SaveChanges()) {
      RepositoryResponse.Failure<User>("We are unable to register user at the moment.");
    }

    return RepositoryResponse.Success<User>("Registration successfull, please login");
  }

  public RepositoryResult<User> UpdateUser(string UserId, UpdateUserDto userDto) 
  {
    if (string.IsNullOrWhiteSpace(UserId)) return RepositoryResponse.Failure<User>("Registration Id is invalid, login.");

    User? user = dataContext.user.FirstOrDefault(u => u.UserId == UserId);

    if(user == null) return RepositoryResponse.Failure<User>("Your identity is not found, register.");

    User updateUser = new User
    {
        UserId = UserId,
        Country = userDto.Country,
        Email = userDto.Email,
        Name = userDto.Name,
        PhoneNumber = userDto.PhoneNumber,
        Password = user.Password,
        UpdatedAt = DateTime.Now
    };

    this.dataContext.user.Update(updateUser);

    if (!this.SaveChanges()) return RepositoryResponse.Failure<User>("We are unable to update your profile at the moment.");

    return RepositoryResponse.Success<User>("profile update successfull.");
  }

  public RepositoryResult<User> GetUserById(string userId)
  {
    if (string.IsNullOrWhiteSpace(userId)) return RepositoryResponse.Failure<User>("User Id is invalid, login.");

    User? user = this.dataContext.user
      .Include(u => u.UserCarts)
      .Include(u => u.UserFavourites)
      .Include(u => u.UserReviews)
      .Include(u => u.UserOrders)
    .FirstOrDefault(u => u.UserId == userId);

    if (user == null) return RepositoryResponse.Failure<User>("Your identity is not found, register.");

    return RepositoryResponse.Success<User>("user successfully retrieved.", user);
  }

  public RepositoryResult<User> GetAllUsers()
  {
    var users = this.dataContext.user
      .Include(u => u.UserCarts)
      .Include(u => u.UserFavourites)
      .Include(u => u.UserReviews)
      .Include(u => u.UserOrders)
    .OrderBy(u => u.CreatedAt).ToList();

    if (users.Count == 0) return RepositoryResponse.Failure<User>("No users found.");

    return RepositoryResponse.Success<User>("users successfully retrieved.", dataList: users);
  }
}