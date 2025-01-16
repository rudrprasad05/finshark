using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Account;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("/api/comment")]
    [ApiController]
    public class AccountController : BaseController
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("login")] 
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = await _userManager.FindByNameAsync(loginDTO.Username);
                if (user == null)
                {
                    return Unauthorized("Invalud username");
                }
                var result = await _signInManager.CheckPasswordSignInAsync (user, loginDTO.Password, false);
                if(!result.Succeeded)
                {
                    return Unauthorized("Username or password is incorrect");
                }
                
                return Ok(
                    new NewUserDTO
                    {
                        Username = user.UserName,
                        Email = user.Email,
                        Token = _tokenService.CreateToken(user)
                    }
                );

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
         
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = new AppUser
                {
                    UserName = registerDTO.Username,
                    Email = registerDTO.Email
                };
                var result = await _userManager.CreateAsync(user, registerDTO.Password);
                if (result.Succeeded)
                {
                    var role = await _userManager.AddToRoleAsync(user, "User");
                    if (role.Succeeded)
                    {
                        return Ok(
                            new NewUserDTO
                            {
                                Username = user.UserName,
                                Email = user.Email,
                                Token = _tokenService.CreateToken(user) 
                            }
                        );
                    }
                    else
                    {
                        return BadRequest(role.Errors);
                    }
                }
                return BadRequest(result.Errors);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        
    }
}