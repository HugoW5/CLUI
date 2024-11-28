using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CLUI
{
    public class ParentButton : Button
    {
        public string Name { get; set; }
        public bool IsOpen { get; set; } = false;
        public List<ChildButton> ChildButtons = new List<ChildButton>();
        public Delegate Click = new Action (() =>
        {
            Console.Title = "hej";
        });

        public void Clicked()
        {
            if (IsOpen)
            {
                //ta bort komponenter
//                Window.Component.RemoveRange(fokusIndex + 1, ChildButtons.Count());
                IsOpen = false;
            }
            else
            {
                //lägg till komponenter
//                Window.Component.AddRange(ChildButtons);
                IsOpen = true;
            }
        }

        public void AddChildButton(ChildButton childButton)
        {
            ChildButtons.Add(childButton);
        }

        public void Render(int offsetX, int offsetY)
        {
            /*Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Blue;*/

            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(offsetX, offsetY + Y);
            Console.Write($" {Name} ");


            if (IsOpen)
            {
                foreach (ChildButton button in ChildButtons)
                {
                    button.Render(offsetX + 2, Console.GetCursorPosition().Top);
                }
            }
        }
    }
}
