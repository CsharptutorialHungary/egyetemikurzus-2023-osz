using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdezetGenerator.Modell
{
    public class Quote
    {
        public string mood { get; set; }
        public string text { get; set; }


        public Quote(string mood, string text)
        {
            this.mood = mood;
            this.text = text;
        }
    }
}
