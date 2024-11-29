using CLUI;
using System;
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
			Window window = new Window(0, 0, Console.BufferWidth / 2, Console.BufferHeight / 2);
			window.AddComponent(new Label
			{
				X = 15,
				Y = 3,
				Width = 20,
				HorizontalAlignment = HorizontalAlignment.Center,
				Text = "Label1",
			});
			window.AddComponent(new Dropdown
			{
				X = 0,
				Y = 0,
				Options = {
					"Volvo",
					"Saab",
					"BMW",
					"Opel",
					"Skóda"
				},
				OnSelected = (int index) =>
				{
					var label = ((Label)window.components[0]);
					label.Text = index.ToString();
					label.Render(window.X, window.Y);
				}
			});
			window.Render();
			window.HandleInput();
		}
	}
}
