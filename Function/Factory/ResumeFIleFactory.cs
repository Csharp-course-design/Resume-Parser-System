using Function;
using Models;

namespace Function.Factory
{
    public class ResumeFIleFactory : IFactory
    {
        /// <summary>
        /// 生成文件的Models类
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        /// <returns></returns>
        public static object Get(string FilePath)
        {
            // path 改为 只获取文件名 
            return new ResumeFile(
                0,
                Path.GetFileName(FilePath),
                Base64Helper.FileToBase64String(FilePath),
                DateTime.Now
                );
        }
    }
}
