using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpAPI.Factory
{
    public interface IFactory
    {
        /// <summary>
        /// 获取Models类
        /// </summary>
        /// <param name="Json">Json文件内容</param>
        /// <returns>Models类</returns>
        abstract public static Object TransJsonToModel(string Json);
    }
}
