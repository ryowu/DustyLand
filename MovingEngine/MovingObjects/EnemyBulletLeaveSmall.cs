using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MovingEngine.MovingObjects
{
	public class EnemyBulletLeaveSmall : EnemyBullet
	{
		public EnemyBulletLeaveSmall()
		{
			this.speed = 4;
			this.damage = 2;
			this.uiImage.Source = ImageManager.Instance.GetBulletImage(BulletSize.LeaveSmall);
			this.uiImage.Width = 8;
			this.uiImage.Height = 12;
			Canvas.SetZIndex(this.uiImage, Layers.BACK);
		}
	}
}
