using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JiebaNet.Segmenter;
using static System.Net.Mime.MediaTypeNames;


namespace Function
{
    /// <summary>
    /// 使用jieba分词库
    /// </summary>
    public class NLPSplit
    {
        public string[] Split(string text) 
        {
            var segmenter = new JiebaSegmenter();
            return (string[])segmenter.Cut(text, cutAll: true);
        }
    }
}
