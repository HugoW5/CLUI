using CLUI;
using CLUI.Components;
using CLUI.Enums;


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
				Width = 20,
				HorizontalAlignment = HorizontalAlignment.Center,
				Id = "btn",
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
