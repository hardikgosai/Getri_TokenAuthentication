using AutoMapper;
using Getri_TokenAuthentication.DTOs;
using Getri_TokenAuthentication.Models;
using Getri_TokenAuthentication.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
