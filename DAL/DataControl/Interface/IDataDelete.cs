using System;
using Microsoft.Data.SqlClient;

namespace DAL.DataControl.Interface
{
    public interface IDataDelete
    {
        /// <summary>
        /// 删除指定数据ID对应的数据行
        /// </summary>
        /// <param name="ID">指定的ID</param>
        /// <returns>影响的条数</returns>
        int DeleteByID(string ID);

        /// <summary>
        /// 删除符合Where语句条件的数据项
        /// </summary>
        /// <param name="Where">限制条件</param>
        /// <returns>影响的条数</returns>
        int Delete(Dictionary<string,List<string>> Wheres);

    }
}
