using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartRenderer
{
    public interface IDisplay
    {
        public void DisplayAge(Chart DisplayBlog);
        public void DisplayEduBg(Chart DisplayBlog);
        public void DisplaySkill(Chart DisplayBlog);

    }
}
