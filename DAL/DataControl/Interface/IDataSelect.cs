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
        /// <param name="whereString">在SQL中 WHERE 部分需要填入的部分</param>
        /// <param name="gropeBy"></param>
        /// <param name="orderByField"></param>
        /// <param name="isAscending"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns>返回查询出的所有数据</returns>
        DataSet Select(String whereString, string gropeBy, string orderByField, bool isAscending, int? pageSize, int? pageNumber);


        /// <summary>
        /// 提供一个通过指定规则执行查询的功能，该功能会直接返回查询的数据，返回指定的字段。
        /// </summary>
        /// <param name="Fields">指定查询出的字段</param>
        /// <param name="whereString">在SQL中 WHERE 部分需要填入的部分</param>
        /// <param name="gropeBy"></param>
        /// <param name="orderByField"></param>
        /// <param name="isAscending"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns>返回查询出的所有数据</returns>
        DataSet Select(List<String> Fields, String whereString, string gropeBy, string orderByField, bool isAscending, int? pageSize, int? pageNumber);

        /// <summary>
        /// 提供一个通过指定规则执行查询的功能，该功能会直接返回查询的数据。
        /// </summary>
        /// <param name="whereString">在SQL中 WHERE 部分需要填入的部分</param>
        /// <returns>对应数据的数据类</returns>
        Object SelectReturnObject(String whereString);



        ///// <summary>
        ///// 提供一个通过指定规则查询的功能，该功能会将查询结果保存到数据库中，以供使用流式处理功能对数据进行进一步加工。
        ///// </summary>
        ///// <param name="whereString">在SQL中 WHERE 部分需要填入的部分</param>
        ///// <param name="ReturnTable">用于指定保存在数据库中的数据的表名</param>
        //void Select(String whereString, String ReturnTable);
    }
}
