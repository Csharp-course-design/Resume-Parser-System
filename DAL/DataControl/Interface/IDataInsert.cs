using System;
using Microsoft.Data.SqlClient;

namespace DAL.DataControl.Interface
{
    public interface IDataInsert
    {
        /// <summary>
        /// 插入指定内容
        /// </summary>
        /// <param name="DataPairs">
        /// 需要插入的所有数据<br/>
        /// - 格式要求<br/>
        /// ----- pair.first :  指定的字段<br/>
        /// ----- pair.second : 插入行的指定字段的值
        /// </param>
        /// <returns>影响的行数</returns>
        /// 
        //int Inseart(params KeyValuePair<String, String>[] DataPairs);  仅提供按对象传入数据的方法


        /// <summary>
        /// 插入指定内容
        /// </summary>
        /// <param name="Item">插入的指定表的数据对象</param>
        /// <returns></returns>
        void Insert(SqlTransaction sqlTransaction, Object Item);

        /// <summary>
        /// 插入指定内容，并返回插入数据的ID
        /// </summary>
        /// <param name="Item">插入的指定表的数据对象</param>
        /// <returns>数据的ID</returns>
        string InsertReturnID(SqlTransaction sqlTransaction, Object Item);
    }
}
