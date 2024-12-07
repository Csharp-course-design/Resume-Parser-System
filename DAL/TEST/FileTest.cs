using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL.DataControl;
namespace DAL.TEST
{
    internal class FileTest
    {
        public ResumeFile resumeFile;
        public ResumeFileControl resumeFileControl;
        public FileTest()
        {
            resumeFile = new ResumeFile(1, "abc", "abcd", Convert.ToDateTime("2006/8/6"));
            resumeFileControl = new ResumeFileControl();
        }

        public void TestInsert()
        {
            string resultId = resumeFileControl.InsertReturnID(resumeFile);
            Console.WriteLine($"插入的简历文件 ID 为: {resultId}");
        }

    }
}
