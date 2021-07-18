using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressCode.IRepository;
using ExpressCode.Model;

namespace ExpressCode.Services.Admin.User
{
    public class UserService : BaseService,IUserService
    {
        public IUserRepository Repository { get; set; }
        public UserGetOutput GetUser()
        {
            var da = Repository.test();
            var entityDto = Mapper.Map<UserGetOutput>(da);
            return entityDto;
        }
    }
}
