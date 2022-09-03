using JwtImplementation.Interfaces;
using JwtImplementation.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtImplementation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IUserService _userService;
        public readonly ITokenGenerator _tokenGenerator;

        public AuthController(IUserService userService,ITokenGenerator tg)
        {
            _userService = userService;
            _tokenGenerator = tg;
        }

        [Route("/Authenticate")]
        [HttpPost]
        public ActionResult Login(UserLoginViewModel loginuser)
        {
            User user = _userService.Login(loginuser);
            String token = _tokenGenerator.GenerateToken(user.Id, user.Email, user.Role);
            return Ok(token);
        }

        [HttpGet]
        [Authorize(Roles ="user")]
        [Route("/all")]
        public ActionResult All()
        {
            List<User> users = _userService.GetAllUsers();
            return Ok(users);
        }

    }
}
