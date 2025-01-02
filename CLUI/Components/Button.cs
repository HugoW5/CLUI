using CLUI.Interfaces;

namespace CLUI.Components
{
	public class Button : IComponent, IFocusable, IClickable
    {
        public string Text { get; set; } = "";
        public bool IsFocused { get; set; } = false;
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; } = 1;
        public int Height { get; set; } = 1;
        public virtual Delegate Click { get; set; } = void () => { Console.Write("\a"); }; // virtual for use in Checkbox
        public ConsoleColor BackGroundColor { get; set; } = ConsoleColor.DarkBlue;
        public ConsoleColor ForeGroundColor { get; set; } = ConsoleColor.DarkGray;
        ///<summary>
        /// (Background, Foreground)
        /// Colors when focused
        /// </summary>
        public (ConsoleColor Background, ConsoleColor Foreground) FoucsColors { get; set; } = (ConsoleColor.Blue, ConsoleColor.White);

        private int _offsetX = 0;
        private int _offsetY = 0;

        public void OnFocus()
        {
            IsFocused = true;
            Render(_offsetX, _offsetY);
        }
        public void OnBlur()
        {
            IsFocused = false;
            Render(_offsetX, _offsetY);
        }
        public virtual void Render(int offsetX, int offsetY)
        {
            _offsetX = offsetX;
            _offsetY = offsetY;

            if (IsFocused)
            {
                Console.BackgroundColor = FoucsColors.Background;
                Console.ForegroundColor = FoucsColors.Foreground;
            }
            else
            {
                Console.BackgroundColor = BackGroundColor;
                Console.ForegroundColor = ForeGroundColor;
            }
            Console.SetCursorPosition(offsetX + X, offsetY + Y);
            Console.Write(Text);
            Console.ResetColor();
        }
        /// <summary>
        /// RE-render
        /// </summary>
        public void Update()
        {
            Render(_offsetX, _offsetY);
        }
    }
}
