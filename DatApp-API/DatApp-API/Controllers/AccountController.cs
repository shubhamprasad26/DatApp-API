using DatApp_API.Data;
using DatApp_API.DTOs;
using DatApp_API.Entities;
using DatApp_API.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DatApp_API.Controllers
{
    
    public class AccountController : BasicController
    {
        private readonly Datacontext _context;
        private readonly ITokenService _token;

        public AccountController(Datacontext context, ITokenService token)
        {
            _context = context;
            _token = token;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDTo rDto)
        {
            using var hmac = new HMACSHA512();
            var User = await _context.users.SingleOrDefaultAsync(x => x.Name.ToLower() == rDto.UserName.ToLower());
            if (User != null) { return Unauthorized("User already Exist"); }

            var usr = new Users
            {
                Name = rDto.UserName.ToLower(),
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(rDto.Password)),
                passwordSalt = hmac.Key
            };

             _context.users.Add(usr);
             await _context.SaveChangesAsync();
            return new UserDto
            {
                UserName = rDto.UserName,
                Token = _token.CreateToken(usr)
            };


        }
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDTo ldto)
        {
            Users usr = await _context.users.SingleOrDefaultAsync(x => x.Name.ToLower() == ldto.UserName.ToLower());
            if (usr == null) { return Unauthorized("Use Not Found"); }

            using var hmac = new HMACSHA512(usr.passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(ldto.Password));

            for(int i = 0; i < computedHash.Length; i++) { if (computedHash[i] != usr.passwordHash[i]) { return Unauthorized("Wrong password"); } }

            return new UserDto
            {
                UserName = ldto.UserName,
                Token = _token.CreateToken(usr)
            };
        }


    }
}
