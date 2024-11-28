using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CLUI
{
    public class Button : IComponent, IFocusable
    {
        public string Text { get; set; } = "";
        public bool IsFocused { get; set; } = false;
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Delegate Click { get; set; } = void () => { Console.Title = "Clicked"; };
        public ConsoleColor BackGroundColor { get; set; } = ConsoleColor.Blue;
        public ConsoleColor ForeGroundColor { get; set; } = ConsoleColor.White;


        private int _offsetX = 0;
        private int _offsetY = 0;

        public void OnFocus()
        {
            IsFocused = true;
            BackGroundColor = ConsoleColor.Green;
            Render(_offsetX, _offsetY);
        }

        public void OnBlur()
        {
            BackGroundColor = ConsoleColor.Red;
            IsFocused = false;
            Render(_offsetX, _offsetY);
        }

        public void Render(int offsetX, int offsetY)
        {
            _offsetX = offsetX;
            _offsetY = offsetY;

            Console.BackgroundColor = BackGroundColor;

            

            //write button label / text
            Console.ForegroundColor = ForeGroundColor;
            Console.SetCursorPosition((X + offsetX) + 1, (Y + offsetY) + 1);
            Console.Write(Text);
            Console.ResetColor();
        }
    }
}
