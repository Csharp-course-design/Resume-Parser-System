using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    internal class ResumeImfo
    {
        int id;
        string name = string.Empty;
        string phone = string.Empty;
        string sdupg = string.Empty;
        string jobExper = string.Empty;
        List<string> skill = new List<string>();

        public int Id { get => id; set => id = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Sdupg { get => sdupg; set => sdupg = value; }
        public string JobExper { get => jobExper; set => jobExper = value; }
        public List<string> Skill { get => skill; set => skill = value; }
        public string Name { get => name; set => name = value; }
    }
}
