using DAL.DataControl.Interface;
using Microsoft.Data.SqlClient;
using Models;

namespace DAL.DataControl
{
    internal class ResumeFileControl : DataBaseControl, IDataInsert
    {
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
                            cmd.Parameters.AddWithValue("@Name", resume.Filename);
                            cmd.Parameters.AddWithValue("@Age", resume.Base64Data);
                            cmd.Parameters.AddWithValue("@Phone", resume.Date);
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
    }
}
