



using Models.ResumeInfo;
using System.Text;
using Models.ResumeInfo;
using Models.ResumeInfo.Apart;


// Hack Data
/*

错误数据

Skills, EduBG, WorkExpers, BaseInfo
System.Collections.Generic.List`1[System.String],School Name: 青岛科技大学, School Type: , Degree: 本科, Major: 数据科学与大数据技术,System.Collections.Generic.List`1[Models.ResumeInfo.Apart.WorkExper],ID: 0, Name: 闫振斌, Age: 0, Phone: 18146552582

 */

namespace Function.TransFactory
{
    public class CSVFactory : ITransFactory
    {
        /// <summary>
        /// 将格式化字符串（CSV）转换为 ResumeInfo 对象
        /// </summary>
        /// <param name="Content">CSV 格式字符串</param>
        /// <returns>ResumeInfo 对象</returns>
        public ResumeInfo Model(string Content)
        {
            if (string.IsNullOrWhiteSpace(Content))
            {
                throw new ArgumentException("Content 不能为空或仅包含空白字符", nameof(Content));
            }

            try
            {
                var lines = Content.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                if (lines.Length < 2)
                {
                    throw new FormatException("CSV 内容格式不正确，缺少表头或数据行");
                }

                var headers = lines[0].Split(',');
                var values = lines[1].Split(',');

                var resume = new ResumeInfo();
                for (int i = 0; i < headers.Length; i++)
                {
                    var propertyName = headers[i].Trim();
                    var propertyValue = values[i].Trim();

                    var property = typeof(ResumeInfo).GetProperty(propertyName);
                    if (property != null)
                    {
                        property.SetValue(resume, Convert.ChangeType(propertyValue, property.PropertyType));
                    }
                }

                return resume;
            }
            catch (Exception ex)
            {
                throw new FormatException("CSV 格式错误或无法解析", ex);
            }
        }

        /// <summary>
        /// 将 ResumeInfo 对象转换为 CSV 格式字符串
        /// </summary>
        /// <param name="Model">ResumeInfo 对象</param>
        /// <returns>CSV 格式字符串</returns>
        public string Content(ResumeInfo Model)
        {
            if (Model == null)
            {
                throw new ArgumentNullException(nameof(Model), "Model 不能为空");
            }

            try
            {
                var properties = typeof(ResumeInfo).GetProperties();
                var headers = new StringBuilder();
                var values = new StringBuilder();

                foreach (var property in properties)
                {
                    headers.Append(property.Name).Append(',');
                    var value = property.GetValue(Model)?.ToString() ?? string.Empty;
                    values.Append(value).Append(',');
                }

                // 移除末尾的多余逗号
                headers.Length--;
                values.Length--;

                return $"{headers}\n{values}";
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("序列化为 CSV 失败", ex);
            }
        }
    }
}

// Wrong format conversion :
//using Models.ResumeInfo.Apart;
//using Models.ResumeInfo;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Function.TransFactory
//{
//    public class CSVFactory : ITransFactory
//    {
//        public ResumeInfo Model(string Content)
//        {
//            if (string.IsNullOrWhiteSpace(Content))
//                throw new ArgumentException("Content 不能为空", nameof(Content));

//            try
//            {
//                var lines = Content.Split('\n', StringSplitOptions.RemoveEmptyEntries);
//                if (lines.Length < 2)
//                    throw new FormatException("CSV 内容格式不正确，缺少表头或数据行");

//                var headers = lines[0].Split(',');
//                var values = lines[1].Split(',');

//                // 校验列数是否匹配
//                if (headers.Length != values.Length)
//                    throw new FormatException("CSV 的列数与数据不匹配");

//                var resume = new ResumeInfo();
//                for (int i = 0; i < headers.Length; i++)
//                {
//                    var propertyName = headers[i].Trim();
//                    var propertyValue = values[i].Trim();

