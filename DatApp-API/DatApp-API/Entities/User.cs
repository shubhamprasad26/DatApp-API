using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatApp_API.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string passwordHash { get; set; }
        public string passwordSalt { get; set; }
    }
}
