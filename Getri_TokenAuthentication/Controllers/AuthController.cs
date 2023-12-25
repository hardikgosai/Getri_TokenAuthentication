using AutoMapper;
using Getri_TokenAuthentication.DTOs;
using Getri_TokenAuthentication.Models;
using Getri_TokenAuthentication.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Getri_TokenAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepository;
        private readonly IMapper mapper;
        private readonly IConfiguration iConfiguration;

        public AuthController(IAuthRepository _authRepository, IMapper _mapper, IConfiguration _iConfiguration)
        {
            authRepository = _authRepository;
            mapper = _mapper;
            iConfiguration = _iConfiguration;
        }

        [HttpPost("Register")]
        public IActionResult Register(RegisterDTO registerDto)
        {
            registerDto.Email = registerDto.Email.ToLower();

            var userToCreate = mapper.Map<TblUser>(registerDto);
            var createdUser = authRepository.Register(userToCreate);

            return StatusCode(201, new { email = createdUser.Email, fullname = createdUser.FullName });
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO loginDto)
        {
            var userFromRepo = authRepository.Login(loginDto.Email.ToLower());
            if (userFromRepo == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.UserId.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(iConfiguration.GetSection("AppSettings:Token").Value));

            //steps for creating token and adding identity
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);  //in-memory

            return Ok(new { token = tokenHandler.WriteToken(token), email = userFromRepo.Email, fullname = userFromRepo.FullName });
        }

        [HttpPost("ValidateToken")]
        public IActionResult ValidateToken(string authToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();

            SecurityToken validatedToken;
            IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
            return Ok(principal.Identity.IsAuthenticated);
        }

        private TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = true, // Because there is no expiration in the generated token
                ValidateAudience = false, // Because there is no audiance in the generated token
                ValidateIssuer = false,   // Because there is no issuer in the generated token               
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(iConfiguration.GetSection("AppSettings:Token").Value)) // The same key as the one that generate the token
            };
        }
    }
}
