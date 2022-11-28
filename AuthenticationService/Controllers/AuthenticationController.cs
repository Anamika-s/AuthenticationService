﻿using ClaimUserService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimUserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private UserDbContext _context;
        private IConfiguration _config;

        public AuthenticationController(UserDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }


        [HttpPost]
        public IActionResult Login(User user)
        {

            IActionResult response = Unauthorized();

            var obj = Authenticate(user);

            if (obj != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }
            return response;

        }

        private string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public User Authenticate(User user)
        {
            User obj = _context.Users.FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);
            return obj;

        }


    }
}
