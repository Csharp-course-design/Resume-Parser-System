using Models;
using Models.ResumeImfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpAPI
{
    /// <summary>
    /// 不支持为空的构造函数，必须传入用户名以及Token以获取类<br/>
    /// 建议提供连接测试方法，以测试用户名与Token是否正确
    /// </summary>
    public class LinkToAPI : IServer
    {
        string user = string.Empty;

        string token = string.Empty;

        /// <summary>
        /// 不支持为空的构造函数，必须传入用户名以及Token以获取类<br/>
        /// 建议提供连接测试方法，以测试用户名与Token是否正确
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        public LinkToAPI(string user, string token)
        {
            User = user;
            Token = token;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string User { get => user; set => user = value; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get => token; set => token = value; }

        public Dictionary<string, double> GetSkillGrade(ResumeFile resumeFile)
        {
            //TODO 编写技能评级代码
            throw new NotImplementedException();
        }

        ResumeImfo IServer.ExtractResumeFile(ResumeFile resumeFile)
        {
            // TODO 编写简历解析代码逻辑
            throw new NotImplementedException();
        }
    }
}
