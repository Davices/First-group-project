using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressCode.Model;

namespace ExpressCode.Services.Admin.User
{
    public class _MapConfig: Profile
    {
        public _MapConfig()
        {
            //查询
            CreateMap<UserEntity, UserGetOutput>();

            //添加
            //CreateMap<, UserEntity>();

        }
    }
}
