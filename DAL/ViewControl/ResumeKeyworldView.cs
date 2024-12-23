using DAL.DataControl;
using Microsoft.Data.SqlClient;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewControl
{
    public class ResumeKeyworldView : DataBaseControl
    {
        /// <summary>
        /// 通过关键字查询
        /// </summary>
        /// <param name="KeyWords"></param>
        /// <returns>List<ResumeFile></returns>
        public List<ResumeFile> SelectView(List<string> KeyWords)
        {
            Dictionary<string, List<string>> Wheres = new Dictionary<string, List<string>>()
            {
                { "KeyworldName" ,  KeyWords}
            };
            string whereClause = BuildWhereClause(Wheres);

            string query = $"SELECT *FROM ResumeModel WHERE Id IN (SELECT ResumeId FROM ResumeKeyworldView WHERE {whereClause});";

            if (Wheres.Count == 0)
            {
                query = "SELECT * FROM ResumeModel;";
            }

            List<ResumeFile> results = new List<ResumeFile>();

            var connection = GetSqlConnection();
            {
                OpenSqlConnection();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(new ResumeFile()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Filename = reader.IsDBNull(reader.GetOrdinal("FileName")) ? string.Empty : reader.GetString(reader.GetOrdinal("FileName")),
                                Base64Data = reader.IsDBNull(reader.GetOrdinal("FileBase64")) ? string.Empty : reader.GetString(reader.GetOrdinal("FileBase64")),
                                Date = reader.IsDBNull(reader.GetOrdinal("ImportDate"))? default(DateTime): reader.GetDateTime(reader.GetOrdinal("ImportDate"))
                            });
                        }
                    }
                }
            }

            return results;

        }
        

        
    }
}
