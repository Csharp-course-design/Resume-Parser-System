using Function;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpAPI.Factory
{
    public class ResumeFIleFactory : IFactory
    {
        /// <summary>
        /// 生成文件的Models类
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <returns></returns>
        public static object TransJsonToModel(string Path)
        {
            return new ResumeFile(
                0,
                Path,
                Base64Helper.FileToBase64String(Path),
                DateTime.Now
                );
        }
    }
}
