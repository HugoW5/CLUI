using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLUI
{
	public interface IComponent
	{
		public int X { get; set; }
		public int Y { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public ConsoleColor BackGroundColor { get; set; }
		public void Render(int offsetX, int offsetY);

	}
}
