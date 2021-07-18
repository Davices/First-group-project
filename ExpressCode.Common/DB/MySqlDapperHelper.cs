using ExpressCode.IRepository;
using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressCode.Common
{
    internal class MySqlDapperHelper:IBaseRepository
    {
        /// <summary>
        /// 增删改
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int Execute(string sql,object param=null)
        {
            using (IDbConnection conn=new MySqlConnection(DBConfigHelper.ConnectMySql))
            {
                return conn.Execute(sql,param);
            }
        }
        
      
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public  List<T> Query<T>(string sql,object param=null) where T : class, new()
        {
            using (IDbConnection conn=new MySqlConnection(DBConfigHelper.ConnectMySql))
            {
                conn.Open();
                List<T> ts = conn.Query<T>(sql,param).ToList();
                return ts;
            }
        }

        
    }
}
