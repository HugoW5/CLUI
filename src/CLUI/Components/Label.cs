using CLUI.Enums;
using CLUI.Interfaces;

namespace CLUI.Components
{
	/// <summary>
	/// Represent a Label component used for rendering basic text.
	/// Supports attibutes such as HorizontalAlignment.
	/// </summary>
	public class Label : IComponent
	{
		public int X { get; set; }
		public int Y { get; set; }
		public int Width { get; set; } = 0;
		public int Height { get; set; } = 1;
		public string Text { get; set; } = "text";
		public ConsoleColor BackGroundColor { get; set; } = ConsoleColor.Gray;
		public ConsoleColor ForeGroundColor { get; set; } = ConsoleColor.Black;
		/// <summary>
		/// HorizontalAlignment Enum can be
		/// Left or Center. Default is Left
		/// </summary>
		public HorizontalAlignment HorizontalAlignment { get; set; } = 0;
		public string Id { get; set; } = string.Empty;

		private int _offsetX = 0;
		private int _offsetY = 0;
		public void Render(int offsetX, int offsetY)
		{
			_offsetX = offsetX;
			_offsetY = offsetY;

			if (Width == 0)
			{
				Width = Text.Length;
			}

			Console.ForegroundColor = ForeGroundColor;
			Console.BackgroundColor = BackGroundColor;
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
			//Make the text appear in the center using a left offset calculated by the Label Witdh and text length
			if (HorizontalAlignment == HorizontalAlignment.Center)
			{
				int totalWhitespace = Width - Text.Length+1;
				if (totalWhitespace < 0) totalWhitespace = 0; // ensure no negative offset
				int leftPadding = totalWhitespace / 2;
				int rightPadding = totalWhitespace - leftPadding;

				Console.SetCursorPosition(X + offsetX, Y + offsetY);
				string outputText = new string(' ', leftPadding) + Text + new string(' ', rightPadding);
				Console.Write(outputText);
			}
		}
		/// <summary>
		/// Re-renders the Label compoent at it's last known postion.
		/// </summary>
		public void Update()
		{
			Render(_offsetX, _offsetY);
		}
	}
}
