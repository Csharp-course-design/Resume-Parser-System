using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Models.ResumeImfo;
using Models.ResumeImfo.Apart;
// G:\01____资源管理\03____工作\简历\赵雨禾-19511669233.pdf

namespace CsharpAPI.Factory
{
    public class ResumeImfoFactory : IFactory
    {
        public static object TransJsonToModel(string json)
        {
            try
            {
                var data = JObject.Parse(json)?["parsing_result"];
                if (data == null) throw new Exception("Parsing result not found in JSON.");

                var workExpers = ParseWorkExperiences(data["work_experience"] as JArray);

                var basicInfo = data["basic_info"];
                var contactInfo = data["contact_info"];
                var baseImfo = new BaseImfo(
                    id: 0,
                    name: basicInfo?["name"]?.ToString() ?? string.Empty,
                    age: int.TryParse(basicInfo?["age"]?.ToString(), out var age) ? age : 0,
                    phone: contactInfo?["phone_number"]?.ToString() ?? string.Empty
                );

                var eduBG = new EduBG(
                    school_name: basicInfo?["school_name"]?.ToString() ?? string.Empty,
                    schooll_type: basicInfo?["school_type"]?.ToString() ?? string.Empty,
                    degree: basicInfo?["degree"]?.ToString() ?? string.Empty,
                    major: basicInfo?["major"]?.ToString() ?? string.Empty
                );

                var skillsArray = data["others"]?["skills"] as JArray;
                var skills = skillsArray != null ? skillsArray.ToObject<List<string>>() : new List<string>();

                return new ResumeImfo(baseImfo, eduBG, skills, workExpers);
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
