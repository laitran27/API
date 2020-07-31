using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Models
{
    public class Roles
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool isCoreRole { get; set; }
        public string Permissions { get; set; }
    }
 
}
