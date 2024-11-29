using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace CLUI
{
	public class Rect : IComponent
	{
		public int X { get; set; }
		public int Y { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public int BorderThickness { get; set; } = 0; // only set to 1
		public ConsoleColor BorderColor { get; set; } = ConsoleColor.DarkGray;
		public ConsoleColor BackGroundColor { get; set; } = ConsoleColor.White;

		public void Render(int offsetX, int offsetY)
		{

			Console.BackgroundColor = BackGroundColor;
			for (int i = 0; i < Width; i++)
			{
				for (int j = 0; j < Height; j++)
				{	

					Console.SetCursorPosition(i + (X + offsetX), j + (Y + offsetY));
					Console.WriteLine(' ');
				}
			}
			if (BorderThickness > 0)
			{
				Console.BackgroundColor = BorderColor;
				//Vertical Borders
				for (int i = 0; i < Height; i++)
				{
					Console.SetCursorPosition(X + offsetX, (i + offsetY + Y));
					Console.Write(' ');
					Console.SetCursorPosition(X + (Width + offsetX),(i + offsetY +Y));
					Console.Write(' ');
				}
				//Horizontal Borders
				for (int i = 0; i < Width; i++)
				{
					Console.SetCursorPosition(X + (i + offsetX), offsetY+Y);
					Console.Write(' ');
					Console.SetCursorPosition(X + (i + offsetX), Height + (Y + offsetY));
					Console.Write("  "); // 2 because it work
				}

			}
		}
	}
}
