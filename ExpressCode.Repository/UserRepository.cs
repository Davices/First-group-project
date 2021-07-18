using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressCode.IRepository;
using ExpressCode.Model;

namespace ExpressCode.Repository
{
    public class UserRepository : IUserRepository
    {

        public UserEntity test()
        {
            UserEntity user = new UserEntity();
            user.age = "12";
            user.name = "张三";
            user.sex = "男";
            return user ;
        }
    }
}
