using Models;
using Models.ResumeInfo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace CsharpAPI
{
    /// <summary>
    /// 不支持为空的构造函数，必须传入用户名以及Token以获取类<br/>
    /// 建议提供连接测试方法，以测试用户名与Token是否正确
    /// </summary>
    public class LinkToAPI : IServer
    {
        private const string ApiUrl = "https://api.xiaoxizn.com/v1/parser/parse_base";
        private const string UserId = "fdaf1790-ab18-11ef-b1d5-ff5abccbf335";
        private const string Secret = "616af12d-d4c1-470f-a61a-3f25bb86d33d";
        private string API_Json = String.Empty;
        public string GetJson()
        {
            return API_Json;
        }

        public void getJson(string filePath) // 改为同步方法
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

                    HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync(ApiUrl, content).Result; // 使用同步的 PostAsync

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

        public ResumeInfo ResumeFile(string filePath)
        {
            getJson(filePath); // API 的 JSON
            return (ResumeInfo)Function.Factory.ResumeInfoFactory.Get(GetJson()); // 解析好的JSON 
        }



        public Dictionary<string, double> GetSkillGrade(ResumeFile resumeFile)
        {
            //TODO 编写技能评级代码
            throw new NotImplementedException();
        }

        ResumeInfo IServer.ExtractResumeFile(ResumeFile resumeFile)
        {
            // TODO 编写简历解析代码逻辑
            throw new NotImplementedException();
        }
    }

    
}
