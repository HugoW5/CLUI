using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLUI.Interfaces
{
    public interface IClickable : ITextDisplay
    {
        public Delegate Click { get; set; }
    }
}
