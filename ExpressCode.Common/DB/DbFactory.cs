using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressCode.IRepository;

namespace ExpressCode.Common
{
    public class DBFactory
    {
        //选择数据库
        public static string ConnectDB;
        //通用BaseRepository接口
        IBaseRepository _IBaseRepository = null;
        
        public IBaseRepository GetBaseRepository()
        {
            switch (ConnectDB)
            {
                case "SqlServer":
                    _IBaseRepository = new SqlServerDapperHelper();
                    
                    break;
                case "MySql":
                    _IBaseRepository = new MySqlDapperHelper();
                    break;
                default:
                    break;
            }
            return _IBaseRepository;
        }
    }
}
