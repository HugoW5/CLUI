using CLUI.Interfaces;
using IComponent = CLUI.Interfaces.IComponent;

namespace CLUI.Components
{
	public class Dropdown : IComponent, IFocusable, IInputHandler
	{
		public int X { get; set; }
		public int Y { get; set; }
		public int Width { get; set; } = 0;
		public int Height { get; set; } = 1;
		public string Id { get; set; } = string.Empty;
		public ConsoleColor BackGroundColor { get; set; } = ConsoleColor.DarkBlue;
		public ConsoleColor ForeGroundColor { get; set; } = ConsoleColor.DarkGray;
		///<summary>
		/// (Background, Foreground)
		/// Colors when focused
		/// </summary>
		public (ConsoleColor Background, ConsoleColor Foreground) FocusColors { get; set; } = (ConsoleColor.Blue, ConsoleColor.White);
		public List<string> Options { get; set; } = new List<string>();
		public int SelectedIndex { get; set; } = -1; // -1 = no selection
		/// <summary>
		/// Runs when the user has selected an item
		/// Parameter index is the selected items postions in Options list
		/// </summary>
		public Delegate OnSelected { get; set; } = (int index) => { throw new NotImplementedException(); };
		public bool IsOpen { get; private set; } = false;
		public bool IsFocused { get; set; } = false;
		public string Text { get; set; } = "Select...";

		private string? SelectedValue = null;
		private int _offsetX = 0;
		private int _offsetY = 0;
		public void Open()
		{
			if (!IsOpen)
			{
				IsOpen = true;
				Height = Options.Count + 1;
			}
			//Re render with open or closed togglew
			Render(_offsetX, _offsetY);
		}
		public void Close()
		{
			if (IsOpen)
			{
				IsOpen = false;
				Height = 1;
				//OnDropdownClosed?.Invoke();
				ClearOptions(_offsetX, _offsetY);
				Render(_offsetX, _offsetY);
			}
		}

		//Render the dropdown
		public void Render(int offsetX, int offsetY)
		{
			//Calculate actual position
			_offsetX = offsetX;
			_offsetY = offsetY;
			if (Width == 0)
			{
				//take the largets and make it the width
				Width = Text.Length;
				if (Options.OrderByDescending(s => s.Length).First().Length > Width)
				{
					Width = Options.OrderByDescending(s => s.Length).First().Length;
				}
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

			//Render the collapsed state (selected value)
			//Make the selected item wider if it needs to
			string selected = $"{SelectedValue ?? Text}";
			string formatedSelected = "[" + selected + new string(' ', Width - selected.Length) + "]";
			Console.SetCursorPosition(offsetX + X, offsetY + Y);
			Console.Write(formatedSelected);

			// Render options if the dropdown is open
			if (IsOpen)
			{
				for (int i = 0; i < Options.Count; i++)
				{
					Console.SetCursorPosition(offsetX + X, offsetY + Y + i + 1);
					if (i == SelectedIndex)
					{
						Console.BackgroundColor = ConsoleColor.Gray; //Highlight selected option
						Console.ForegroundColor = ConsoleColor.Black;
					}
					else
					{
						Console.BackgroundColor = BackGroundColor;
						Console.ForegroundColor = ConsoleColor.White;
					}
					Console.Write(Options[i].PadRight(Width));
				}
			}

			Console.ResetColor();
		}
		//Handle input, navigation + selection
		public void HandleInput()
		{
			Open();
			bool runFunction = true;
			while (runFunction)
			{
				if (Console.KeyAvailable)
				{
					ConsoleKey key = Console.ReadKey(intercept: true).Key;
					switch (key)
					{
						case ConsoleKey.Tab:
							//Move focus to the next component
							Close();
							runFunction = false;
							break;
						case ConsoleKey.Escape:
							//Move close this and focus to the next component
							Close();
							runFunction = false;
							break;
						case ConsoleKey.Enter:
							if (SelectedIndex >= 0 && SelectedIndex < Options.Count)
							{
								OnOptionSelected(Options[SelectedIndex]);
							}
							Close();
							runFunction = false;
							break;
						case ConsoleKey.UpArrow:
							if (SelectedIndex > 0)
							{
								SelectedIndex--;
								Render(_offsetX, _offsetY);
							}
							break;
						case ConsoleKey.DownArrow:
							if (SelectedIndex < Options.Count - 1)
							{
								SelectedIndex++;
								Render(_offsetX, _offsetY);
							}
							break;
					}
				}
			}
		}
		private void OnOptionSelected(string opt)
		{
			SelectedValue = opt;
			//call the delegate on selected with the index parameter
			OnSelected.DynamicInvoke(SelectedIndex);
		}
		// Focus handling methods
		public void OnFocus()
		{
			IsFocused = true;
			Render(_offsetX, _offsetY);
		}
		public void OnBlur()
		{
			IsFocused = false;
			//Close();
			Render(_offsetX, _offsetY);
		}
		private void ClearOptions(int offsetX, int offsetY)
		{
			Console.BackgroundColor = ConsoleColor.White;
			for (int i = 0; i < Options.Count; i++)
			{
				Console.SetCursorPosition(offsetX + X, offsetY + Y + i + 1);
				Console.Write(new string(' ', Width)); // Overwrite the area with white spaces
			}
			Console.ResetColor();
		}
	}
}
