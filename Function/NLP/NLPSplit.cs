using JiebaNet.Segmenter;


namespace Function.NLP
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
