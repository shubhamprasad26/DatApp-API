using DatApp_API.Data;
using DatApp_API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public AccountController(Datacontext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<Users>> Register(string userName, string password)
        {
            using var hmac = new HMACSHA512();

            var usr = new Users
            {
                Name = userName,
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                passwordSalt = hmac.Key
            };

             _context.users.Add(usr);
             await _context.SaveChangesAsync();
            return usr;


        }
    }
}
