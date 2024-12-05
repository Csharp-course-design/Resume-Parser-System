using System.Text.Json; // 用于 JSON 序列化和反序列化
using Models.ResumeImfo;

namespace Function.TransFactory
{
    internal class JsonFactory : ITransFactory
    {
        /// <summary>
        /// 将格式化字符串（JSON）转换为 ResumeImfo 对象
        /// </summary>
        /// <param name="Content">JSON 格式字符串</param>
        /// <returns>ResumeImfo 对象</returns>
        public ResumeImfo Model(string Content)
        {
            if (string.IsNullOrWhiteSpace(Content))
            {
                throw new ArgumentException("Content 不能为空或仅包含空白字符", nameof(Content));
            }

            try
            {
                return JsonSerializer.Deserialize<ResumeImfo>(Content)
                       ?? throw new InvalidOperationException("反序列化失败，结果为 null");
            }
            catch (JsonException ex)
            {
                throw new FormatException("JSON 格式错误", ex);
            }
        }

        /// <summary>
        /// 将 ResumeImfo 对象转换为 JSON 格式字符串
        /// </summary>
        /// <param name="Model">ResumeImfo 对象</param>
        /// <returns>JSON 格式字符串</returns>
        public string Content(ResumeImfo Model)
        {
            if (Model == null)
            {
                throw new ArgumentNullException(nameof(Model), "Model 不能为空");
            }

            try
            {
                return JsonSerializer.Serialize(Model, new JsonSerializerOptions
                {
                    WriteIndented = true // 格式化输出，方便阅读
                });
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("序列化失败", ex);
            }
        }
    }
}
