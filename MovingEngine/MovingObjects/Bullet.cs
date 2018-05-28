using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MovingEngine.MovingObjects
{
	public class Bullet : LinerObject
	{
		public Bullet()
		{
			this.speed = 8;
			this.uiImage.Source = new BitmapImage(new Uri("pack://application:,,,/MovingEngine;component/Resources/RedBall.png"));
			this.uiImage.Width = 15;
			this.uiImage.Height = 15;
			Canvas.SetZIndex(this.uiImage, Layers.BACK);
		}
	}
}
