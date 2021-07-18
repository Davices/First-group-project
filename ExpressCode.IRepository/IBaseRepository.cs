using System;
using System.Collections.Generic;

namespace ExpressCode.IRepository
{
    //通用接口
    public interface IBaseRepository
    {
        /// <summary>
        /// 增删改
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        int Execute(string sql, object param = null);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        List<T> Query<T>(string sql, object param = null) where T : class, new();
    }
}
