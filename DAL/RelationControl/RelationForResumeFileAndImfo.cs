using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL.RelationControl
{
    public class RelationForResumeFileAndInfo
    {
        /// <summary>
        /// 建立简历与简历信息关系
        /// </summary>
        /// <param name="FileId">文件id</param>
        /// <param name="InfoId">信息id</param>
        /// <returns></returns>
        public bool Link(string FileId, List<string> InfoId)
        {
            bool flag = true;
            foreach (var item in InfoId) {
                string sql = "INSERT INTO RelationResumInfor (ResumeModelId,ResumeInfoId)" +
                "VALUES (@ResumeModelId,@ResumeInfoId);";
                // 准备 SqlCommand
                SqlParameter[] cmdParms = new SqlParameter[]
                    {
                                    new SqlParameter("@ResumeModelId", SqlDbType.Int) { Value = int.Parse(FileId) },
                                    new SqlParameter("@ResumeInfoId", SqlDbType.Int) {Value = int.Parse(item) },
                    };
                flag &= (DBHelper.ExecuteSql(sql, cmdParms) > 0);
            }
            return flag;
            
        }
    }
}
