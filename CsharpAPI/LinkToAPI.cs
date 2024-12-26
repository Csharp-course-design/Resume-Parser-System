using Function;
using Microsoft.VisualBasic.FileIO;
using Models;
using Models.ResumeInfo;
using System.Text;
using System.Text.Json;

namespace CsharpAPI
{
    /// <summary>
    /// 提供与小析API的连接并执行简历解析相关的功能。
    /// 该类支持从文件路径获取简历，解析简历并返回结果，以及提取简历的技能评级。
    /// </summary>
    public class LinkToAPI : IServer
    {
        private const string ApiUrl = "https://api.xiaoxizn.com/v1/parser/parse_base";
        private const string UserId = "3a70ef70-b2fb-11ef-b1d5-ff5abccbf335";
        private const string Secret = "a21447af-000c-47b8-95cc-6b49308709f8";

        private string API_Json = String.Empty;

        /// <summary>
        /// 获取最近一次调用API的JSON响应结果。
        /// </summary>
        /// <returns>返回JSON格式的API响应数据。</returns>
        public string GetJson()
        {
            return API_Json;
        }

        /// <summary>
        /// 同步获取指定文件路径的简历文件内容，并发送到API进行解析。
        /// </summary>
        /// <param name="filePath">简历文件的路径。</param>
        public void getJson(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            {
                Console.WriteLine("文件路径无效或文件不存在！");
                return;
            }

            try
            {
                // 将简历文件编码为 Base64
                string fileContent = Convert.ToBase64String(File.ReadAllBytes(filePath));
                string fileName = Path.GetFileName(filePath);

                // 构建请求体
                var requestBody = new
                {
                    resume_base = fileContent,
                    file_name = fileName,
                    parse_mode = "general" // 可选值: fast, general, accurate
                };

                string jsonRequest = JsonSerializer.Serialize(requestBody);

                // 发送请求
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("id", UserId);
                    client.DefaultRequestHeaders.Add("secret", Secret);

                    HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json"); // UTF-8 编码发送
                    HttpResponseMessage response = client.PostAsync(ApiUrl, content).Result; // 使用同步的 PostAsync 接受信息

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = response.Content.ReadAsStringAsync().Result; // 使用同步的 ReadAsStringAsync

                        // 格式化并输出结果
                        var options = new JsonSerializerOptions { WriteIndented = true };
                        var parsedResult = JsonSerializer.Deserialize<JsonElement>(responseBody);
                        API_Json = JsonSerializer.Serialize(parsedResult, options);
                    }
                    else
                    {
                        Console.WriteLine($"请求失败：{response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发生错误：{ex.Message}");
            }
        }

        /// <summary>
        /// 获取简历文件的解析信息。
        /// </summary>
        /// <param name="filePath">简历文件的路径。</param>
        /// <returns>返回解析后的简历信息对象。</returns>
        public ResumeInfo ResumeFile(string filePath)
        {
            getJson(filePath); // API 的 JSON
            return (ResumeInfo)Function.Factory.ResumeInfoFactory.Get(GetJson()); // 解析好的JSON 
        }

        /// <summary>
        /// 获取简历中的技能评分。
        /// </summary>
        /// <param name="resumeFile">简历文件对象，包含简历的Base64编码数据和文件名。</param>
        /// <returns>返回包含技能评分的字符串。</returns>
        public string GetSkillGrade(ResumeFile resumeFile)
        {
            try
            {
                // 将简历文件编码为 Base64
                string fileContent = resumeFile.Base64Data;
                string fileName = resumeFile.Filename;

                // 构建请求体
                var requestBody = new
                {
                    resume_base = fileContent,
                    file_name = fileName
                };

                string jsonRequest = JsonSerializer.Serialize(requestBody);

                // 构建请求 URL 和查询参数
                string requestUrl = $"https://api.xiaoxizn.com/v1/bundle/analyze_base";

                // 发送请求
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("id", "9852a030-bce1-11ef-b1d5-ff5abccbf335");
                    client.DefaultRequestHeaders.Add("secret", "2c9be89f-cb79-42d1-94f0-a47902f980b6");

                    HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync(requestUrl, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = response.Content.ReadAsStringAsync().Result;

                        // 格式化返回结果
                        return ExtractPredictedSkillsFromJson(responseBody);
                        //return (responseBody);
                    }
                    else
                    {
                        return $"请求失败：{response.StatusCode} - {response.ReasonPhrase}";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"发生错误：{ex.Message}";
            }
        }

        /// <summary>
        /// 提取并格式化简历解析结果中的技能信息。
        /// </summary>
        /// <param name="jsonResponse">API返回的JSON响应数据。</param>
        /// <returns>返回格式化后的技能信息字符串。</returns>
        public string ExtractPredictedSkillsFromJson(string jsonResponse)
        {
            try
            {
                // 解析 JSON 响应
                var parsedResult = JsonSerializer.Deserialize<JsonElement>(jsonResponse);

                // 提取 parsing_result 中的 predicted_skills 字段
                if (parsedResult.TryGetProperty("predicted_result", out var parsingResult) &&
                    parsingResult.TryGetProperty("predicted_skills", out var predictedSkills))
                {
                    // 将 predicted_skills 转换为字符串形式
                    var skillsList = new List<string>();
                    foreach (var skill in predictedSkills.EnumerateArray())
                    {
                        string skillName = skill.GetProperty("skill").GetString();
                        double score = skill.GetProperty("score").GetDouble();
                        skillsList.Add($"Skill: {skillName}, Score: {score:F2}");
                    }

                    // 将技能列表格式化为一个字符串
                    return string.Join("\n", skillsList);
                }
                else
                {
                    return "未找到 predicted_skills 数据";
                }
            }
            catch (Exception ex)
            {
                return $"发生错误：{ex.Message}";
            }
        }

        /// <summary>
        /// 将Base64编码的简历文件转换为文件并提取简历信息。
        /// </summary>
        /// <param name="resumeFile">简历文件对象，包含Base64编码数据和文件名。</param>
        /// <returns>返回提取的简历信息。</returns>
        public ResumeInfo ExtractResumeFile(ResumeFile resumeFile)
        {
            // 将Base64编码的文件内容转换为实际文件
            Base64Helper.Base64StringToFile(resumeFile.Base64Data, resumeFile.Filename);

            // 使用之前定义的 ResumeFile 方法获取解析后的简历信息
            ResumeInfo resumeInfo = ResumeFile(resumeFile.Filename);

            // 删除临时的简历文件
            FileSystem.DeleteFile(resumeFile.Filename, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);

            return resumeInfo;
        }
    }
}
