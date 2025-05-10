using CLUI.Interfaces;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IComponent = CLUI.Interfaces.IComponent;

namespace CLUI
{
	/// <summary>
	/// Represents a window in the console that can contain Components.
	/// Provides method for adding, removing and rendering components, and userinput.
	/// Supports windows postiononing, background color and window borders.
	/// 
	/// </summary>
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

		public List<IComponent> components = new List<Interfaces.IComponent>();
		private int focusedIndex = -1;
		private bool runFunction = true;
		/// <summary>
		/// Add a component to the window
		/// </summary>
		/// <param name="component">The component to add</param>
		public void AddComponent(IComponent component)
		{
			components.Add(component);
		}
		/// <summary>
		/// Removes a component from the window.
		/// </summary>
		/// <param name="component">The component to remove</param>
		public void RemoveComponent(IComponent component)
		{
			components.Remove(component);
		}
		/// <summary>
		/// Renders the window and all of the components inside the window.
		/// </summary>
		public void Render()
		{
			//Render Window
			Console.BackgroundColor = BackgroundColor;
			for (int i = 0; i <= Width; i++)
			{
				for (int j = 0; j <= Height; j++)
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

			/*
						//Render componets

						// Add the children of the Layout component
						List<IComponent> tmpComponents = new List<IComponent>(components);
						List<IComponent> newComponents = new List<IComponent>();

						foreach (var component in components)
						{
							if (component is ILayout tmpLayout)
							{
								tmpComponents.Add(tmpLayout);
								foreach (var child in tmpLayout.Children)
								{
									tmpComponents.Add(child);
								}
							}
						}

						for (int i = 0; i < tmpComponents.Count; i++)
						{
							if (!components.Contains(tmpComponents[i]) && !newComponents.Contains(tmpComponents[i]))
							{
								newComponents.Add(tmpComponents[i]);
							}
						}

						// Add new components to the original list after iteration
						components.AddRange(newComponents);
			*/

			foreach (IComponent component in components)
			{
				//Render component with window postion offset
				component.Render(X, Y);
			}
		}
		/// <summary>
		/// Method for disposing windows
		/// </summary>
		public void Dispose()
		{
			runFunction = false;
		}
		/// <summary>
		/// Handels user-input to the application/window.
		/// </summary>
		public void HandleInput()
		{
			runFunction = true;
			while (runFunction)
			{
				if (Console.KeyAvailable)
				{
					var keyInfo = Console.ReadKey(intercept: true);
					var key = keyInfo.Key;
					var modifiers = keyInfo.Modifiers;

					switch (key)
					{
						case ConsoleKey.Tab:
							if (modifiers.HasFlag(ConsoleModifiers.Shift))
							{
								// Handle Shift+Tab
								MoveFocus(-1);
							}
							else
							{
								// Handle Tab
								MoveFocus(1);
							}
							break;
						case ConsoleKey.DownArrow or ConsoleKey.RightArrow:
							MoveFocus(1);
							break;
						case ConsoleKey.UpArrow or ConsoleKey.LeftArrow:
							MoveFocus(-1);
							break;
						case ConsoleKey.Escape:
							runFunction = false;
							return;
						case ConsoleKey.Enter or ConsoleKey.Spacebar:
							HandleComponentClick(components[focusedIndex]);
							break;
					}
				}
			}
		}
		/// <summary>
		/// Handles component clicks
		/// </summary>
		/// <param name="component">The clicked component</param>
		private void HandleComponentClick(IComponent component)
		{
			if (component is IInputHandler clickableInputHandler)
			{
				clickableInputHandler.HandleInput();
				return;
			}
			//find component functions
			if (component is IClickable currenctClickable)
			{
				currenctClickable.Click.DynamicInvoke();
				return;
			}

		}
		/// <summary>
		/// Moves focus to another compoent in the components list.
		/// Moves the focus either back or forward.
		/// </summary>
		/// <param name="nextMove">Eiter -1 or +1, moves back or forward</param>
		private void MoveFocus(int nextMove)
		{
			// Blur the current focusable component if applicable
			if (focusedIndex >= 0 && components[focusedIndex] is IFocusable currentFocusable)
			{
				currentFocusable.OnBlur();
			}

			do
			{   
				if (nextMove < 0 && focusedIndex == 0)
				{
					focusedIndex = components.Count - 1;
				}
				else if (nextMove > 0 && focusedIndex == components.Count - 1)
				{
					focusedIndex = 0;
				}
				else
				{
					focusedIndex = (focusedIndex + nextMove + components.Count) % components.Count;
				}
			} while (components[focusedIndex] is not IFocusable || ComponentIsUnFocusable()); // = focusable component that should not get focused

			// Focus the new focusable component
			if (components[focusedIndex] is IFocusable newFocusable)
			{
				newFocusable.OnFocus();
			}
			Console.Title = components[focusedIndex].Id;
		}

		/// <summary>
		/// Retrive an component using the componets Id.
		/// </summary>
		/// <param name="id">The specifed id</param>
		/// <returns>Return the compoonent with the speficied if</returns>
		/// <exception cref="InvalidOperationException">No component with the specified id exits</exception>
		public IComponent GetComponentById(string id)
		{
			IComponent? tmpComponent = components.FirstOrDefault(c => c.Id == id);
			if (tmpComponent != null)
			{
				return tmpComponent;
			}
			else
			{
				throw new InvalidOperationException($"Component with ID '{id}' was not found.");
			}
		}
		private bool ComponentIsUnFocusable()
		{
			//Do not focus on the layout if it does not contain any interactive components
			if (components[focusedIndex] is ILayout tmpLayout)
			{
				if (!tmpLayout.ContainsInteractiveComponents())
				{
					return true;
				}
			}
			return false;
		}
	}
}
