using CLUI.Interfaces;
using CLUI.Enums;
using CLUI.Components;

namespace CLUI.Layouts
{
	/// <summary>
	/// Represent a stack-based layout panel.
	/// The StackPanel arranges child components either horizontally or vertically.
	/// Supports Adding and Remoing children +
	/// (WIP) Overflow Handling
	/// </summary>
	public class StackPanel : ILayout, IFocusable, IInputHandler
	{
		public int X { get; set; }
		public int Y { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public string Id { get; set; } = string.Empty;
		public ConsoleColor BackGroundColor { get; set; } = ConsoleColor.White;
		public ConsoleColor ForeGroundColor { get; set; }
		public List<IComponent> Children { get; private set; } = new List<IComponent>();
		/// <summary>
		/// The StackingAlignment Enum sets how the Arrange() method arrages the child components.
		/// It Supports Horizontal and Vertical Stacking.
		/// </summary>
		public StackingAlignment StackingAlignment { get; set; } = StackingAlignment.Vertical; // Default to vertical stacking
		/// <summary>
		/// Overflow attribute handles what happens incase of component overflow inside the stackpanel.
		/// Show, Hide & Scroll
		/// </summary>
		public Overflow Overflow { get; set; } = Overflow.Show;
		public int Spacing { get; set; } = 0; // Space between children
		private int _offsetX;
		private int _offsetY;
		public bool IsFocused { get; set; }
		private int focusedIndex = 0;
		private List<IComponent> VisibleComponents = new List<IComponent>();

		public void OnFocus()
		{
			IsFocused = true;
			MoveFocus(0); // place the foucs on the first focusable component
			HandleInput();
		}
		/// <summary>
		/// Handles the focus event by setting the component as not focused and triggering a re-render
		/// </summary>
		public void OnBlur()
		{
			IsFocused = false;
		}
		public void HandleInput()
		{
			bool runFunction = true;
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
						case ConsoleKey.Escape:
							if (Children[focusedIndex] is IFocusable child)
							{
								child.OnBlur();
							}
							runFunction = false;
							return;
						case ConsoleKey.Enter or ConsoleKey.Spacebar:
							HandleComponentClick(Children[focusedIndex]);
							break;
					}
				}
			}
		}
		public bool ContainsInteractiveComponents()
		{
			for (int i = 0; i < Children.Count; i++)
			{
				if (Children[i] is IFocusable)
				{
					return true;
				}
			}
			return false;
		}

		private void MoveFocus(int nextMove)
		{
			// Blur the current focusable component if applicable
			if (focusedIndex >= 0 && Children[focusedIndex] is IFocusable currentFocusable)
			{
				currentFocusable.OnBlur();
			}
			do
			{
				if (nextMove < 0 && focusedIndex == 0)
				{
					focusedIndex = Children.Count - 1;
				}
				else if (nextMove > 0 && focusedIndex == Children.Count - 1)
				{
					focusedIndex = 0;
				}
				else
				{
					focusedIndex = (focusedIndex + nextMove + Children.Count) % Children.Count;
				}
			} while (Children[focusedIndex] is not IFocusable);

			// Focus the new focusable component
			if (Children[focusedIndex] is IFocusable newFocusable)
			{
				newFocusable.OnFocus();
			}
			Console.Title = Children[focusedIndex].GetHashCode().ToString();
		}

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
		/// Add a child to the StackPanel.
		/// And arrange the StackPanel.
		/// </summary>
		/// <param name="child">The child to add to the StackPanel</param>
		public void AddChild(IComponent child)
		{
			Children.Add(child);
			Arrange();
		}
		/// <summary>
		/// Add a list of child components.
		/// </summary>
		/// <param name="children">The list of components</param>
		public void AddChild(List<IComponent> children)
		{
			foreach (IComponent child in children)
			{
				Children.Add(child);
			}
			Arrange();
		}
		/// <summary>
		/// Removes a child from the StackPanel
		/// </summary>
		/// <param name="child">The child to remove</param>
		public void RemoveChild(IComponent child)
		{
			Children.Remove(child);
			Arrange();
		}
		/// <summary>
		/// Arranges the child-components inside the StackPanel.
		/// </summary>s
		public void Arrange()
		{
			int offsetX = X;
			int offsetY = Y;

			//Make sure that every component with text has the minimun width of the text.
			foreach (var child in Children)
			{
				if (child is ITextDisplay && child is IComponent tmpComponent)
				{
					if (tmpComponent is ITextDisplay textDisplay)
					{
						if (tmpComponent.Width < textDisplay.Text.Length)
						{
							tmpComponent.Width = textDisplay.Text.Length;
						}
					}

				}

				child.X = offsetX;
				child.Y = offsetY;

				if (StackingAlignment == 0)
				{
					offsetY += child.Height + Spacing;
				}
				else
				{
					offsetX += child.Width + Spacing;
				}
			}
		}

		public void Render(int offsetX, int offsetY)
		{
			//Arrange the child components
			_offsetX = offsetX;
			_offsetY = offsetY;
			Arrange();

			Console.BackgroundColor = BackGroundColor;
			for (int row = 0; row < Height; row++)
			{
				Console.SetCursorPosition(offsetX + X, offsetY + Y + row);
				Console.Write(new string(' ', Width + 1));
			}
			if (Overflow == Overflow.Show)
			{
				foreach (var child in Children)
				{
					child.Render(offsetX, offsetY);
				}
			}
			else if (Overflow == Overflow.Hide)
			{
				//Calculate amount of visible components
				int space = 0;
				int components = 0;
				for (int i = 0; i < Children.Count; i++)
				{
					if (space + Children[i].Height > this.Height)
					{
						break;
					}
					space += Children[i].Height;
					components++;
				}
				for (int i = 0; i < components; i++)
				{
					// Render everything that fits
					Children[i].Render(offsetX, offsetY);
				}
			}
			else if (Overflow == Overflow.Scroll)
			{
				RenderScrollableComponents();
			}
			Console.ResetColor();
		}
		/// <summary>
		/// Method for rendering components when the overlflow
		/// handling is set to scroll
		/// </summary>
		private void RenderScrollableComponents()
		{
			//Calculate amount of visible components
			int space = 0;
			for (int i = 0; i < Children.Count; i++)
			{
				if (space + Children[i].Height > Height)
				{
					break;
				}
				space += Children[i].Height;
				Children[i].Render(_offsetX, _offsetY);
				VisibleComponents.Add(Children[i]);
			}
		}
	}

}
