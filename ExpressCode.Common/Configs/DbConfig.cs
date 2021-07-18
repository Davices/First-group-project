using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressCode.Common.Configs
{
    public class DbConfig
    {
        public string UseDB { get; set; }   //选择数据库
        public string MySqlConstr { get; set; }//Mysql连接字符串
        public string SqlServerConStr { get; set; }//Sqlserver连接字符串
        public string dbConnect { get; set; }
    }
}
