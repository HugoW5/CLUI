using CLUI;
using CLUI.Components;
using CLUI.Enums;
using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Xml;

namespace ShowCase
{
    internal class Program
	{
		static void Main(string[] args)
		{

			Window window = new Window(0, 0, Console.BufferWidth / 2, Console.BufferHeight / 2);
			window.AddComponent(new Checkbox
			{
				X = 0,
				Y = 0,
				Checked = true,
				OnClicked = (bool _checked) =>
				{
					string text = (_checked ? "Check box is cheked" : "Check box is not cheked");
					var label = ((Label)window.components[1]);
					label.Text = text;
					label.Update();

				}
			});
			window.AddComponent(new Label
			{	
				Width = 25,
				HorizontalAlignment = HorizontalAlignment.Center,
				X = 5,
				Y = 0,
				Text = "Label 1",
			});


			window.Render();
			window.HandleInput();
		}
	}
}
