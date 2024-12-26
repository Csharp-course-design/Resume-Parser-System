using Models.ResumeInfo;

namespace BLL
{
    public class ArmyClass
    {
        public static Dictionary<string, int> CountSkill(List<ResumeInfo> resumeInfos)
        {
            return GetSkillStatistics(Skill(resumeInfos));
        }

        public static Dictionary<string, int> CountEduBG(List<ResumeInfo> resumeInfos)
        {
            return GetEducationStatistics(EduBG(resumeInfos));
        }

        public static Dictionary<string, int> CountAge(List<ResumeInfo> resumeInfos)
        {
            return GetAgeStatistics(Age(resumeInfos));
        }

        public static List<string> Skill(List<ResumeInfo> resumeInfos)
        {
            List<string> strings = new List<string>();
            foreach (ResumeInfo resumeInfo in resumeInfos)
            {
                strings.AddRange(resumeInfo.Skills);
            }
            return strings;
        }

        public static List<string> EduBG(List<ResumeInfo> resumeInfos)
        {
            List<string> strings = new List<string>();
            foreach (ResumeInfo resumeInfo in resumeInfos)
            {
                strings.Add(resumeInfo.EduBG.School_name);
            }
            return strings;
        }

        public static List<int> Age(List<ResumeInfo> resumeInfos)
        {
            List<int> Ages = new List<int>();
            foreach (ResumeInfo resumeInfo in resumeInfos)
            {
                Ages.Add(resumeInfo.BaseInfo.Age);
            }
            return Ages;
        }
        public static Dictionary<string, int> GetAgeStatistics(List<int> ages)
        {
            // 创建年龄段字典
            var ageGroups = new Dictionary<string, int>
        {
            { "20-29", 0 },
            { "30-39", 0 },
            { "40-49", 0 },
            { "50-59", 0 },
            { "60+", 0 }
        };

            // 按照年龄范围进行分组
            foreach (var age in ages)
            {
                if (age >= 20 && age <= 29)
                    ageGroups["20-29"]++;
                else if (age >= 30 && age <= 39)
                    ageGroups["30-39"]++;
                else if (age >= 40 && age <= 49)
                    ageGroups["40-49"]++;
                else if (age >= 50 && age <= 59)
                    ageGroups["50-59"]++;
                else if (age >= 60)
                    ageGroups["60+"]++;
            }

            return ageGroups;
        }

        public static Dictionary<string, int> GetSkillStatistics(List<string> skills)
        {
            // 创建一个字典来存储技能及其出现次数
            var skillCount = new Dictionary<string, int>();

            // 遍历技能列表，统计每个技能的出现次数
            foreach (var skill in skills)
            {
                if (skillCount.ContainsKey(skill))
                {
                    skillCount[skill]++;
                }
                else
                {
                    skillCount[skill] = 1;
                }
            }

            return skillCount;
        }

        public static Dictionary<string, int> GetEducationStatistics(List<string> educationBackgrounds)
        {
            // 创建一个字典来存储教育背景及其出现次数
            var educationCount = new Dictionary<string, int>();

            // 遍历教育背景列表，统计每种教育背景的出现次数
            foreach (var education in educationBackgrounds)
            {
                if (educationCount.ContainsKey(education))
                {
                    educationCount[education]++;
                }
                else
                {
                    educationCount[education] = 1;
                }
            }

            return educationCount;
        }
    }
}
