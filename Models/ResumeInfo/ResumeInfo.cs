using Models.ResumeInfo.Apart;
using System.Text;
using System.Threading.Tasks;
using Models.ResumeInfo.Apart;

namespace Models.ResumeInfo
{
    public class ResumeInfo
    {
        BaseInfo baseInfo = new BaseInfo();
        EduBG eduBG = new EduBG();
        List<string> skill = new List<string>();
        List<WorkExper> workExpers = new List<WorkExper>();

        public ResumeInfo()
        {
        }

        public ResumeInfo(BaseInfo baseInfo, EduBG eduBG, List<string> skill, List<WorkExper> workExpers)
        {
            BaseInfo = baseInfo;
            EduBG = eduBG;
            this.skill = skill;
            this.workExpers = workExpers;
        }


        /// <summary>
        /// 技能
        /// </summary>
        public List<string> Skills { get => skill; set => skill = value; }

        /// <summary>
        /// 教育背景
        /// </summary>
        public EduBG EduBG { get => eduBG; set => eduBG = value; }

        /// <summary>
        /// 工作经历
        /// </summary>
        public List<WorkExper> WorkExpers { get => workExpers; set => workExpers = value; }

        /// <summary>
        /// 基础信息
        /// </summary>
        public BaseInfo BaseInfo { get => baseInfo; set => baseInfo = value; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("=== Resume Information ===");
            sb.AppendLine("Basic Information:");
            sb.AppendLine(BaseInfo.ToString());
            sb.AppendLine();

            sb.AppendLine("Education Background:");
            sb.AppendLine(EduBG.ToString());
            sb.AppendLine();

            sb.AppendLine("Skills:");
            if (Skills.Any())
                sb.AppendLine(string.Join(", ", Skills));
            else
                sb.AppendLine("None");
            sb.AppendLine();

            sb.AppendLine("Work Experience:");
            if (WorkExpers.Any())
            {
                foreach (var workExper in WorkExpers)
                {
                    sb.AppendLine(workExper.ToString());
                }
            }
            else
            {
                sb.AppendLine("No work experience.");
            }

            return sb.ToString();
        }
    }
}
