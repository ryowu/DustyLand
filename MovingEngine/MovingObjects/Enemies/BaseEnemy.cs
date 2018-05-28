using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MovingEngine.MovingObjects.Enemies
{
	public class BaseEnemy : LivingElement, IBaseEnemy
	{
		public BaseEnemy()
		{
			this.speed = 0.3;
			this.atk = 1;
			this.def = 2;
			this.hp = 10;
			this.uiImage.Source = new BitmapImage(new Uri("pack://application:,,,/MovingEngine;component/Resources/Slime.png"));
			this.uiImage.Width = 50;
			this.uiImage.Height = 50;
			Canvas.SetZIndex(this.uiImage, Layers.FRONT);
		}

		public void MoveTo(Point target)
		{
			if (!this.canBeRemoved)
			{
				Vector dt = target - this.pos;
				this.vec = dt / (dt.Length / this.speed);
				this.Move();
			}
		}
	}
}
