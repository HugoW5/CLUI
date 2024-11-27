using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace CLUI
{
	public class Button : IComponent, IFocusable
	{
		public string Text { get; set; } = "";
		public bool IsFocused { get; set; } = false;
		public int X { get; set; }
		public int Y { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public int BorderThickness { get; set; } = 0;
		public ConsoleColor BorderColor { get; set; } = ConsoleColor.DarkGray;
		public ConsoleColor BackGroundColor { get; set; }
		public ConsoleColor ForeGroundColor { get; set; }

		private int _offsetX = 0;
		private int _offsetY = 0;

		public void OnFocus()
		{
			IsFocused = true;
			BorderThickness = 1;
			Render(_offsetX, _offsetY);
		}

		public void OnBlur()
		{
			IsFocused = false;
			BorderThickness = 0;
			Render(_offsetX, _offsetY);
		}

		public void Render(int offsetX, int offsetY)
		{
			_offsetX = offsetX;
			_offsetY = offsetY;

			Console.BackgroundColor = BackGroundColor;
			for (int i = 0; i < Width; i++)
			{
				for (int j = 0; j < Height; j++)
				{

					Console.SetCursorPosition(i + (X + offsetX), j + (Y + offsetY));
					Console.WriteLine(' ');
				}
			}
			//write button label / text
			Console.ForegroundColor = ForeGroundColor;
			Console.SetCursorPosition((X + offsetX) + 1, (Y + offsetY) + 1);
            Console.Write(Text);
			Console.ResetColor();
			if (BorderThickness > 0)
			{
				Console.BackgroundColor = BorderColor;
				//Vertical Borders
				for (int i = 0; i < Height; i++)
				{
					Console.SetCursorPosition(X + offsetX, (i + offsetY + Y));
					Console.Write(' ');
					Console.SetCursorPosition(X + (Width-1 + offsetX), (i + offsetY + Y));
					Console.Write(' ');
				}
				//Horizontal Borders
				for (int i = 0; i < Width; i++)
				{
					Console.SetCursorPosition(X + (i + offsetX), offsetY + Y);
					Console.Write(' ');
					Console.SetCursorPosition(X + (i + offsetX), Height-1 + (Y + offsetY));
					Console.Write(' '); // 2 because it work
				}
			}
		}
	}
}
