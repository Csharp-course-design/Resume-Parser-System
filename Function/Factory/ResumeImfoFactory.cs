using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ResumeImfo;
using Models.ResumeImfo.Apart;

namespace CsharpAPI.Factory
{
    public class ResumeImfoFactory : IFactory
    {

        static object IFactory.TransJsonToModel(string Json)
        {
            ResumeImfo resumeImfo = null;
            try
            {
                dynamic data = (JsonConvert.DeserializeObject(Json));
                data = data.parsing_result;
                List<WorkExper> workExpers = new List<WorkExper>();
                foreach (dynamic workExper in data.work_experience)
                {
                    workExpers.Add(new WorkExper(
                        workExper.start_time_year,
                        workExper.start_time_month,
                        workExper.end_time_year,
                        workExper.end_time_month,
                        workExper.still_active,
                        workExper.company_name,
                        workExper.department,
                        workExper.location,
                        workExper.job_title
                        ));
                }
                resumeImfo = new ResumeImfo(
                    new BaseImfo(
                        0, 
                        data.basic_info.name, 
                        data.basic_info.age,
                        data.contact_info.phone_number
                        ),
                    new EduBG(
                        data.basic_info.school_name,
                        data.basic_info.school_type,
                        data.basic_info.degree,
                        data.basic_info.major
                        ),
                    data.others.skills,
                    workExpers
                    );
            }
            catch (Exception ex) { }
            return resumeImfo;
        }
    }
}
