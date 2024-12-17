using JiebaNet.Segmenter;


namespace Function.NLP
{
    /// <summary>
    /// 使用jieba分词库
    /// </summary>
    public class NLPSplit
    {
        /// <summary>
        /// 分词方法
        /// </summary>
        /// <param name="text">要分词的文本</param>
        /// <param name="cutAll">是否启用全模式分词</param>
        /// <returns>分词结果数组</returns>
        static public string[] Split(string text)
        {
            try
            {
                var segmenter = new JiebaSegmenter();
                return segmenter.Cut(text, cutAll: true).ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"分词失败: {ex.Message}");
                return Array.Empty<string>();
            }
        }
    }
}
