using CLUI.Interfaces;

namespace CLUI.Components
{
	public class TextBox : IComponent, IFocusable, IInputHandler
	{
		public bool IsFocused { get; set; } = false;
		public int X { get; set; }
		public int Y { get; set; }
		public int Width { get; set; }
		public int Height { get; set; } = 1;
		public string PlaceHolder { get; set; } = "";
		public string Text { get; set; } = "";
		public string Id { get; set; }
		public ConsoleColor BackGroundColor { get; set; } = ConsoleColor.DarkGray;
		public ConsoleColor ForeGroundColor { get; set; } = ConsoleColor.Gray;
		///<summary>
		/// (Background, Foreground)
		/// Colors when focused
		/// </summary>
		public (ConsoleColor Background, ConsoleColor Foreground) FoucsColors { get; set; } = (ConsoleColor.Gray, ConsoleColor.Black);

		private int _offsetX = 0;
		private int _offsetY = 0;
		public void HandleInput()
		{
			bool runFunction = true;
			while (runFunction)
			{
				Console.CursorVisible = true;
				if (Console.KeyAvailable)
				{
					ConsoleKeyInfo key = Console.ReadKey(intercept: true);
					switch (key.Key)
					{
						case ConsoleKey.Tab:
							// Move focus to the next component
							Console.CursorVisible = false;
							runFunction = false;
							break;
						case ConsoleKey.Escape:
							// Move close this and focus to the next component
							Console.CursorVisible = false;
							runFunction = false;
							break;
						case ConsoleKey.Enter:
							// Move close this and focus to the next component
							Console.CursorVisible = false;
							runFunction = false;
							break;
						default:
							getInput(key);
							break;
					}
				}
			}
		}
		private void getInput(ConsoleKeyInfo key)
		{
			if (key.Key == ConsoleKey.Backspace)
			{
				if (Text.Length > 0)
				{
					Text = Text.Substring(0, Text.Length - 1);
				}
			}
			else
			{
				if (Text.Length <= Width-1)
				{
					Text += key.KeyChar;
				}
			}

			Render(_offsetX, _offsetY);
		}
		public void OnBlur()
		{
			IsFocused = false;
			Render(_offsetX, _offsetY);
		}

		public void OnFocus()
		{
			IsFocused = true;
			Render(_offsetX, _offsetY);
		}

		public virtual void Render(int offsetX, int offsetY)
		{
			_offsetX = offsetX;
			_offsetY = offsetY;
			//height can only be = 1
			if (Height != 1)
			{
				Height = 1;
			}
			if (IsFocused)
			{
				Console.BackgroundColor = FoucsColors.Background;
				Console.ForegroundColor = FoucsColors.Foreground;
			}
			else
			{
				Console.BackgroundColor = BackGroundColor;
				Console.ForegroundColor = ForeGroundColor;
			}
			for (int i = 0; i < Width; i++)
			{
				Console.SetCursorPosition(i + X + offsetX, +Y + offsetY);
				Console.Write(' ');
			}
			Console.SetCursorPosition(X + offsetX, Y + offsetY);
			//display placeholder
			if(Text.Length == 0)
			{
                Console.Write(PlaceHolder);
				Console.SetCursorPosition(X + offsetX, Y + offsetY);
			}
			else
			{
			Console.Write(Text);
			}
		}
	}
}
