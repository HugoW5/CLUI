﻿using CLUI.Interfaces;
using CLUI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLUI.Components;

namespace CLUI.Layouts
{
	public class StackPanel : ILayout
	{
		public int X { get; set; }
		public int Y { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public string Id { get; set; } = string.Empty;
		public ConsoleColor BackGroundColor { get; set; }
		public ConsoleColor ForeGroundColor { get; set; }
		public List<IComponent> Children { get; private set; } = new List<IComponent>();
		public StackingAlignment StackingAlignment { get; set; } = StackingAlignment.Vertical; // Default to vertical stacking
		public int Spacing { get; set; } = 0; // Space between children

		public void AddChild(IComponent child)
		{
			Children.Add(child);
			Arrange();
		}
		public void AddChild(List<IComponent> children)
		{
			foreach (IComponent child in children)
			{
				Children.Add(child);
			}
			Arrange();
		}

		public void RemoveChild(IComponent child)
		{
			Children.Remove(child);
			Arrange();
		}

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
			Arrange();

			Console.BackgroundColor = BackGroundColor;
			for (int row = 0; row < Height; row++)
			{
				Console.SetCursorPosition(offsetX + X, offsetY + Y + row);
				Console.Write(new string(' ', Width+1));
			}
			Console.ResetColor();
		}
	}

}
