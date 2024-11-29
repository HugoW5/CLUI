using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CLUI
{
	public class Button : IComponent, IFocusable, IClickable
	{
		public string Text { get; set; } = "";
		public bool IsFocused { get; set; } = false;
		public int X { get; set; }
		public int Y { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public Delegate Click { get; set; } = void () => { Console.Write("\a");};
		public ConsoleColor BackGroundColor { get; set; } = ConsoleColor.DarkBlue;
		public ConsoleColor ForeGroundColor { get; set; } = ConsoleColor.Black;
		///<summary>
		/// (Background, Foreground)
		/// Colors when focused
		/// </summary>
		public (ConsoleColor Background, ConsoleColor Foreground) FoucsColors { get; set; } = (ConsoleColor.Blue, ConsoleColor.White);

		private int _offsetX = 0;
		private int _offsetY = 0;

		public void OnFocus()
		{
			IsFocused = true;
			Render(_offsetX, _offsetY);
		}
		public void OnBlur()
		{
			IsFocused = false;
			Render(_offsetX, _offsetY);
		}
		public void Render(int offsetX, int offsetY)
		{
			_offsetX = offsetX;
			_offsetY = offsetY;

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
			Console.SetCursorPosition(offsetX + X, offsetY + Y);
			Console.Write(Text);
			Console.ResetColor();
		}
	}
}
