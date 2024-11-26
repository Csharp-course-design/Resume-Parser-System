using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpAPI
{
    internal interface IServer
    {
        ResumeFile ExtractResumeFile(ResumeFile resumeFile);


    }
}
