using Models.ResumeInfo;
using Models.ResumeInfo.Apart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Function.TransFactory
{
    public class CSVFactory : ITransFactory
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
                string details = string.Join(",", parts.Skip(1));

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
        private string GetStringValue(string[] parts, int index)
        {
            return parts.Length > index && parts[index].Split(": ").Length > 1
                ? parts[index].Split(": ")[1].Trim()
                : string.Empty;
        }

        private int GetIntValue(string[] parts, int index)
        {
            return parts.Length > index && parts[index].Split(": ").Length > 1
                ? int.TryParse(parts[index].Split(": ")[1].Trim(), out int result) ? result : 0
                : 0;
        }

        private bool GetBoolValue(string[] parts, int index)
        {
            return parts.Length > index && parts[index].Split(": ").Length > 1
                ? bool.TryParse(parts[index].Split(": ")[1].Trim(), out bool result) && result
                : false;
        }

        private BaseInfo ParseBaseInfo(string details)
        {
            var parts = details.Split(", ").Select(p => p.Trim()).ToArray();
            return new BaseInfo(
                GetIntValue(parts, 0),
                GetStringValue(parts, 1),
                GetIntValue(parts, 2),
                GetStringValue(parts, 3)
            );
        }

        private EduBG ParseEduBG(string details)
        {
            var parts = details.Split(", ").Select(p => p.Trim()).ToArray();
            return new EduBG(
                GetStringValue(parts, 0),
                GetStringValue(parts, 1),
                GetStringValue(parts, 2),
                GetStringValue(parts, 3)
            );
        }

        private WorkExper ParseWorkExper(string details)
        {
            var parts = details.Split(", ").Select(p => p.Trim()).ToArray();
            return new WorkExper(
                GetStringValue(parts, 0),
                GetStringValue(parts, 1),
                GetStringValue(parts, 2),
                GetStringValue(parts, 3),
                GetBoolValue(parts, 4),
                GetStringValue(parts, 5),
                GetStringValue(parts, 6),
                GetStringValue(parts, 7),
                GetStringValue(parts, 8)
            );
        }

    }
}
