using DAL.DataControl;
using Function.NLP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// 语义匹配扩充
    /// </summary>
    public class ElasticKey
    {
        /// <summary>
        /// 语义匹配扩充关键词
        /// </summary>
        /// <param name="keys">需扩充的key</param>
        /// <returns>扩充后的key</returns>
        public static List<string> Get(List<string> keys)
        {
            HashSet<string> result = new HashSet<string>();
            List<string> ans = new List<string>();
            foreach (string key in keys) {
                result.Add(key);
                ans =  (new KeyworldControl()).Select();
                foreach (string value in ans) {
                    if (NLPMatch.Match(key, value) > 0)
                    {
                        if (!result.Contains(value))
                        {
                            result.Add(value);
                        }
                    }
                }
            }

            return result.ToList();

        }
    }
}
