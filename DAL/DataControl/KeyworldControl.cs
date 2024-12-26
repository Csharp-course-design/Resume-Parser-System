using DAL.DataControl.Interface;
using Microsoft.Data.SqlClient;

namespace DAL.DataControl
{
    public class KeyworldControl : DataBaseControl, IDataInsert
    {
        public void Insert<T>(T Item)
        {
            InsertReturnID(Item);
        }

        public string InsertReturnID<T>(T Item)
        {
            SqlConnection conn = GetSqlConnection();
            {
                try
                {
                    OpenSqlConnection();
                    if (Item is string item)
                    {
                        // 1. 检查关键字是否已经存在
                        string checkSql = "SELECT Id FROM ResumKeyworldModel WHERE World = @Keyword";

                        using (SqlCommand checkCmd = new SqlCommand(checkSql, conn))
                        {
                            // 设置查询参数
                            checkCmd.Parameters.AddWithValue("@Keyword", item);

                            // 执行查询，获取已存在的关键字的 ID
                            var existingId = checkCmd.ExecuteScalar();
                            if (existingId != null)
                            {
                                // 如果找到了，直接返回已有的 Id
                                return existingId.ToString();
                            }
                        }

                        // 2. 如果关键字不存在，执行插入操作
                        string insertSql = "INSERT INTO ResumKeyworldModel (World) OUTPUT INSERTED.Id VALUES (@Keyword)";

                        using (SqlCommand insertCmd = new SqlCommand(insertSql, conn))
                        {
                            // 设置插入参数
                            insertCmd.Parameters.AddWithValue("@Keyword", item);

                            // 执行插入并返回新插入记录的 ID
                            int newId = (int)insertCmd.ExecuteScalar();

                            // 返回新插入的关键字的 Id
                            return newId.ToString();
                        }
                        CloseSqlConnection();
                    }


                }
                catch { }
                return null;
            }
        }
        /// <summary>
        /// 查出所有的
        /// </summary>
        /// <returns></returns>
        public List<string> Select()
        {
            //string whereClause = BuildWhereClause(Wheres);
            string query = $"SELECT World FROM ResumKeyworldModel;";

            List<string> results = new List<string>();

            var connection = GetSqlConnection();
            {
                OpenSqlConnection();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(

                                reader.GetString(0)
                             //   Id = reader.GetInt32(reader.GetOrdinal("Id")),
                             //   FileName = reader.IsDBNull(reader.GetOrdinal("FileName")) ? null : reader.GetString(reader.GetOrdinal("FileName")),
                             //   FileBase64 = reader.IsDBNull(reader.GetOrdinal("FileBase64")) ? null : reader.GetString(reader.GetOrdinal("FileBase64")),
                             //   ImportDate = reader.IsDBNull(reader.GetOrdinal("ImportDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ImportDate"))
                             );
                        }
                    }
                }
            }

            return results;
        }



    }
}
