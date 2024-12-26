namespace Models.ResumeInfo.Apart
{
    public class BaseInfo
    {
        private int id;
        private string name = string.Empty;
        private string phone = string.Empty;
        private int age;

        public BaseInfo() { }

        public BaseInfo(int id, string name, int age, string phone)
        {
            this.id = id;
            this.name = name;
            this.age = age;
            this.phone = phone;
        }

        /// <summary>
        /// 信息ID
        /// </summary>
        public int Id { get => id; set => id = value; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get => phone; set => phone = value; }

        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get => age; set => age = value; }


        public string ToStr()
        {
            return $"{id},{name},{age},{phone}";
        }
    }
}
