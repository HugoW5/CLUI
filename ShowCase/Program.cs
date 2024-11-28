using CLUI;

namespace ShowCase
{
	internal class Program
	{
		static void Main(string[] args)
		{

			Console.CursorVisible = false;
			Window window = new Window(27, 5, 30, 12, ConsoleColor.White, ConsoleColor.Red);

			int clickCount = 0;

			window.AddComponent(new Label
			{
				Text = "This is a box",
				X = 10,
				Y = 3,
				Width=13,
				BackGroundColor = ConsoleColor.White,
				ForeGroundColor = ConsoleColor.Black,
			});
			window.AddComponent(new Rect
			{
				BackGroundColor = ConsoleColor.DarkGray,
				X = 1,
				Y = 6,
				Width = 29,
				Height = 1
			});
			window.AddComponent(new Button
			{
				X = 2,
				Y = 8,
				Width = 6,
				Height = 3,
				Text = "No",
				Click = () =>
				{
					clickCount++;
					((Label)window.components[0]).Text = clickCount.ToString();
					((Label)window.components[0]).Update();
				},

				BackGroundColor = ConsoleColor.Red,
				ForeGroundColor = ConsoleColor.Black,
				BorderColor = ConsoleColor.DarkBlue,
			});
			window.AddComponent(new Button
			{
				X = 11,
				Y = 8,
				Width = 6,
				Height = 3,
				Text = "Yes",
				BackGroundColor = ConsoleColor.Green,
				ForeGroundColor = ConsoleColor.White,
				BorderColor = ConsoleColor.DarkBlue,
			});
			window.AddComponent(new Button
			{
				X = 20,
				Y = 8,
				Width = 8,
				Height = 3,
				Text = "Cancel",
				BackGroundColor = ConsoleColor.Blue,
				ForeGroundColor = ConsoleColor.White,
				BorderColor = ConsoleColor.DarkBlue,
			});

			window.Render();

			window.HandleInput();
		}
	}
}
