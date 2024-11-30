using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CLUI
{
	public class Checkbox : Button
	{
		public bool Checked { get; set; } = false;
		public string CheckedText { get; set; } = "[X]";
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
		private void Toggle()
		{
			Checked = !Checked;
			Text = Checked ? CheckedText : UnCheckedText;
			OnClicked.DynamicInvoke(Checked);
			Update();
		}
		public void SetValue(bool setValue)
		{
			Checked = setValue;
			Text = Checked ? UnCheckedText : CheckedText;
			Update();
		}
	}
}
