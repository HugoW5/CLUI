using CLUI;
using CLUI.Components;
using CLUI.Enums;
using CLUI.Layouts;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Security.Principal;


namespace ShowCase
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.CursorVisible = false;
			Window window = new Window(0, 0, 40, 20);

			window.AddComponent(new Button
			{
				X = 0,
				Y = 0,
				Text = "Re-Render Window",
				Id="buton1",
				Click = () =>
				{
					window.Render();
				}
			});

			window.AddComponent(new StackPanel
			{
				X = 0,
				Y = 2,
				Width = window.Width,
				Height = 5,
				Overflow = Overflow.Hide,
				Id = "stack",
			});


			for (int i = 0; i <5; i++)
			{
				var btn = new Button
				{
					Width = window.Width,
					HorizontalAlignment = HorizontalAlignment.Center,
					Text = $"Label {i}"
				};
				((StackPanel)window.GetComponentById("stack")).AddChild(btn);
			}

			window.Render();
			window.HandleInput();
		}
	}
}
