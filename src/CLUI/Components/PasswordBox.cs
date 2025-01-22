namespace CLUI.Components
{
	/// <summary>
	/// PasswordBox is derived from TextBox.
	/// Acts as a TextBox but maskes the inputed charcaters.
	/// </summary>
	public class PasswordBox : TextBox
	{
	/// <summary>
	/// Overirdes the TextBox Render method and masking the characters with an asterisk.
	/// </summary>
	/// <param name="offsetX"></param>
	/// <param name="offsetY"></param>
		public override void Render(int offsetX, int offsetY)
		{
			if (Height != 1)
			{
				Height = 1;
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
			for (int i = 0; i < Width; i++)
			{
				Console.SetCursorPosition(i + X + offsetX, +Y + offsetY);
				Console.Write(' ');
			}
			Console.SetCursorPosition(X + offsetX, Y + offsetY);
			//display placeholder
			if (Text.Length == 0)
			{
				Console.Write(PlaceHolder);
				Console.SetCursorPosition(X + offsetX, Y + offsetY);
			}
			else
			{
				Console.Write(new string('*', Text.Length));
			}
		}
	}
}
