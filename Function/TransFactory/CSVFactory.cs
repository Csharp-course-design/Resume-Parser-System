using Models.ResumeInfo;
using Models.ResumeInfo.Apart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Function.TransFactory
{
    public class TransFactory : ITransFactory
    {
        public ResumeInfo Model(string Content)
        {
            var lines = Content.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            ResumeInfo resumeInfo = new ResumeInfo();

            foreach (var line in lines.Skip(1)) // Skip header
            {
                var parts = line.Split(',');
                if (parts.Length < 2) continue;

                string category = parts[0].Trim();
                string details = parts[1].Trim();

                switch (category)
                {
                    case "Basic Information":
                        resumeInfo.BaseInfo = ParseBaseInfo(details);
                        break;
                    case "Education Background":
                        resumeInfo.EduBG = ParseEduBG(details);
                        break;
                    case "Skills":
                        resumeInfo.Skills = details.Split(';').Select(s => s.Trim()).ToList();
                        break;
                    case "Work Experience":
                        resumeInfo.WorkExpers.Add(ParseWorkExper(details));
                        break;
                }
            }

            return resumeInfo;
        }

        public string Content(ResumeInfo Model)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Category,Details");

            sb.AppendLine($"Basic Information,{Model.BaseInfo}");
            sb.AppendLine($"Education Background,{Model.EduBG}");

            sb.AppendLine(Model.Skills.Any() ? $"Skills,{string.Join("; ", Model.Skills)}" : "Skills,None");

            if (Model.WorkExpers.Any())
            {
                foreach (var workExper in Model.WorkExpers)
                {
                    sb.AppendLine($"Work Experience,{workExper}");
                }
            }
            else
            {
                sb.AppendLine("Work Experience,No work experience");
            }

            return sb.ToString();
        }

        private BaseInfo ParseBaseInfo(string details)
        {
            var parts = details.Split(';').Select(p => p.Trim()).ToArray();
            return new BaseInfo(int.Parse(parts[0]), parts[1], int.Parse(parts[2]), parts[3]);
        }

        private EduBG ParseEduBG(string details)
        {
            var parts = details.Split(';').Select(p => p.Trim()).ToArray();
            return new EduBG(parts[0], parts[1], parts[2], parts[3]);
        }

        private WorkExper ParseWorkExper(string details)
        {
            var parts = details.Split(';').Select(p => p.Trim()).ToArray();
            return new WorkExper(parts[0], parts[1], parts[2], parts[3], bool.Parse(parts[4]), parts[5], parts[6], parts[7], parts[8]);
        }
    }
}
