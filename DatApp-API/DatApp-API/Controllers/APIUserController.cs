using DatApp_API.Data;
using DatApp_API.Entities;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [HttpGet]      
        public async Task<ActionResult<IEnumerable<Users>>> GetAll()
        {
            return await _context.users.ToListAsync();
        }
    }
}
