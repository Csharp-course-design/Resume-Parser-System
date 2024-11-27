using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace DAL.DataControl.Interface
{
    public interface IDataUpdate
    {

        /// <summary>
        /// 将指定数据ID的记录的指定字段替换为Item对象里存放的数据
        /// </summary>
        /// <param name="ID">指定的数据ID</param>
        /// <param name="Item">要替换的内容</param>
        /// <returns>影响的条数</returns>
        int Update<T>(string ID, T Item);

        /// <summary>
        /// 提供一个将所有符合Where语句的记录的指定字段替换为Item对象里存放的数据
        /// </summary>
        /// <param name="Item">要替换的内容</param>
        /// <param name="Where">限制条件</param>
        /// <returns>影响的条数</returns>
        int Update<T>(T Item, Dictionary<string, List<string>> Wheres);

    }
}
