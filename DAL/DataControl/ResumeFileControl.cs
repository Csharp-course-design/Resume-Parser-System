using DAL.DataControl.Interface;
using Microsoft.Data.SqlClient;
using Models;
using Models.ResumeInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataControl
{
    public class ResumeFileControl : DataBaseControl, IDataInsert, IDataSelect
    {
        public ResumeFileControl()
        {

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

        private string BuildWhereClause(Dictionary<string, List<string>> wheres)
        {
            if (wheres == null || wheres.Count == 0)
                return "1=1";

            var clauses = new List<string>();
            foreach (var where in wheres)
            {
                if (where.Value.Count == 1)
                {
                    clauses.Add($"{where.Key} = '{where.Value[0]}'");
                }
                else
                {
                    var inClause = string.Join(", ", where.Value.ConvertAll(val => $"'{val}'"));
                    clauses.Add($"{where.Key} IN ({inClause})");
                }
            }

            return string.Join(" AND ", clauses);
        }

        public List<object> Select(Dictionary<string, List<string>> Wheres)
        {
            string whereClause = BuildWhereClause(Wheres);
            string query = $"SELECT * FROM ResumeModel WHERE {whereClause}";
            if (Wheres.Count == 0) {
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
