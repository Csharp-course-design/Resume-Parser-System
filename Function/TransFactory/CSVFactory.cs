using Models.ResumeImfo;
using System.Text;

namespace Function.TransFactory
{
    internal class CSVFactory : ITransFactory
    {
        /// <summary>
        /// 将格式化字符串（CSV）转换为 ResumeImfo 对象
        /// </summary>
        /// <param name="Content">CSV 格式字符串</param>
        /// <returns>ResumeImfo 对象</returns>
        public ResumeImfo Model(string Content)
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

                var resume = new ResumeImfo();
                for (int i = 0; i < headers.Length; i++)
                {
                    var propertyName = headers[i].Trim();
                    var propertyValue = values[i].Trim();

                    var property = typeof(ResumeImfo).GetProperty(propertyName);
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
        /// 将 ResumeImfo 对象转换为 CSV 格式字符串
        /// </summary>
        /// <param name="Model">ResumeImfo 对象</param>
        /// <returns>CSV 格式字符串</returns>
        public string Content(ResumeImfo Model)
        {
            if (Model == null)
            {
                throw new ArgumentNullException(nameof(Model), "Model 不能为空");
            }

            try
            {
                var properties = typeof(ResumeImfo).GetProperties();
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
