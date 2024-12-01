using CLUI;
using CLUI.Components;
using CLUI.Enums;
using CLUI.Interfaces;
using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Xml;

namespace ShowCase
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Window window = new Window(0, 0, 30, 12);
			window.AddComponent(new Label
			{
				X = 0,
				Y = 0,
				Text = "Window Title",
				Width = window.Width,
				HorizontalAlignment = HorizontalAlignment.Center,
			});

			window.Render();
			window.HandleInput();
		}
	}
}
