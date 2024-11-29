using CLUI;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Xml;

namespace ShowCase
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//Console.CursorVisible = false;
			Window window = new(0, 0, Console.BufferWidth/2, Console.BufferHeight/2);
			window.AddComponent(new Button
			{
				X = 0,
				Y = 12,
				Text = "Yes",
			});
			window.AddComponent(new Button
			{
				X =  window.Width-1,
				Y = 12,
				Text = "No"
			});
			window.AddComponent(new Label
			{
				X = 0,
				Y = 0,
				Width = window.Width,
				Text = "Welcome username!",
				HorizontalAlignment = HorizontalAlignment.Center,

			});
			window.AddComponent(new Dropdown
			{
				X = 5,
				Y = 2,
				BackGroundColor = ConsoleColor.DarkBlue,
				ForeGroundColor = ConsoleColor.Black,
				FoucsColors = (ConsoleColor.Blue, ConsoleColor.White),
				Options = {
					"Accounts",
					"Profile",
					"Sign out"
				}

			});
			window.Render();
			window.HandleInput();
		}
	}
}
