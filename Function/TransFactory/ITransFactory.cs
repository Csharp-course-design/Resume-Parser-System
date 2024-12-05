using Models.ResumeImfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Function.TransFactory
{
    internal interface ITransFactory
    {
        /// <summary>
        /// 将格式化字符转换为对象
        /// </summary>
        /// <param name="Content">格式化字符串</param>
        /// <returns></returns>
        public ResumeImfo Model(string Content);

        /// <summary>
        /// 将对象转换为格式化字符串
        /// </summary>
        /// <param name="Model">对象</param>
        /// <returns></returns>
        public string Content(ResumeImfo Model);
    }
}
