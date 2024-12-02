using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Models.ResumeImfo;
using Models.ResumeImfo.Apart;

namespace CsharpAPI.Factory
{
    public class ResumeImfoFactory : IFactory
    {
        public static object TransJsonToModel(string json)
        {
            ResumeImfo resumeImfo = null;

            try
            {
                // 将 JSON 转换为 JObject 以便于解析
                var data = JObject.Parse(json)?["parsing_result"];
                if (data == null)
                {
                    throw new Exception("Parsing result not found in JSON.");
                }

                // 工作经历解析
                List<WorkExper> workExpers = new List<WorkExper>();
                var workExperiences = data["work_experience"] as JArray;
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

                // 基础信息解析
                var basicInfo = data["basic_info"];
                var contactInfo = data["contact_info"];
                var baseImfo = new BaseImfo(
                    id: 0,
                    name: basicInfo?["name"]?.ToString() ?? string.Empty,
                    age: int.TryParse(basicInfo?["age"]?.ToString(), out var age) ? age : 0,
                    phone: contactInfo?["phone_number"]?.ToString() ?? string.Empty
                );

                // 教育背景解析
                var eduBG = new EduBG(
                    school_name: basicInfo?["school_name"]?.ToString() ?? string.Empty,
                    schooll_type: basicInfo?["school_type"]?.ToString() ?? string.Empty,
                    degree: basicInfo?["degree"]?.ToString() ?? string.Empty,
                    major: basicInfo?["major"]?.ToString() ?? string.Empty
                );

                // 技能解析
                var skills = data["others"]?["skills"]?.ToString() ?? string.Empty;

                // 构造最终简历信息对象
                resumeImfo = new ResumeImfo(baseImfo, eduBG, skills, workExpers);
            }
            catch (Exception ex)
            {
                // 打印错误信息，便于调试
                Console.WriteLine($"Error parsing JSON: {ex.Message}");
            }

            return resumeImfo;
        }
    }
}
