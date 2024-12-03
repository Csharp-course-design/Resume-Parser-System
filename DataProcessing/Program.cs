using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    private const string ApiUrl = "https://api.xiaoxizn.com/v1/parser/parse_base";
    private const string UserId = "fdaf1790-ab18-11ef-b1d5-ff5abccbf335";
    private const string Secret = "616af12d-d4c1-470f-a61a-3f25bb86d33d";

    static async Task Main(string[] args)
    {
        Console.WriteLine("请输入简历文件路径:");
        string filePath = Console.ReadLine();

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
                HttpResponseMessage response = await client.PostAsync(ApiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // 格式化并输出结果
                    var options = new JsonSerializerOptions { WriteIndented = true };
                    var parsedResult = JsonSerializer.Deserialize<JsonElement>(responseBody);
                    Console.WriteLine(JsonSerializer.Serialize(parsedResult, options));
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
}
/*

C:\Users\95432\Desktop\闫振斌.pdf
{
  "applied_job": "",
  "src_id": "",
  "ocr_count": 0,
  "cv_language": "zh",
  "src_site": "other",
  "file_name": "\u95EB\u632F\u658C.pdf",
  "errormessage": "succeed",
  "cv_id": "",
  "english_parsing_result": {},
  "avatar_url": "",
  "updated_time": "",
  "cv_name": "8e66abe0058d0eee8ff7587584b9d66e.pdf",
  "resume_integrity": 0.6352691808368806,
  "parsing_result": {
    "project_experience": [
      {
        "start_time_month": "",
        "project_name": "\u5B8C\u6210\u94F6\u884C\u7EA2\u540D\u5355\u7684\u5B9A\u65F6\u540C\u6B65\u4EFB\u52A1,",
        "description": "",
        "end_time_year": "",
        "still_active": 0,
        "skills": [],
        "end_time_month": "",
        "start_time_year": "",
        "company_name": "",
        "job_function": "",
        "job_title": "",
        "location": ""
      },
      {
        "start_time_month": "",
        "project_name": "\u72EC\u7ACB\u5B8C\u6210\u7528\u6237\u7BA1\u7406\u7684\u4E00\u6574\u5957\u903B\u8F91,",
        "description": "",
        "end_time_year": "",
        "still_active": 0,
        "skills": [],
        "end_time_month": "",
        "start_time_year": "",
        "company_name": "",
        "job_function": "",
        "job_title": "",
        "location": ""
      },
      {
        "start_time_month": "",
        "project_name": "\u8272\u7684\u529F\u80FD(\u91C7\u7528\u4E8C\u5206\u8FDB\u884C\u4F18\u5316)",
        "description": "Excel \u5BFC\u51FA\n\u5B8C\u6210\u5BF9\u9879\u76EE\u4E2D\u90E8\u5206\u529F\u80FD\u7684\u4F18\u5316: \u91C7\u7528\u8D23\u4EFB\u94FE\u8BBE\u8BA1\u6A21\u5F0F\u5BF9\u94F6\u884C\u9879\u76EE\u7684\u903B\u8F91\u8FDB\u884C\u4F18\u5316",
        "end_time_year": "",
        "still_active": 0,
        "skills": [
          "\u8BBE\u8BA1\u6A21\u5F0F",
          "\u529F\u80FD",
          "excel",
          "\u4F18\u5316"
        ],
        "end_time_month": "",
        "start_time_year": "",
        "company_name": "",
        "job_function": "",
        "job_title": "",
        "location": ""
      },
      {
        "start_time_month": "",
        "project_name": "\u4E86\u89E3\u4E86\u94F6\u884C\u9879\u76EE\u4E2D\u7684\u6570\u636E\u8868\u7279\u70B9,",
        "description": "\u9879\u76EE\u91C7\u7528 SpringCloudAlibaba \u6784\u5EFA, \u5206\u4E3A\u7F51\u5173\uFF0C\u8BA2\u5355\uFF0C\u652F\u4ED8\uFF0C\u8D2D\u7968\uFF0C\u7528\u6237\u4E94\u5927\u6A21\u5757\u3002\u5B8C\u6210\u4F1A\u5458\u6CE8\u518C, \u8F66\u7968\u67E5\u8BE2,\n\u8F66\u7968\u4E0B\u5355\u53CA\u652F\u4ED8\u7B49\u4E1A\u52A1\u3002\u5728\u9AD8\u5E76\u53D1\u7684\u73AF\u5883\u4E0B\uFF0C\u6CE8\u5B9A 12306 \u7CFB\u7EDF\u662F\u4E00\u4E2A\u9AD8\u5EA6\u590D\u6742\u3001\u7CBE\u5BC6\u7684\u7CFB\u7EDF\u3002\n\u4E3A\u4E86\u89E3\u51B3\u652F\u4ED8\u7ED3\u679C\u56DE\u8C03\u7684\u95EE\u9898, \u4F7F\u7528 netapp \u5DE5\u5177\u5B9E\u73B0\u5185\u7F51\u7A7F\u900F\n\u4E3A\u4E86\u89E3\u51B3\u6CE8\u518C\u7528\u6237\u65F6\u7684\u7F13\u5B58\u7A7F\u900F\u4EE5\u53CA\u7528\u6237\u540D\u6CE8\u9500\u540E\u7684\u590D\u7528\u95EE\u9898\uFF0C\u5728\u5E03\u9686\u8FC7\u6EE4\u5668\u7684\u57FA\u7840\u4E0A\u6DFB\u52A0\u4E86\u4E00\u5C42 redis\n\u7F13\u5B58\uFF0C\u89E3\u51B3\u4E86\u4F8B\u5982\u5E03\u9686\u8FC7\u6EE4\u5668\u65E0\u6CD5\u5220\u9664\u7B49\u5E38\u89C1\u7F13\u5B58\u7A7F\u900F\u89E3\u51B3\u65B9\u6848\u7684\u5F0A\u7AEF\n\u57FA\u4E8E\u5217\u8F66\u641C\u7D22\u7684\u7279\u70B9, \u4F7F\u7528 Redis \u8FDB\u884C\u8F66\u7968\u641C\u7D22\u800C\u975E ElasticSearch\n\u4E3A\u4E86\u89E3\u51B3\u6570\u636E\u6821\u9A8C\u7B49\u5404\u79CD\u903B\u8F91\u7EC4\u5408\u65F6\u4EE3\u7801\u8FC7\u957F, \u4F7F\u7528\u8D23\u4EFB\u94FE\u6A21\u5F0F\u89E3\u8026\u5404\u79CD\u903B\u8F91\u5224\u65AD\n\u901A\u8FC7 RocketMQ \u5EF6\u65F6\u6D88\u606F\u7279\u6027\uFF0C\u5B8C\u6210\u7528\u6237\u8D2D\u7968 10 \u5206\u949F\u540E\u672A\u652F\u4ED8\u60C5\u51B5\u4E0B\u53D6\u6D88\u8BA2\u5355\u529F\u80FD\n\u5C01\u88C5\u7F13\u5B58\u7EC4\u4EF6\u5E93\u907F\u514D\u6CE8\u518C\u7528\u6237\u65F6\uFF0C\u7528\u6237\u540D\u5168\u5C40\u552F\u4E00\u5E26\u6765\u7684\u7F13\u5B58\u7A7F\u900F\u95EE\u9898\uFF0C\u51CF\u8F7B\u6570\u636E\u5E93\u8BBF\u95EE\u538B\u529B\n\u4F7F\u7528 BinLog \u914D\u5408 RocketMQ \u6D88\u606F\u961F\u5217\u5B8C\u6210 MySQL \u6570\u636E\u5E93\u4E0E Redis \u7F13\u5B58\u4E4B\u95F4\u7684\u6570\u636E\u6700\u7EC8\u4E00\u81F4\u6027\n\u901A\u8FC7 RedisLua \u811A\u672C\u539F\u5B50\u7279\u6027\uFF0C\u5B8C\u6210\u7528\u6237\u8D2D\u7968\u4EE4\u724C\u5206\u914D\uFF0C\u901A\u8FC7\u4EE4\u724C\u9650\u6D41\u4EE5\u5E94\u5BF9\u6D77\u91CF\u7528\u6237\u8D2D\u7968\u8BF7\u6C42\n\u901A\u8FC7\u8BA2\u5355\u53F7\u548C\u7528\u6237\u4FE1\u606F\u590D\u5408\u5206\u7247\u7B97\u6CD5\u5B8C\u6210\u8BA2\u5355\u6570\u636E\u5206\u5E93\u5206\u8868\uFF0C\u652F\u6301\u8BA2\u5355\u53F7\u548C\u7528\u6237\u67E5\u8BE2\u7EF4\u5EA6\n\u521B\u5EFA\u8BA2\u5355\u660E\u7EC6\u4E0E\u4E58\u8F66\u4EBA\u7684\u5173\u8054\u8868\uFF0C\u5206\u5E93\u5206\u8868\u89C4\u5219\u540C\u8BA2\u5355\uFF0C\u5B8C\u6210\u4E58\u8F66\u4EBA\u8D26\u53F7\u767B\u5F55\u67E5\u8BE2\u672C\u4EBA\u8F66\u7968\u529F\u80FD",
        "end_time_year": "",
        "still_active": 0,
        "skills": [
          "springcloud",
          "redis",
          "alibaba",
          "\u7F13\u5B58",
          "\u6570\u636E\u5E93",
          "elasticsearch",
          "mysql",
          "\u529F\u80FD",
          "rocketmq"
        ],
        "end_time_month": "",
        "start_time_year": "",
        "company_name": "",
        "job_function": "",
        "job_title": "",
        "location": ""
      }
    ],
    "contact_info": {
      "phone_number": "18146552582",
      "QQ": "582410308",
      "wechat": "",
      "email": "qq2582410308@163.com",
      "home_phone_number": ""
    },
    "social_experience": [],
    "education_experience": [
      {
        "ranking": "",
        "major": "\u6570\u636E\u79D1\u5B66\u4E0E\u5927\u6570\u636E\u6280\u672F",
        "degree": "\u672C\u79D1",
        "end_time_year": "2025",
        "still_active": 1,
        "school_rank": "180",
        "school_level": "",
        "end_time_month": "06",
        "school_name": "\u9752\u5C9B\u79D1\u6280\u5927\u5B66",
        "abroad_country": "",
        "GPA": "",
        "courses": "",
        "start_time_year": "2021",
        "location": "\u9752\u5C9B",
        "department": "",
        "study_model": "",
        "abroad": 0,
        "start_time_month": "09"
      }
    ],
    "resume_trainning_rawtext": "",
    "basic_info": {
      "major": "\u6570\u636E\u79D1\u5B66\u4E0E\u5927\u6570\u636E\u6280\u672F",
      "expect_location": "",
      "current_salary": "",
      "current_location_norm": "",
      "current_location": "",
      "expect_location_norm": "",
      "desired_salary": "",
      "birthplace": "",
      "political_status": "",
      "desired_industry": "",
      "desired_position": "",
      "zipcode": "",
      "professional_level": "\u521D\u7EA7",
      "national_identity_number": "",
      "date_of_birth": "",
      "birthplace_norm": "",
      "num_work_experience": 1,
      "current_position": "\u540E\u7AEF\u5F00\u53D1\u5DE5\u7A0B\u5E08",
      "work_start_year": "2025",
      "degree": "\u672C\u79D1",
      "current_company": "\u4E0A\u6D77\u81F4\u5B87\u4FE1\u606F\u79D1\u6280\u6709\u9650\u516C\u53F8",
      "school_name": "\u9752\u5C9B\u79D1\u6280\u5927\u5B66",
      "ethnic": "",
      "recent_graduate_year": "2025",
      "name": "\u95EB\u632F\u658C",
      "lastupdate_time": "2024-12-03-04-54-05",
      "gender": "\u7537",
      "age": null,
      "marital_status": "",
      "current_status": "",
      "school_type": "",
      "detailed_location": "",
      "industry": ""
    },
    "training_experience": [],
    "work_experience": [
      {
        "salary": "",
        "description": "\u9879\u76EE\u4ECB\u7ECD: \u53C2\u4E0E\u4E2D\u56FD\u94F6\u884C\u5386\u53F2\u6570\u636E\u68C0\u7D22\u7CFB\u7EDF\u7684\u5F00\u53D1\uFF0C\u8BE5\u7CFB\u7EDF\u4E3B\u8981\u5305\u62EC\u6743\u9650\u7BA1\u7406\u3001\u7CFB\u7EDF\u8BBE\u7F6E\u3001\u5BA2\u6237\u4FE1\u606F\u3001\u4EA4\u6613\u8BB0\u5F55\u548C",
        "end_time_year": "2024",
        "still_active": 0,
        "skills": [],
        "end_time_month": "11",
        "industry": "",
        "start_time_month": "10",
        "company_type": "",
        "department": "",
        "company_name": "\u4E0A\u6D77\u81F4\u5B87\u4FE1\u606F\u79D1\u6280\u6709\u9650\u516C\u53F8",
        "start_time_year": "2024",
        "location": "",
        "report_to": "",
        "job_function": "\u8F6F\u4EF6/\u4E92\u8054\u7F51\u5F00\u53D1/\u7CFB\u7EDF\u96C6\u6210",
        "underling_num": "",
        "job_title": "\u540E\u7AEF\u5F00\u53D1\u5DE5\u7A0B\u5E08",
        "company_size": ""
      }
    ],
    "others": {
      "language": [],
      "certificate": [],
      "skills": [
        "Jpa",
        "Aop",
        "C\u002B\u002B",
        "SQL",
        "Ioc",
        "Vue",
        "Java",
        "Hive",
        "MySQL",
        "Cloud",
        "Redis",
        "C/C\u002B\u002B",
        "Excel",
        "\u4F18\u5316",
        "Spring",
        "Innodb",
        "Python",
        "\u7F13\u5B58",
        "\u67B6\u6784",
        "\u529F\u80FD",
        "Mybatis",
        "Java Ee",
        "Alibaba",
        "Sentinel",
        "rocketmq",
        "\u5927\u6570\u636E",
        "\u6570\u636E\u5E93",
        "Spring Boot",
        "springcloud",
        "Mybatis-Plus",
        "Sql\u6570\u636E\u5E93",
        "\u6570\u636E\u7ED3\u6784",
        "Spring\u6846\u67B6",
        "\u7A0B\u5E8F\u8BBE\u8BA1",
        "\u8BBE\u8BA1\u6A21\u5F0F",
        "Elasticsearch",
        "Mysql\u6570\u636E\u5E93"
      ],
      "self_evaluation": "",
      "awards": [],
      "it_skills": [
        "C\u002B\u002B",
        "SQL",
        "Java",
        "Hive",
        "MySQL",
        "Excel",
        "Python",
        "rocketmq",
        "springcloud"
      ],
      "business_skills": [
        "Excel"
      ]
    },
    "resume_rawtext": ""
  },
  "version": "5.7.0",
  "client_id": "fdaf1790-ab18-11ef-b1d5-ff5abccbf335",
  "file_ext": "pdf",
  "errorcode": 0,
  "avatar_data": "",
  "hash_id": "8e66abe0058d0eee8ff7587584b9d66e20"
}

 */ 