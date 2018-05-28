using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MovingEngine.MovingObjects
{
	public class MovingElement : IMovingElement
	{
		protected Image uiImage = new Image();
		protected Point pos;
		protected Vector vec;
		protected double speed = 1;
		protected bool canBeRemoved = false;

		public Image UiImage { get => uiImage; set => uiImage = value; }
		public Point Pos { get => pos; set => pos = value; }
		public Vector Vec { get => vec; set => vec = value; }
		public double Speed { get => speed; set => speed = value; }
		public bool CanBeRemoved { get => canBeRemoved; set => canBeRemoved = value; }

		public virtual void Move()
		{
			if (!this.canBeRemoved)
				this.pos += this.vec;
		}

		public virtual void CheckBorder(double width, double height)
		{
			if (this.pos.X > width || this.pos.X < -15 || this.pos.Y > height || this.pos.Y < -15)
			{
				this.canBeRemoved = true;
			}
		}
	}
}
