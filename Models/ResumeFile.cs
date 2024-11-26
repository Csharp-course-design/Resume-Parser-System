namespace Models
{
    public class ResumeFile
    {
        public ResumeFile()
        {
        }

        public ResumeFile(int id, string filename, string base64Data, DateTime date)
        {
            Id = id;
            Filename = filename;
            Base64Data = base64Data;
            Date = date;
        }

        public int Id { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string Filename { get; set; } = string.Empty;

        /// <summary>
        /// Base64格式编码的内容
        /// </summary>
        public string Base64Data { get; set; } = string.Empty;

        public DateTime Date { get; set; } = DateTime.MinValue;

    }
}
