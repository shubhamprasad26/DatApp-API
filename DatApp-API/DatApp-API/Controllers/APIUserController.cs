using DatApp_API.Data;
using DatApp_API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatApp_API.Controllers
{
    
    public class APIUserController : BasicController
    {
        private readonly Datacontext _context;

        public APIUserController(Datacontext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route]

        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            return await _context.user.ToListAsync();
        }
    }
}
