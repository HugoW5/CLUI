﻿using CLUI.Enums;
using CLUI.Interfaces;

namespace CLUI.Components
{
	/// <summary>
	/// Represents a button component.
	/// Supporting focus, click handling and rendering
	/// </summary>
	public class Button : IComponent, IFocusable, IClickable
    {
        public string Text { get; set; } = "";
        public bool IsFocused { get; set; } = false;
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; } = 1;
        public int Height { get; set; } = 1;
		public string Id { get; set; } = string.Empty;
		public HorizontalAlignment HorizontalAlignment { get; set; }
		public virtual Delegate Click { get; set; } = void () => { Console.Write("\a"); }; // virtual for use in Checkbox
        public ConsoleColor BackGroundColor { get; set; } = ConsoleColor.DarkBlue;
        public ConsoleColor ForeGroundColor { get; set; } = ConsoleColor.DarkGray;
        ///<summary>
        /// (Background, Foreground)
        /// Colors when focused
        /// </summary>
        public (ConsoleColor Background, ConsoleColor Foreground) FocusColors { get; set; } = (ConsoleColor.Blue, ConsoleColor.White);

		private int _offsetX = 0;
		private int _offsetY = 0;
		/// <summary>
		/// Handles the focus event by setting the button as focues and triggering a re-render
		/// </summary>
		public void OnFocus()
		{
			IsFocused = true;
			Render(_offsetX, _offsetY);
		}
		/// <summary>
		/// Handles the focus event by setting the button as not focused and triggering a re-render
		/// </summary>
		public void OnBlur()
		{
			IsFocused = false;
			Render(_offsetX, _offsetY);
		}
		public virtual void Render(int offsetX, int offsetY)
		{
			_offsetX = offsetX;
			_offsetY = offsetY;

			if (Width == 1)
			{
				Width = Text.Length;
			}
			if (IsFocused)
			{
				Console.BackgroundColor = FocusColors.Background;
				Console.ForegroundColor = FocusColors.Foreground;
			}
			else
			{
				Console.BackgroundColor = BackGroundColor;
				Console.ForegroundColor = ForeGroundColor;
			}
			if (HorizontalAlignment == HorizontalAlignment.Left)
			{
				if (Width < Text.Length)
				{
					Width = Text.Length;
				}
				Console.SetCursorPosition(X + offsetX, Y + offsetY);
				string outputText = Text;
				outputText += new string(' ', Width - Text.Length);
				Console.Write(outputText);
			}
			if (HorizontalAlignment == HorizontalAlignment.Center)
			{
				int totalWhitespace = Width+1 - Text.Length;
				if (totalWhitespace < 0) totalWhitespace = 0; // no negative offset
				int leftPadding = totalWhitespace / 2;
				int rightPadding = totalWhitespace - leftPadding;

				Console.SetCursorPosition(X + offsetX, Y + offsetY);
				string outputText = new string(' ', leftPadding) + Text + new string(' ', rightPadding);
				Console.Write(outputText);
				Console.ResetColor();
			}
		}
		/// <summary>
		/// Updates the button by re-rendering it at it's last known offset postion
		/// </summary>
		public void Update()
		{
			Render(_offsetX, _offsetY);
		}
	}
}
