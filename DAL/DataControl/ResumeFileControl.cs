using DAL.DataControl.Interface;
using Microsoft.Data.SqlClient;
using Models;

namespace DAL.DataControl
{
    public class ResumeFileControl : DataBaseControl, IDataInsert, IDataSelect, IDataDelete
    {
        public ResumeFileControl()
        {

        }

        public int Delete(Dictionary<string, List<string>> Wheres)
        {
            throw new NotImplementedException();
        }

        public int DeleteByID(string ID)
        {
            string query = "DELETE FROM ResumeModel WHERE Id = " + ID + ";";
            return (DBHelper.ExecuteSql(query));
        }

        public void Insert<T>(T Item)
        {
            InsertReturnID(Item);
        }

        /// <summary>
        /// 简历文件实体的插入
        /// </summary>
        /// <typeparam name="T">必须为ResumeFile类型</typeparam>
        /// <param name="Item">ResumeFile变量</param>
        /// <returns>id的string类型</returns>
        public string InsertReturnID<T>(T Item)
        {
            SqlConnection conn = GetSqlConnection();
            {
                try
                {
                    OpenSqlConnection();

                    string checkSql = "SELECT Id FROM ResumeModel WHERE FileBase64 = @FileBase64";


                    if (Item is ResumeFile resume1)
                    {
                        using (SqlCommand checkCmd = new SqlCommand(checkSql, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@FileBase64", resume1.Base64Data);

                            // 执行查询，获取已存在的关键字的 ID
                            var existingId = checkCmd.ExecuteScalar();
                            if (existingId != null)
                            {
                                // 如果找到了，直接返回已有的 Id
                                return existingId.ToString();
                            }
                        }


                    }


                    string IdReturn;
                    string sql = @"
        INSERT INTO ResumeModel
        ( FileName, FileBase64, ImportDate)
        VALUES
        ( @FileName, @FileBase64, @ImportDate);
        
        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    if (Item is ResumeFile resume)
                    {
                        // 准备 SqlCommand
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@FileName", resume.Filename);
                            cmd.Parameters.AddWithValue("@FileBase64", resume.Base64Data);
                            cmd.Parameters.AddWithValue("@ImportDate", resume.Date);
                            // 执行插入操作并获取生成的 ID
                            var insertedId = cmd.ExecuteScalar();
                            IdReturn = insertedId.ToString();
                            CloseSqlConnection();
                            return IdReturn;
                        }
                    }
                }
                catch (Exception ex) { }
            }
            return null;
        }


        public List<object> Select(Dictionary<string, List<string>> Wheres)
        {
            string whereClause = BuildWhereClause(Wheres);
            string query = $"SELECT * FROM ResumeModel WHERE {whereClause}";
            if (Wheres.Count == 0)
            {
                query = "SELECT * FROM ResumeModel;";
            }

            List<object> results = new List<object>();

            using (var connection = GetSqlConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(new
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FileName = reader.IsDBNull(reader.GetOrdinal("FileName")) ? null : reader.GetString(reader.GetOrdinal("FileName")),
                                FileBase64 = reader.IsDBNull(reader.GetOrdinal("FileBase64")) ? null : reader.GetString(reader.GetOrdinal("FileBase64")),
                                ImportDate = reader.IsDBNull(reader.GetOrdinal("ImportDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ImportDate"))
                            });
                        }
                    }
                }
            }

            return results;
        }

        public List<object> Select(List<string> Fields, Dictionary<string, List<string>> Wheres)
        {
            string fieldList = Fields != null && Fields.Count > 0 ? string.Join(", ", Fields) : "*";
            string whereClause = BuildWhereClause(Wheres);
            string query = $"SELECT {fieldList} FROM ResumeModel WHERE {whereClause}";
            if (Wheres.Count == 0) { query = "SELECT * FROM ResumeModel;"; }

            List<object> results = new List<object>();

            using (var connection = GetSqlConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var record = new Dictionary<string, object>();
                            foreach (var field in Fields)
                            {
                                record[field] = reader[field];
                            }
                            results.Add(record);
                        }
                    }
                }
            }

            return results;
        }

        public T SelectReturnObject<T>(Dictionary<string, List<string>> Wheres)
        {
            string whereClause = BuildWhereClause(Wheres);
            string query = $"SELECT TOP 1 * FROM ResumeModel WHERE {whereClause}";
            if (Wheres.Count == 0) { query = "SELECT * FROM ResumeModel;"; }
            using (var connection = GetSqlConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return (T)Activator.CreateInstance(typeof(T), new
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FileName = reader.IsDBNull(reader.GetOrdinal("FileName")) ? null : reader.GetString(reader.GetOrdinal("FileName")),
                                FileBase64 = reader.IsDBNull(reader.GetOrdinal("FileBase64")) ? null : reader.GetString(reader.GetOrdinal("FileBase64")),
                                ImportDate = reader.IsDBNull(reader.GetOrdinal("ImportDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ImportDate"))
                            });
                        }

                    }
                }
            }

            return default(T);
        }
    }
}
