using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLUI.Interfaces
{
    public interface IFocusable
    {
        void OnFocus();   // component gets focus
        void OnBlur();    // component loses focus
        bool IsFocused { get; set; } // focus state
    }
}
