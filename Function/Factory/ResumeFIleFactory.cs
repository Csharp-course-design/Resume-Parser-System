using Function;
using Models;

namespace CsharpAPI.Factory
{
    public class ResumeFIleFactory : IFactory
    {
        /// <summary>
        /// 生成文件的Models类
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <returns></returns>
        public static object Get(string Path)
        {
            // path 改为 只获取文件名 
            return new ResumeFile(
                0,
                Path,
                Base64Helper.FileToBase64String(Path),
                DateTime.Now
                );
        }
    }
}
