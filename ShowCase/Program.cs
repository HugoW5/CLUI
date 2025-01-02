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
			Console.CursorVisible = false;
			Window window = new Window(0, 0, 30, 12);
			window.AddComponent(new Label
			{
				X = 0,
				Y = 0,
				Text = "Window Title",
				Width = window.Width,
				HorizontalAlignment = HorizontalAlignment.Center,
			});
			window.AddComponent(new PasswordBox
			{
				X = 5,
				Y = 5,
				PlaceHolder = "Password",
				Width = 14
			});
			window.AddComponent(new Button
			{
				X = 5, 
				Y = 7,
				Text = "Click",
				Click = () =>
				{
					Console.Title = ((PasswordBox)window.components[1]).Text;
				}
			});

			window.Render();
			window.HandleInput();
		}
	}
}
