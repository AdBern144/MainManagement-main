using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManagement.DataContracts;
using StockManagement.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;

namespace StockManagement.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public ActionResult<string> Login([FromBody] LoginDto model)
    {
        try
        {
            var result = _userService.Login(model.Email, model.Password);

            if (result != null)
            {
                var jwt = _userService.GetToken(result);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwt)
                });
            }

            return Unauthorized();
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
            throw;
        }

    }

    [HttpPost("register")]
    public async Task<ActionResult<string>> RegisterAsync([FromBody] RegisterDto model)
    {
        try
        {
            var result = await _userService.RegisterAsync(model.Email, model.Password);

            if (result != null)
            {
                var jwt = _userService.GetToken(result);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwt)
                });
            }
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

        return NotFound();
    }
};