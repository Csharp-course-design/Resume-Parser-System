using System;
using Microsoft.Data.SqlClient;

namespace DAL.DataControl.Interface
{
    public interface IDataInsert
    {
        /// <summary>
        /// 插入指定内容
        /// </summary>
        /// <param name="Item">插入的指定表的数据对象</param>
        /// <returns></returns>
        void Insert<T>(T Item);

        /// <summary>
        /// 插入指定内容，并返回插入数据的ID
        /// </summary>
        /// <param name="Item">插入的指定表的数据对象</param>
        /// <returns>数据的ID</returns>
        string InsertReturnID<T>(T Item);
    }
}
