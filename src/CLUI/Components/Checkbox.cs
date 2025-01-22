namespace CLUI.Components
{
	/// <summary>
	/// The checkbox component derives from Button but the Click method is not a settable deleagte, use the OnClicked method insted.
	/// Incorporates fields such as Checked (bool), CheckedText And UnCheckedText.
	/// </summary>
	public class Checkbox : Button
	{
		public bool Checked { get; set; } = false;
		/// <summary>
		/// Text to be displayed if the box is checked
		/// </summary>
		public string CheckedText { get; set; } = "[X]";
		/// <summary>
		/// Text to be displayed if the box is unchecked
		/// </summary>
		public string UnCheckedText { get; set; } = "[ ]";
		public override Delegate Click
		{
			get => new Action(Toggle);
			set => throw new NotSupportedException("Cannot set click property on a checkbox");
		}
		public Delegate OnClicked { get; set; } = (bool _checked) => { throw new NotImplementedException(); };
		public override void Render(int offsetX, int offsetY)
		{
			Text = Checked ? CheckedText : UnCheckedText;
			base.Render(offsetX, offsetY);
		}
		/// <summary>
		/// Togles the checkbox on and off.
		/// </summary>
		private void Toggle()
		{
			Checked = !Checked;
			Text = Checked ? CheckedText : UnCheckedText;
			OnClicked.DynamicInvoke(Checked);
			Update();
		}
		/// <summary>
		/// Sets the new bool value and updates the component by re-rendering it
		/// </summary>
		/// <param name="setValue"></param>
		public void SetValue(bool setValue)
		{
			Checked = setValue;
			Text = Checked ? UnCheckedText : CheckedText;
			Update();
		}
	}
}
