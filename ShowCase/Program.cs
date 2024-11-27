using CLUI;

namespace ShowCase
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Window window = new Window(10, 5, 30, 15, ConsoleColor.White);
			window.AddComponent(new Button
			{
				X = 1,
				Y = 11,
				Width = 6,
				Height = 3,
				Text = "No",
				BackGroundColor = ConsoleColor.Red,
				ForeGroundColor = ConsoleColor.Black,
				BorderColor = ConsoleColor.DarkBlue,
			});
			window.AddComponent(new Button
			{
				X = 10,
				Y = 11,
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
				Y = 11,
				Width = 8,
				Height = 3,
				Text = "Cancel",
				BackGroundColor = ConsoleColor.Blue,
				ForeGroundColor = ConsoleColor.White,
				BorderColor = ConsoleColor.DarkBlue,
			});

			window.Render();
			window.HandleInput();

			Console.Read();
		}
	}
}
