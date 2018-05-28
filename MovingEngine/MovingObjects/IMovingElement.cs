using System.Windows;
using System.Windows.Controls;

namespace MovingEngine.MovingObjects
{
	public interface IMovingElement
	{
		bool CanBeRemoved { get; set; }
		Point Pos { get; set; }
		double Speed { get; set; }
		Image UiImage { get; set; }
		Vector Vec { get; set; }

		void CheckBorder(double width, double height);
		void Move();
	}
}