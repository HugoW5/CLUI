using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLUI
{
    public class ChildButton : Button
    {
        public string Name { get; set; }
        public string Clicked()
        {
            return Name;
        }


    }
}
