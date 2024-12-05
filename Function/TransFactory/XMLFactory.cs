using Models.ResumeInfo;
using System.Xml.Serialization;
using Models.ResumeInfo;

namespace Function.TransFactory
{
    internal class XMLFactory : ITransFactory
    {
        /// <summary>
        /// 将格式化字符串（XML）转换为 ResumeInfo 对象
        /// </summary>
        /// <param name="Content">XML 格式字符串</param>
        /// <returns>ResumeInfo 对象</returns>
        public ResumeInfo Model(string Content)
        {
            if (string.IsNullOrWhiteSpace(Content))
            {
                throw new ArgumentException("Content 不能为空或仅包含空白字符", nameof(Content));
            }

            try
            {
                var serializer = new XmlSerializer(typeof(ResumeInfo));
                using var reader = new StringReader(Content);
                return serializer.Deserialize(reader) as ResumeInfo
                       ?? throw new InvalidOperationException("反序列化失败，结果为 null");
            }
            catch (InvalidOperationException ex)
            {
                throw new FormatException("XML 格式错误", ex);
            }
        }

        /// <summary>
        /// 将 ResumeInfo 对象转换为 XML 格式字符串
        /// </summary>
        /// <param name="Model">ResumeInfo 对象</param>
        /// <returns>XML 格式字符串</returns>
        public string Content(ResumeInfo Model)
        {
            if (Model == null)
            {
                throw new ArgumentNullException(nameof(Model), "Model 不能为空");
            }

            try
            {
                var serializer = new XmlSerializer(typeof(ResumeInfo));
                using var writer = new StringWriter();
                serializer.Serialize(writer, Model);
                return writer.ToString();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("序列化失败", ex);
            }
        }
    }
}
