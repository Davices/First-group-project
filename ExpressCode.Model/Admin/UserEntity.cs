using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressCode.Model
{
    public class UserEntity:BaseEntity
    {
        public string name { get; set; }
        public string sex { get; set; }
        public string age { get; set; }

        public string MyProperty { get; set; }
    }
}
