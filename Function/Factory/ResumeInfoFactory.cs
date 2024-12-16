using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Models.ResumeInfo;
using Models.ResumeInfo.Apart;
// G:\01____资源管理\03____工作\简历\赵雨禾-19511669233.pdf

namespace Function.Factory
{
    public class ResumeInfoFactory : IFactory
    {
        public static object Get(string json)
        {
            try
            {
                var data = JObject.Parse(json)?["parsing_result"]; // 获得解析结果的 Json 列表
                if (data == null) throw new Exception("Parsing result not found in JSON."); // 异常处理

                var workExpers = ParseWorkExperiences(data["work_experience"] as JArray); // 解析工作经历

                var basicInfo = data["basic_info"]; // 解析基本信息 
                var contactInfo = data["contact_info"]; // 解析联系方式
                var baseInfo = new BaseInfo(
                    id: 0,
                    name: basicInfo?["name"]?.ToString() ?? string.Empty,
                    age: int.TryParse(basicInfo?["age"]?.ToString(), out var age) ? age : 0,
                    phone: contactInfo?["phone_number"]?.ToString() ?? string.Empty
                ); // 解析到 BaseInfo 类

                var eduBG = new EduBG(
                    school_name: basicInfo?["school_name"]?.ToString() ?? string.Empty,
                    schooll_type: basicInfo?["school_type"]?.ToString() ?? string.Empty,
                    degree: basicInfo?["degree"]?.ToString() ?? string.Empty,
                    major: basicInfo?["major"]?.ToString() ?? string.Empty
                ); // 解析到 eduBG 类

                var skillsArray = data["others"]?["skills"] as JArray; // 技能
                var skills = skillsArray != null ? skillsArray.ToObject<List<string>>() : new List<string>();

                return new ResumeInfo(baseInfo, eduBG, skills, workExpers);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing JSON: {ex.Message}");
                return null;
            }
        }

        private static List<WorkExper> ParseWorkExperiences(JArray workExperiences)
        {
            var workExpers = new List<WorkExper>();
            if (workExperiences != null)
            {
                foreach (var workExper in workExperiences)
                {
                    workExpers.Add(new WorkExper(
                        workExper["start_time_year"]?.ToString() ?? string.Empty,
                        workExper["start_time_month"]?.ToString() ?? string.Empty,
                        workExper["end_time_year"]?.ToString() ?? string.Empty,
                        workExper["end_time_month"]?.ToString() ?? string.Empty,
                        bool.TryParse(workExper["still_active"]?.ToString(), out var still_active) ? still_active : false,
                        workExper["company_name"]?.ToString() ?? string.Empty,
                        workExper["department"]?.ToString() ?? string.Empty,
                        workExper["location"]?.ToString() ?? string.Empty,
                        workExper["job_title"]?.ToString() ?? string.Empty
                    ));
                }
            }
            return workExpers;
        }

    }
}
