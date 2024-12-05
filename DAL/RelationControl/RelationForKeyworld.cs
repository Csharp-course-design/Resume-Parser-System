using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL.RelationControl
{
    internal class RelationForKeyworld
    {
        /// <summary>
        /// 建立简历实体与关键字关系表
        /// </summary>
        /// <param name="FileId">文件id</param>
        /// <param name="KeyId">关键字id</param>
        /// <returns></returns>
        public bool Link(string FileId, string KeyId)
        {
            string sql = "INSERT INTO RelationResumKeyworld (ResumeId,KeyId)" +
                "VALUES (@ResumeId,@KeyId);";
            // 准备 SqlCommand
            SqlParameter[] cmdParms = new SqlParameter[]
                {
                                    new SqlParameter("@ResumeId", SqlDbType.Int) { Value = int.Parse(FileId) },
                                    new SqlParameter("@KeyId", SqlDbType.Int) {Value = int.Parse(KeyId) },
                };
            return DBHelper.ExecuteSql(sql, cmdParms) > 0;
        }
    }
}
