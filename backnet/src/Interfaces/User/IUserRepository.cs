
public interface IUserRepository
{
    public RepositoryResult<User> Register(CreateUserDto userDto);
    public RepositoryResult<User> UpdateUser(string UserId, UpdateUserDto userDto);
    public RepositoryResult<User> GetUserById(string userId);
    public RepositoryResult<User> GetAllUsers();
    public bool SaveChanges();
}