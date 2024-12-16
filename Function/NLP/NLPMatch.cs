using System;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace Function.NLP
{// 输入数据类
    public class TextData
    {
        public string Text { get; set; }
    }

    // 输出特征类
    public class FeatureData
    {
        [VectorType]  // 自动匹配实际维度
        public float[] Features { get; set; }
    }

    class NLPMatch
    {
        public static double Match(string a, string b)
        {
            // 创建ML.NET环境
            var mlContext = new MLContext();

            // 示例中文文本
            var data = new[]
            {
                new TextData { Text = a },
                new TextData { Text = b }
            };

            // 加载数据
            var dataView = mlContext.Data.LoadFromEnumerable(data);

            // 使用文本特征提取模型管道
            var textPipeline = mlContext.Transforms.Text.FeaturizeText("Features", "Text");
            var model = textPipeline.Fit(dataView);

            // 转换文本数据
            var transformedData = model.Transform(dataView);

            // 提取特征向量
            var features = mlContext.Data
                .CreateEnumerable<FeatureData>(transformedData, reuseRowObject: false)
                .ToArray();

            // 显示特征向量
            Console.WriteLine("提取的特征向量:");
            foreach (var feature in features)
            {
                Console.WriteLine(string.Join(", ", feature.Features));
            }

            // 计算语义相似度
            return ComputeCosineSimilarity(features[0].Features, features[1].Features);
        }

        // 计算余弦相似度
        public static double ComputeCosineSimilarity(float[] vector1, float[] vector2)
        {
            double dotProduct = vector1.Zip(vector2, (a, b) => a * b).Sum();
            double magnitude1 = Math.Sqrt(vector1.Sum(a => a * a));
            double magnitude2 = Math.Sqrt(vector2.Sum(b => b * b));

            if (magnitude1 == 0 || magnitude2 == 0) return 0;
            return dotProduct / (magnitude1 * magnitude2);
        }
    }
}
