using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressCode.Model;

namespace ExpressCode.Services.Admin.User
{
    public class UserGetOutput:BaseEntity
    {
        public string name { get; set; }
        public string sex { get; set; }
    }
}
