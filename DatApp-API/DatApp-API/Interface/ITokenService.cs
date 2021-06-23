using DatApp_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatApp_API.Interface
{
    public interface ITokenService
    {
        string CreateToken(Users usr);
    }
}
