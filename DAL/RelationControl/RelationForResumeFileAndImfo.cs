using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL.RelationControl
{
    internal class RelationForResumeFileAndImfo
    {
        /// <summary>
        /// 建立简历与简历信息关系
        /// </summary>
        /// <param name="FileId">文件id</param>
        /// <param name="ImfoId">信息id</param>
        /// <returns></returns>
        public bool Link(string FileId, string ImfoId)
        {

            string sql = "INSERT INTO RelationResumInfor (ResumeModelId,ResumeImfoId)" +
                "VALUES (@ResumeModelId,@ResumeImfoId);";
            // 准备 SqlCommand
            SqlParameter[] cmdParms = new SqlParameter[]
                {
                                    new SqlParameter("@ResumeModelId", SqlDbType.Int) { Value = int.Parse(FileId) },
                                    new SqlParameter("@ResumeImfoId", SqlDbType.Int) {Value = int.Parse(ImfoId) },
                };
            return DBHelper.ExecuteSql(sql, cmdParms) > 0;
        }
    }
}
