using System;

namespace Models.ResumeImfo.Apart
{
    public class BaseImfo
    {
        private int id;
        private string name = string.Empty;
        private string phone = string.Empty;

        public BaseImfo() { }

        public BaseImfo(int id, string name, string phone)
        {
            this.id = id;
            this.name = name;
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

        public override bool Equals(object? obj)
        {
            if (obj is BaseImfo other)
            {
                return id == other.id && name == other.name && phone == other.phone;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id, name, phone);
        }

        public override string ToString()
        {
            return $"ID: {id}, Name: {name}, Phone: {phone}";
        }
    }
}
