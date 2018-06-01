using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MovingEngine.MovingObjects
{
    public class EnemyBulletMiddle : EnemyBullet
	{
		
		public EnemyBulletMiddle()
		{
			this.speed = 4;
			this.damage = 2;
			this.uiImage.Source = ImageManager.Instance.GetBulletImage(new Int32Rect(208, 90, 25, 25));
			this.uiImage.Width = 25;
			this.uiImage.Height = 25;
			Canvas.SetZIndex(this.uiImage, Layers.BACK);
		}


	}
}
