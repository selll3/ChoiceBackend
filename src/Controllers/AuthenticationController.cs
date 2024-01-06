using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Choice_Ym.Abstractions;
using Choice_Ym.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Choice_Ym.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthenticateController(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var user = await _userRepository.GetAsync(u => u.Email == userLoginDto.Email && u.Password == userLoginDto.Password);
            if (user is not null)
            {
                var authClaims = new List<Claim> //Name
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                var token = _tokenService.GetToken(authClaims);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    isSuccess = true,
                    email = user.Email,
                    username = user.Username,
                    age = user.Age
                });
            }
            return Ok(new
            {
                token = string.Empty,
                isSuccess = false
            });
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            bool response = await _userRepository.AddAsync(new() { Age = userRegisterDto.Age, Email = userRegisterDto.Email, Password = userRegisterDto.Password, Username = userRegisterDto.Username });
            if (response)
                response = await _userRepository.SaveChangesAsync();
            return Ok(response);
        }
    }
}