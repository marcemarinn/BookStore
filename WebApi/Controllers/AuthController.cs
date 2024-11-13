using Core.Dtos;
using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers;

public class AuthController : BaseApiController
{
    private readonly IConfiguration _configuration;

      private readonly IUserService _userService;

    public AuthController(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async  Task<IActionResult> Register(UserDto request)
    {
        var users = await _userService.Register(request);

        return Ok(users);
    }
    [HttpPost("login")]
    public ActionResult<User> Login(UserDto request)
    {
        //if (user.UserName != request.UserName)
        //   return BadRequest("User not found.");
        //if (!BCrypt.Net.BCrypt.Verify(request.PassWord, user.PassWordHash))
        //    return BadRequest("bad passwords");

        //string token = CreateToken(user);
            
        return Ok();
    }


   


}
