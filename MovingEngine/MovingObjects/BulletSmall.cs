using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MovingEngine.MovingObjects
{
	public class BulletSmall : LinerObject
	{
		public BulletSmall()
		{
			this.speed = 8;
			this.uiImage.Source = new BitmapImage(new Uri("pack://application:,,,/MovingEngine;component/Resources/bullet.png"));
			this.uiImage.Width = 48;
			this.uiImage.Height = 48;
			Canvas.SetZIndex(this.uiImage, Layers.BACK);
		}
	}
}
