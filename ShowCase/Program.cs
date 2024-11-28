using CLUI;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace ShowCase
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//Console.CursorVisible = false;


			Window window = new Window(10, 2, 30, 20, ConsoleColor.Blue);
			Dropdown dropdown = new Dropdown();
			dropdown.AddParentButton(new ParentButton
			{
				Y = 0,
				Name = "Accounts",
				ChildButtons =
				{
					new ChildButton
					{
						Name = "Privatkonto",
					},
					new ChildButton
					{
						Name = "Sparkonto"
					}
				}
			});
            dropdown.AddParentButton(new ParentButton
            {
				Y = 1,
                Name = "Sign out",
                ChildButtons =
                {
                    new ChildButton
                    {
                        Name = "Log out",
                    },
                    new ChildButton
                    {
                        Name = "Exit"
                    }
                }
            });

			/*			foreach (var item in dropdown._parentButtons) 
						{
							window.AddComponent(item);
						}*/

			window.AddComponent(dropdown);
			
            window.Render();
			window.HandleInput();
		}
	}
}
