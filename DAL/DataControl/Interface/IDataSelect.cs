using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace DAL.DataControl.Interface
{
    public interface IDataSelect
    {
        /// <summary>
        /// 提供一个通过指定规则执行查询的功能，该功能会直接返回查询的数据。
        /// </summary>
        /// <param name="Wheres">格式化的匹配字典</param>
        /// <returns>返回查询出的所有数据</returns>
        DataSet Select(Dictionary<string, List<string>> Wheres);


        /// <summary>
        /// 提供一个通过指定规则执行查询的功能，该功能会直接返回查询的数据，返回指定的字段。
        /// </summary>
        /// <param name="Fields">指定查询出的字段</param>
        /// <param name="Wheres">格式化的匹配字典</param>
        /// <returns>返回查询出的所有数据</returns>
        DataSet Select(List<String> Fields, Dictionary<string, List<string>> Wheres);

        /// <summary>
        /// 提供一个通过指定规则执行查询的功能，该功能会直接返回查询的数据。
        /// </summary>
        /// <param name="Wheres">格式化的匹配字典</param>
        /// <returns>对应数据的数据类</returns>
        T SelectReturnObject<T>(Dictionary<string, List<string>> Wheres);

    }
}
