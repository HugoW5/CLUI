using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLUI
{
    public class Dropdown : IComponent
    {
        public List<ParentButton> _parentButtons = new List<ParentButton>();
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public ConsoleColor BackGroundColor { get; set; }

        public void AddParentButton(ParentButton parnetButton)
        {
            _parentButtons.Add(parnetButton);
        }

        public void Render(int offsetX, int offsetY)
        {
            foreach (var button in _parentButtons)
            {
                button.Render(offsetX, offsetY);
            }
        }

    }
}
