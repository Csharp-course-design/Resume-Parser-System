using CsharpAPI;
using DAL.DataControl;
using DAL.RelationControl;
using Function.Factory;
using Function.NLP;
using Models;
using Models.ResumeInfo;

namespace BLL
{
    public class FileControl
    {
        /// <summary>
        /// 把指定路径下的文件添加到数据库中，并绑定好对应的关键
        /// </summary>
        /// <param name="FilePath">指定路径下的文件</param>
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
            catch (Exception ex)
            {
                (new ResumeFileControl()).DeleteByID(FileId);
            }
        }
    }
}
