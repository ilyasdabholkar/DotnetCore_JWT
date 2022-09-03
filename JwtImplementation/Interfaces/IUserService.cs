using JwtImplementation.Models.User;

namespace JwtImplementation.Interfaces
{
    public interface IUserService
    {
        public User Login(UserLoginViewModel loginUser);
        public List<User> GetAllUsers();
    }
}
