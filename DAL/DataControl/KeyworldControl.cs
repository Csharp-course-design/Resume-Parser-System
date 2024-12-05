using DAL.DataControl.Interface;
using Microsoft.Data.SqlClient;
using Models;

namespace DAL.DataControl
{
    internal class KeyworldControl : DataBaseControl, IDataInsert
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
                    if (Item is KeyWord item)
                    {
                        // 1. 检查关键字是否已经存在
                        string checkSql = "SELECT World FROM ResumKeyworldModel WHERE World = @Keyword";

                        using (SqlCommand checkCmd = new SqlCommand(checkSql, conn))
                        {
                            // 设置查询参数
                            checkCmd.Parameters.AddWithValue("@Keyword", item.Word);

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
                            insertCmd.Parameters.AddWithValue("@Keyword", item.Word);

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
    }
}
