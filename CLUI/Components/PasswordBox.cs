namespace CLUI.Components
{
	public class PasswordBox : TextBox
	{
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
