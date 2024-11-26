using System;

namespace Models.ResumeImfo.Apart
{
    public class EduBG
    {
        private string school_name = string.Empty;
        private string schooll_type = string.Empty;
        private string degree = string.Empty;
        private string major = string.Empty;

        public EduBG() { }

        public EduBG(string school_name, string schooll_type, string degree, string major)
        {
            this.school_name = school_name;
            this.schooll_type = schooll_type;
            this.degree = degree;
            this.major = major;
        }

        /// <summary>
        /// 最高学历学校
        /// </summary>
        public string School_name { get => school_name; set => school_name = value; }

        /// <summary>
        /// 985 211/211/空值
        /// </summary>
        public string Schooll_type { get => schooll_type; set => schooll_type = value; }

        /// <summary>
        /// 最高学历 博士/MBA/EMBA/硕士/本科/大专/高中/中专/初中
        /// </summary>
        public string Degree { get => degree; set => degree = value; }

        /// <summary>
        /// 最高学历专业
        /// </summary>
        public string Major { get => major; set => major = value; }

        public override bool Equals(object? obj)
        {
            return obj is EduBG bG &&
                   school_name == bG.school_name &&
                   schooll_type == bG.schooll_type &&
                   degree == bG.degree &&
                   major == bG.major;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(school_name, schooll_type, degree, major);
        }

        public override string ToString()
        {
            return $"School Name: {school_name}, School Type: {schooll_type}, Degree: {degree}, Major: {major}";
        }
    }
}
