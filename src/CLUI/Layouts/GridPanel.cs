using CLUI.Interfaces;
using System;
using System.Collections.Generic;

namespace CLUI.Layouts
{
    public class GridPanel : ILayout
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Id { get; set; } = string.Empty;
        public ConsoleColor BackGroundColor { get; set; }
        public ConsoleColor ForeGroundColor { get; set; }

        public int Rows { get; }
        public int Columns { get; }

        public int Spacing { get; set; } = 0; // Space between cells

        public List<IComponent> Children { get; private set; } = new List<IComponent>();

        private IComponent?[,] grid; // 2D array to hold components in grid cells

        public GridPanel(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            grid = new IComponent[rows, columns]; // Initialize grid
        }

        public void AddChild(IComponent child)
        {
            // Find first available position (row, column) for the child
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    if (grid[row, col] == null)
                    {
                        grid[row, col] = child;
                        Children.Add(child);
                        Arrange();
                        return;
                    }
                }
            }

            throw new InvalidOperationException("No available space in the grid.");
        }

        public void AddChild(IComponent child, int row, int column)
        {
            if (row >= Rows || column >= Columns)
                throw new ArgumentOutOfRangeException("Row or column is out of grid bounds.");

            if (grid[row, column] != null)
                throw new InvalidOperationException($"Cell ({row}, {column}) is already occupied.");

            grid[row, column] = child;
            Children.Add(child);
            Arrange();
        }

        public void RemoveChild(IComponent child)
        {
            // Find and remove child from grid
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    if (grid[row, col] == child)
                    {
                        grid[row, col] = null;
                        Children.Remove(child);
                        Arrange();
                        return;
                    }
                }
            }

            throw new InvalidOperationException("Child not found in grid.");
        }

        public void Arrange()
        {
            int cellWidth = Width / Columns;
            int cellHeight = Height / Rows;

            foreach (var child in Children)
            {
                // Find the position for the child
                for (int row = 0; row < Rows; row++)
                {
                    for (int col = 0; col < Columns; col++)
                    {
                        if (grid[row, col] == child)
                        {
                            child.X = X + col * (cellWidth + Spacing);
                            child.Y = Y + row * (cellHeight + Spacing);
                            child.Width = Math.Min(cellWidth, child.Width);
                            child.Height = Math.Min(cellHeight, child.Height);
                            break;
                        }
                    }
                }
            }
        }

        public void Render(int offsetX, int offsetY)
        {
            // Render background
            Console.BackgroundColor = BackGroundColor;
            for (int row = 0; row < Height; row++)
            {
                Console.SetCursorPosition(offsetX + X, offsetY + Y + row);
                Console.Write(new string(' ', Width));
            }
        }
    }
}
