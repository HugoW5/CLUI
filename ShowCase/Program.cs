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


			window.AddComponent(new GridPanel(5, 2)
			{
				X = 10,
				Y = 2,
				Width = 20,
				Height = 10,
				BackGroundColor = ConsoleColor.Gray,
				Id = "grid"
			});
			((GridPanel)window.GetComponentById("grid")).AddChild(new Label { Text = "1", }, 0, 0);
			((GridPanel)window.GetComponentById("grid")).AddChild(new Button { Text = "Delete", Width = 10, HorizontalAlignment = HorizontalAlignment.Center }, 0, 1);

			((GridPanel)window.GetComponentById("grid")).AddChild(new Label { Text = "2", }, 1, 0);
			((GridPanel)window.GetComponentById("grid")).AddChild(new Button { Text = "Delete", Width = 10, HorizontalAlignment = HorizontalAlignment.Center }, 1, 1);

			((GridPanel)window.GetComponentById("grid")).AddChild(new Label { Text = "3" }, 2, 0);
			((GridPanel)window.GetComponentById("grid")).AddChild(new Button { Text = "Delete", Width = 10, HorizontalAlignment = HorizontalAlignment.Center }, 2, 1);

			((GridPanel)window.GetComponentById("grid")).AddChild(new Label { Text = "4" }, 3, 0);
			((GridPanel)window.GetComponentById("grid")).AddChild(new Button { Text = "Delete", Width = 10, HorizontalAlignment = HorizontalAlignment.Center }, 3, 1);

			window.Render();
			window.HandleInput();
		}
	}
}
