using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ResumeImfo.Apart
{
    public class WorkExper
    {
        string start_time_year = string.Empty;
        string start_time_month = string.Empty;
        string end_time_year = string.Empty;
        string end_time_month = string.Empty;
        bool still_active;
        string company_name = string.Empty;
        string department = string.Empty;
        string location = string.Empty;
        string job_title = string.Empty;

        public WorkExper()
        {
        }

        public WorkExper(string start_time_year, string start_time_month, string end_time_year, string end_time_month, bool still_active, string company_name, string department, string location, string job_title)
        {
            this.start_time_year = start_time_year;
            this.start_time_month = start_time_month;
            this.end_time_year = end_time_year;
            this.end_time_month = end_time_month;
            this.still_active = still_active;
            this.company_name = company_name;
            this.department = department;
            this.location = location;
            this.job_title = job_title;
        }

        /// <summary>
        /// 开始时间年份
        /// </summary>
        public string Start_time_year { get => start_time_year; set => start_time_year = value; }

        /// <summary>
        /// 开始时间月份
        /// </summary>
        public string Start_time_month { get => start_time_month; set => start_time_month = value; }

        /// <summary>
        /// 	结束时间年份
        /// </summary>
        public string End_time_year { get => end_time_year; set => end_time_year = value; }

        /// <summary>
        /// 结束时间月份
        /// </summary>
        public string End_time_month { get => end_time_month; set => end_time_month = value; }

        /// <summary>
        /// 是否仍在
        /// </summary>
        public bool Still_active { get => still_active; set => still_active = value; }

        /// <summary>
        /// 公司/学校/社团名
        /// </summary>
        public string Company_name { get => company_name; set => company_name = value; }

        /// <summary>
        /// 所属部门
        /// </summary>
        public string Department { get => department; set => department = value; }

        /// <summary>
        /// 地点
        /// </summary>
        public string Location { get => location; set => location = value; }

        /// <summary>
        /// 职位名
        /// </summary>
        public string Job_title { get => job_title; set => job_title = value; }

        public override bool Equals(object? obj)
        {
            if (obj is WorkExper other)
            {
                return start_time_year == other.start_time_year &&
                       start_time_month == other.start_time_month &&
                       end_time_year == other.end_time_year &&
                       end_time_month == other.end_time_month &&
                       still_active == other.still_active &&
                       company_name == other.company_name &&
                       department == other.department &&
                       location == other.location &&
                       job_title == other.job_title;
            }
            return false;
        }


        public override string ToString()
        {
            string activeStatus = still_active ? "Still Active" : $"{end_time_year}-{end_time_month}";
            return $"Company: {company_name}, Department: {department}, Location: {location}, Job Title: {job_title}, Start: {start_time_year}-{start_time_month}, End: {activeStatus}";
        }

    }
}
