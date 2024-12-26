using Models.ResumeInfo;
using Newtonsoft.Json;

namespace BLL
{
    /// <summary>
    /// 用于管理简历信息的控制类，支持从文件加载、保存、修改简历信息。
    /// </summary>
    public class ResumeInfoControl : IDisposable
    {
        private string staticPath = AppDomain.CurrentDomain.BaseDirectory + @"\InfoSet.json";
        private Dictionary<string, ResumeInfo> InfoDic = new Dictionary<string, ResumeInfo>();

        /// <summary>
        /// 初始化 ResumeInfoControl 类实例。如果文件存在，将从文件加载数据；否则，创建一个空字典并创建一个空的文件。
        /// </summary>
        public ResumeInfoControl()
        {
            // 检查文件是否存在
            if (File.Exists(staticPath))
            {
                try
                {
                    // 读取文件内容
                    string jsonData = File.ReadAllText(staticPath);

                    // 反序列化 JSON 数据到字典
                    InfoDic = JsonConvert.DeserializeObject<Dictionary<string, ResumeInfo>>(jsonData) ?? new Dictionary<string, ResumeInfo>();
                }
                catch (Exception ex)
                {
                    // 错误处理（例如文件格式错误）
                    Console.WriteLine($"Error reading or deserializing JSON: {ex.Message}");

                    // 即使发生异常，也保持 InfoDic 为一个空字典
                    InfoDic = new Dictionary<string, ResumeInfo>();
                }
            }
            else
            {
                // 如果文件不存在，创建一个空文件并初始化字典
                Console.WriteLine("File not found, creating empty InfoDic and file.");
                InfoDic = new Dictionary<string, ResumeInfo>();

                // 创建空文件
                CreateEmptyFile();
            }
        }

        /// <summary>
        /// 显式创建一个空的文件
        /// </summary>
        private void CreateEmptyFile()
        {
            try
            {
                // 创建一个空的 JSON 文件
                string emptyJson = JsonConvert.SerializeObject(InfoDic, Formatting.Indented);
                File.WriteAllText(staticPath, emptyJson);
                Console.WriteLine("Empty file has been created.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating empty file: {ex.Message}");
            }
        }

        /// <summary>
        /// 获取当前简历信息字典。
        /// </summary>
        /// <returns>返回当前存储的简历信息字典。</returns>
        public Dictionary<string, ResumeInfo> GetInfoDic()
        {
            return InfoDic;
        }

        /// <summary>
        /// 获取指定命名空间的简历信息。如果不存在，则返回 null。
        /// </summary>
        /// <param name="nameSpace">简历信息的命名空间。</param>
        /// <returns>返回指定命名空间的简历信息对象，如果不存在则返回 null。</returns>
        public ResumeInfo this[string nameSpace]
        {
            get
            {
                // 返回指定键的 ResumeInfo，如果不存在则返回 null
                InfoDic.TryGetValue(nameSpace, out ResumeInfo resumeInfo);
                return resumeInfo;
            }
            set
            {
                // 如果键存在，则修改对应的值
                if (InfoDic.ContainsKey(nameSpace))
                {
                    InfoDic[nameSpace] = value;
                }
                else
                {
                    // 如果键不存在，则添加新的键值对
                    InfoDic.Add(nameSpace, value);
                }
            }
        }

        /// <summary>
        /// 显式保存所有简历信息到文件中。
        /// </summary>
        /// <remarks>
        /// 调用此方法将会将当前字典序列化为 JSON 格式并写入文件。
        /// </remarks>
        /// <exception cref="IOException">
        /// 如果保存文件时发生任何 I/O 错误，将抛出此异常。
        /// </exception>
        public void SaveChanges()
        {
            SaveToFile();
        }

        /// <summary>
        /// 将当前字典中的简历信息保存到文件。
        /// </summary>
        /// <remarks>
        /// 此方法会将整个字典序列化为 JSON 格式并保存到指定的文件中。
        /// </remarks>
        /// <exception cref="IOException">
        /// 如果保存文件时发生任何 I/O 错误，将抛出此异常。
        /// </exception>
        private void SaveToFile()
        {
            try
            {
                // 将字典序列化为 JSON 字符串
                string jsonData = JsonConvert.SerializeObject(InfoDic, Formatting.Indented);

                // 保存到文件
                File.WriteAllText(staticPath, jsonData);
                Console.WriteLine("Data has been saved to file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }

        /// <summary>
        /// 释放资源并确保数据保存到文件。
        /// </summary>
        /// <remarks>
        /// 该方法会在对象销毁前被调用，确保所有未保存的更改都会被保存。
        /// </remarks>
        public void Dispose()
        {
            // 显式保存文件
            SaveToFile();

            // 防止多次释放
            GC.SuppressFinalize(this);
        }
    }
}
