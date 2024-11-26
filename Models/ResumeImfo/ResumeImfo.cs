using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ResumeImfo.Apart;

namespace Models.ResumeImfo
{
    public class ResumeImfo
    {
        BaseImfo baseImfo = new BaseImfo();
        EduBG eduBG = new EduBG();
        List<string> skill = new List<string>();
        List<WorkExper> workExpers = new List<WorkExper>();

        public ResumeImfo()
        {
        }

        public ResumeImfo(BaseImfo baseImfo, EduBG eduBG, List<string> skill, List<WorkExper> workExpers)
        {
            BaseImfo = baseImfo;
            EduBG = eduBG;
            this.skill = skill;
            this.workExpers = workExpers;
        }


        /// <summary>
        /// 技能
        /// </summary>
        public List<string> Skill { get => skill; set => skill = value; }
        
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
        public BaseImfo BaseImfo { get => baseImfo; set => baseImfo = value; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("=== Resume Information ===");
            sb.AppendLine("Basic Information:");
            sb.AppendLine(BaseImfo.ToString());
            sb.AppendLine();

            sb.AppendLine("Education Background:");
            sb.AppendLine(EduBG.ToString());
            sb.AppendLine();

            sb.AppendLine("Skills:");
            if (Skill.Any())
                sb.AppendLine(string.Join(", ", Skill));
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
