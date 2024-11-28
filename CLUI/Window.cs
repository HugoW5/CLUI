using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLUI
{
	public class Window
	{
		public int X { get; set; }
		public int Y { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public int BorderThickness { get; set; } = 0;
		public ConsoleColor BorderColor { get; set; }
		public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.White;
		public Window(int x, int y, int width, int height)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}
		public Window(int x, int y, int width, int height, ConsoleColor backgroundColor)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
			BackgroundColor = backgroundColor;
		}
		public Window(int x, int y, int width, int height, ConsoleColor backgroundColor, ConsoleColor borderColor)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
			BackgroundColor = backgroundColor;
			BorderThickness = 1;
			BorderColor = borderColor;
		}

		public List<IComponent> components = new List<IComponent>();
		private int focusedIndex = 0;
		public void AddComponent(IComponent component)
		{
			components.Add(component);
		}
		public void RemoveComponent(IComponent component)
		{
			components.Remove(component);
		}
		public void Render()
		{
			//Render Window
			Console.BackgroundColor = BackgroundColor;
			for (int i = 0; i < Width; i++)
			{
				for (int j = 0; j < Height; j++)
				{
					Console.SetCursorPosition(X + i, Y + j);
					Console.Write(' ');
				}
			}
			if (BorderThickness > 0)
			{
				Console.BackgroundColor = BorderColor;
				//Vertical Borders
				for (int i = 0; i < Height; i++)
				{
					Console.SetCursorPosition(X, (i + Y));
					Console.Write(' ');
					Console.SetCursorPosition(X + (Width), (i + Y));
					Console.Write(' ');
				}
				//Horizontal Borders
				for (int i = 0; i < Width; i++)
				{
					Console.SetCursorPosition(X + (i), Y);
					Console.Write(' ');
					Console.SetCursorPosition(X + (i), Height + (Y));
					Console.Write("  "); // 2 because it work
				}

			}
			Console.ResetColor();


			//Render componets
			foreach (IComponent component in components)
			{
				//Render component with window postion offset
				component.Render(X+4, Y+2);
			}
		}
		public void HandleInput()
		{
			bool runFunction = true;
			while (runFunction)
			{
				if (Console.KeyAvailable)
				{
					ConsoleKey key = Console.ReadKey(intercept: true).Key;
					switch (key)
					{
						case ConsoleKey.Tab:
							MoveFocus();
							break;
						case ConsoleKey.Escape:
							runFunction = false;
							return;
							case ConsoleKey.Enter:
							((Button)components[focusedIndex]).Click.DynamicInvoke();
							break;
					}
				}

			}
		}
		private void MoveFocus()
		{
			// Find the next focusable component
			if (focusedIndex != -1 && components[focusedIndex] is IFocusable currentFocusable)
			{
				currentFocusable.OnBlur();
			}

			do
			{
				focusedIndex = (focusedIndex + 1) % components.Count;
			} while (!(components[focusedIndex] is IFocusable));

			if (components[focusedIndex] is IFocusable newFocusable)
			{
				newFocusable.OnFocus();
			}
		}

	}
}