//                    var property = typeof(ResumeInfo).GetProperty(propertyName);
//                    if (property != null)
//                    {
//                        if (property.PropertyType == typeof(List<EduBG>))
//                        {
//                            // 解析为 List<EduBG>
//                            var eduList = propertyValue
//                                .Split('|', StringSplitOptions.RemoveEmptyEntries)
//                                .Select(ParseEduBG)
//                                .ToList();
//                            property.SetValue(resume, eduList);
//                        }
//                        else if (property.PropertyType == typeof(List<WorkExper>))
//                        {
//                            // 解析为 List<WorkExper>
//                            var workExperList = propertyValue
//                                .Split('|', StringSplitOptions.RemoveEmptyEntries)
//                                .Select(ParseWorkExper)
//                                .ToList();
//                            property.SetValue(resume, workExperList);
//                        }
//                        else
//                        {
//                            // 默认处理基础类型
//                            property.SetValue(resume, Convert.ChangeType(propertyValue, property.PropertyType));
//                        }
//                    }
//                }

//                return resume;
//            }
//            catch (Exception ex)
//            {
//                throw new FormatException($"CSV 格式错误或无法解析，错误内容：'{Content}'", ex);
//            }
//        }

//        public string Content(ResumeInfo Model)
//        {
//            if (Model == null)
//                throw new ArgumentNullException(nameof(Model), "Model 不能为空");

//            try
//            {
//                var properties = typeof(ResumeInfo).GetProperties();
//                var headers = new StringBuilder();
//                var values = new StringBuilder();

//                foreach (var property in properties)
//                {
//                    headers.Append(property.Name).Append(',');

//                    var value = property.GetValue(Model);
//                    if (value is List<EduBG> eduList)
//                    {
//                        // 序列化 List<EduBG>
//                        values.Append(string.Join('|', eduList.Select(SerializeEduBG)));
//                    }
//                    else if (value is List<WorkExper> workExperList)
//                    {
//                        // 序列化 List<WorkExper>
//                        values.Append(string.Join('|', workExperList.Select(SerializeWorkExper)));
//                    }
//                    else
//                    {
//                        // 序列化其他类型
//                        values.Append(value?.ToString() ?? string.Empty);
//                    }

//                    values.Append(',');
//                }

//                // 移除末尾多余的逗号
//                headers.Length--;
//                values.Length--;

//                return $"{headers}\n{values}";
//            }
//            catch (Exception ex)
//            {
//                throw new InvalidOperationException("序列化为 CSV 失败", ex);
//            }
//        }

//        private EduBG ParseEduBG(string eduBGString)
//        {
//            // 假设 EduBG 格式为 "SchoolName:SchoolType:Degree:Major"
//            var parts = eduBGString.Split(':');
//            if (parts.Length != 4)
//                throw new FormatException("EduBG 格式错误，应为 'SchoolName:SchoolType:Degree:Major'");

//            return new EduBG(parts[0], parts[1], parts[2], parts[3]);
//        }

//        private string SerializeEduBG(EduBG eduBG)
//        {
//            // 序列化 EduBG 格式为 "SchoolName:SchoolType:Degree:Major"
//            return $"{eduBG.School_name}:{eduBG.Schooll_type}:{eduBG.Degree}:{eduBG.Major}";
//        }

//        private WorkExper ParseWorkExper(string workExperString)
//        {
//            // 假设 WorkExper 格式为 "StartYear:StartMonth:EndYear:EndMonth:StillActive:CompanyName:Department:Location:JobTitle"
//            var parts = workExperString.Split(':');
//            if (parts.Length != 9)
//                throw new FormatException("WorkExper 格式错误，应为 'StartYear:StartMonth:EndYear:EndMonth:StillActive:CompanyName:Department:Location:JobTitle'");

//            return new WorkExper
//            {
//                Start_time_year = parts[0],
//                Start_time_month = parts[1],
//                End_time_year = parts[2],
//                End_time_month = parts[3],
//                Still_active = bool.Parse(parts[4]),
//                Company_name = parts[5],
//                Department = parts[6],
//                Location = parts[7],
//                Job_title = parts[8]
//            };
//        }

//        private string SerializeWorkExper(WorkExper workExper)
//        {
//            // 序列化 WorkExper 格式为 "StartYear:StartMonth:EndYear:EndMonth:StillActive:CompanyName:Department:Location:JobTitle"
//            return $"{workExper.Start_time_year}:{workExper.Start_time_month}:{workExper.End_time_year}:{workExper.End_time_month}:{workExper.Still_active}:{workExper.Company_name}:{workExper.Department}:{workExper.Location}:{workExper.Job_title}";
//        }
//    }
//}