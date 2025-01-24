using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLUI.Interfaces
{
	public interface ILayout : IComponent
	{
		public List<IComponent> Children { get; }
		public void AddChild(IComponent child);
		public void RemoveChild(IComponent child);
		public bool ContainsInteractiveComponents();
		public void Arrange();
	}

}
