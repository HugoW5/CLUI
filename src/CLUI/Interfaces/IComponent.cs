using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLUI.Interfaces
{
    public interface IComponent
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Id { get; set; }
		public ConsoleColor BackGroundColor { get; set; }
        /// <summary>
        /// Renders the component at the specified offset postion.
        /// </summary>
        /// <param name="offsetX">The horizontal offset</param>
        /// <param name="offsetY">The vertical offset</param>
        public void Render(int offsetX, int offsetY);
    }
}
