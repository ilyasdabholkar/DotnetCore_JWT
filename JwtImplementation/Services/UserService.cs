using JwtImplementation.Context;
using JwtImplementation.Interfaces;
using JwtImplementation.Models.User;

namespace JwtImplementation.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User Login(UserLoginViewModel loginUser)
        {
            User user = _dbContext.Users.ToList().FirstOrDefault(u=>u.Email==loginUser.Email && u.Password==loginUser.Password);
            if(user == null){
                throw new Exception();
            }else
            {
                return user;
            }
        }


        public List<User> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }
    }
}
