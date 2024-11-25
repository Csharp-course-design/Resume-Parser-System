using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    internal class KeyWord
    {
        public int Id { get; set; }

        string word = string.Empty;

        public string Word
        {
            get { return word; }
            set { word = value; }
        }
    }
}
