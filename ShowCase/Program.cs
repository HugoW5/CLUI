using CLUI;
using CLUI.Components;
using CLUI.Enums;
using CLUI.Layouts;
using System.Security.Principal;


namespace ShowCase
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.CursorVisible = false;
			Window window = new Window(0, 0, 40, 20);
			window.AddComponent(new StackPanel
			{
				X = 10,
				Y = 0,
				Width = 30,
				Height = 10,
				BackGroundColor = ConsoleColor.Gray,
				Spacing = 1,
				StackingAlignment = StackingAlignment.Horizontal,
				Id = "stackpanel",
				Children =
				{
					new Button{Text="hej"},
					new Button{Text="hej"},
				}
			});

			((StackPanel)window.GetComponentById("stackpanel")).AddChild([
				new Button { Text = "hej" },
				new Button { Text = "hej" },
				new Button { Text = "hej" },
			]);

			window.Render();
			window.HandleInput();
		}
	}
}
