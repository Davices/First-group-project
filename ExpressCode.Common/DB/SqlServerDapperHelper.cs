using ExpressCode.IRepository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressCode.Common
{
    internal class SqlServerDapperHelper:IBaseRepository
    {

        /// <summary>
        /// 增删改
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int Execute(string sql,object param=null)
        {
            using (IDbConnection conn=new SqlConnection(DBConfigHelper.ConnectSqlServer))
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
            using (IDbConnection conn=new SqlConnection(DBConfigHelper.ConnectSqlServer))
            {
                conn.Open();
                List<T> ts = conn.Query<T>(sql,param).ToList();
                return ts;
            }
        }

        /// <summary>
        /// dapper通用的分页方法
        /// </summary>
        /// <typeparam name="T">泛型集合实体类</typeparam>
        /// <param name="conn">数据库连接池连接对象</param>
        /// <param name="files">列</param>
        /// <param name="tableName">表</param>
        /// <param name="where">条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前页显示条数</param>
        /// <param name="total">结果集条数</param>
        /// <returns></returns>
        public  IEnumerable<T> GetPageList<T>(IDbConnection conn, string files, string tableName, string where, string orderby, int pageIndex, int pageSize, out int total) where T : class, new()
        {

            int skip = 1;
            if (pageIndex > 0)
            {
                skip = (pageIndex - 1) * pageSize + 1;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT COUNT(1) FROM {0} where {1};", tableName, where);
            sb.AppendFormat(@"SELECT  {0}
                                FROM(SELECT ROW_NUMBER() OVER(ORDER BY {3}) AS RowNum,{0}
                                          FROM  {1}
                                          WHERE {2}
                                        ) AS result
                                WHERE  RowNum >= {4}   AND RowNum <= {5}
                                ORDER BY {3}", files, tableName, where, orderby, skip, pageIndex * pageSize);
            using (var reader = conn.QueryMultiple(sb.ToString()))
            {
                total = reader.ReadFirst<int>();
                return reader.Read<T>();
            }
        }
    }
}
