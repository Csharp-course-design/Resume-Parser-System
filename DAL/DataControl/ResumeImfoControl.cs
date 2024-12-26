using DAL.DataControl.Interface;
using Microsoft.Data.SqlClient;
using Models.ResumeInfo;
using Models.ResumeInfo.Apart;
using System.Data;

namespace DAL.DataControl
{
    internal class ResumeInfoControl : DataBaseControl, IDataInsert
    {
        public void Insert<T>(T Item)
        {
            InsertReturnID(Item);
        }
        /// <summary>
        /// 简历信息，技能，工作经验的插入。
        /// </summary>
        /// <typeparam name="T">ResumeInfo类型</typeparam>
        /// <param name="Item">ResumeInfo类型变量</param>
        /// <returns>会返回简历信息基础表的id</returns>
        public string InsertReturnID<T>(T Item)
        {
            SqlConnection conn = GetSqlConnection();
            {
                try
                {
                    OpenSqlConnection();

                    //Item = (ResumeInfo)Item ;
                    // 构造 SQL 语句，假设 ResumeInfo 表名为 ResumeInfoTable
                    //对简历信息表的插入
                    string IdReturn;
                    string sql = @"
        INSERT INTO ResumeInfo
        ( Name, Age, Phone, SchoolName, SchoolType, Degree, Major)
        VALUES
        ( @Name, @Age, @Phone, @SchoolName, @SchoolType, @Degree, @Major);
        
        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    // 假设 item 是 ResumeInfo 类型，将属性映射到 SQL 参数
                    if (Item is ResumeInfo resume)
                    {
                        // 准备 SqlCommand
                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@Name", resume.BaseInfo.Name);
                            cmd.Parameters.AddWithValue("@Age", resume.BaseInfo.Age);
                            cmd.Parameters.AddWithValue("@Phone", resume.BaseInfo.Phone);
                            cmd.Parameters.AddWithValue("@SchoolName", resume.EduBG.School_name);
                            cmd.Parameters.AddWithValue("@SchoolType", resume.EduBG.Schooll_type);
                            cmd.Parameters.AddWithValue("@Degree", resume.EduBG.Degree);
                            cmd.Parameters.AddWithValue("@Major", resume.EduBG.Major);
                            // 执行插入操作并获取生成的 ID
                            var insertedId = cmd.ExecuteScalar();
                            IdReturn = insertedId.ToString();
                        }

                        string sql1 = @"
        INSERT INTO WorkExperience
        (ResumeId, StartTimeYear, StartTimeMonth, EndTimeYear, StillActive, CompanyName, Department,Location,JobTitle)
        VALUES
        (@ResumeId, @StartTimeYear, @StartTimeMonth, @EndTimeYear, @StillActive, @CompanyName, @Department,@Location,@JobTitle)
        ;";
                        foreach (WorkExper item in resume.WorkExpers)
                        {
                            SqlParameter[] cmdParms = new SqlParameter[]
                            {
                                    new SqlParameter("@ResumeId", SqlDbType.Int) { Value = int.Parse(IdReturn) },
                                    new SqlParameter("@StartTimeYear", SqlDbType.NVarChar, 4) { Value = item.Start_time_year },
                                    new SqlParameter("@StartTimeMonth", SqlDbType.NVarChar, 2) { Value = item.Start_time_month },
                                    new SqlParameter("@EndTimeYear", SqlDbType.NVarChar, 4) { Value = item.End_time_year },
                                    new SqlParameter("@EndTimeMonth", SqlDbType.NVarChar, 2) { Value = item.End_time_month },
                                    new SqlParameter("@StillActive", SqlDbType.Bit) { Value = item.Still_active },
                                    new SqlParameter("@CompanyName", SqlDbType.NVarChar, 200) { Value = item.Company_name },
                                    new SqlParameter("@Department", SqlDbType.NVarChar, 100) { Value = item.Department },
                                    new SqlParameter("@Location", SqlDbType.NVarChar, 100) { Value = item.Location },
                                    new SqlParameter("@JobTitle", SqlDbType.NVarChar, 100) { Value = item.Job_title }
                            };
                            DBHelper.ExecuteSql(sql, cmdParms);

                        }
                        //对技能表插入
                        string sql2 = @"
        INSERT INTO Skills (ResumeId, Skill)
        VALUES (@ResumeId, @Skill);
        
       ;";
                        foreach (string item in resume.Skills)
                        {
                            // 准备 SqlCommand
                            SqlParameter[] cmdParms = new SqlParameter[]
                                {
                                    new SqlParameter("@ResumeId", SqlDbType.Int) { Value = int.Parse(IdReturn) },
                                    new SqlParameter("@Skill", SqlDbType.NVarChar, 4) { Value = item },
                                };
                            DBHelper.ExecuteSql(sql, cmdParms);

                        }

                        CloseSqlConnection();
                        // 返回生成的 ID
                        return IdReturn;


                    }
                }
                catch (Exception ex) { }

            }
            return null;
        }
    }


}
