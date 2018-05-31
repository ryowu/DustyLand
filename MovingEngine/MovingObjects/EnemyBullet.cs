using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MovingEngine.MovingObjects
{
    public class EnemyBullet : LinerObject
    {
		private double damage;
		public EnemyBullet()
		{
			this.speed = 8;
			this.uiImage.Source = new BitmapImage(new Uri("pack://application:,,,/MovingEngine;component/Resources/commonbullet.gif"));
			this.uiImage.Width = 10;
			this.uiImage.Height = 10;
			Canvas.SetZIndex(this.uiImage, Layers.BACK);
		}

		public double Damage { get => damage; set => damage = value; }
	}
}
