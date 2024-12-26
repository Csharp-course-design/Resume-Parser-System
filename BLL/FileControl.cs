using CsharpAPI;
using DAL.DataControl;
using DAL.RelationControl;
using Function.Factory;
using Function.NLP;
using Models;
using Models.ResumeInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FileControl
    {
        static public void SinglePut(string FilePath)
        {
            string FileId = string.Empty;
            try
            {
                ResumeFile resumeFile = (ResumeFile)ResumeFIleFactory.Get(FilePath);
                FileId = (new ResumeFileControl()).InsertReturnID(resumeFile);
                List<string> KeyIds = new List<string>();
                ResumeInfo ResumeInfo = (new LinkToAPI()).ExtractResumeFile(resumeFile);

                // 将简历以字典格式保存为本地
                (new ResumeInfoControl())[resumeFile.Filename] = ResumeInfo;

                string fileContent = ResumeInfo.ToStr();
                List<string> KeyWords = new List<string>(NLPSplit.Split(fileContent));
                foreach (string Key in KeyWords)
                {
                    if (Key != "")
                    {
                        KeyIds.Add(new KeyworldControl().InsertReturnID(Key));
                    }
                }
            (new RelationForKeyworld()).Link(FileId, KeyIds);
            }
            catch (Exception ex) {
                (new ResumeFileControl()).DeleteByID(FileId);
            }
        }
    }
}
