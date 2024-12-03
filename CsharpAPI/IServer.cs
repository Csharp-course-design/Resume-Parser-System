using Models;
using Models.ResumeImfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpAPI
{
    internal interface IServer
    {
        /// <summary>
        /// 简历解析服务
        /// </summary>
        /// <param name="resumeFile">简历文件对象</param>
        /// <returns>返回简历信息类</returns>
        ResumeImfo ExtractResumeFile(ResumeFile resumeFile);

        /// <summary>
        /// 获取当前简历技能评级
        /// </summary>
        /// <param name="resumeFile">简历文件对象</param>
        /// <returns>
        /// 返回数据字典<br/>
        /// 其中，<br/>
        /// 键为技能字符串<br/>
        /// 值为技能评分
        /// </returns>
        Dictionary<string, double> GetSkillGrade(ResumeFile resumeFile);
    }
}
