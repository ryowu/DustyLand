﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MovingEngine.MovingObjects
{
    public class EnemyBulletSmall : EnemyBullet
	{
		public EnemyBulletSmall()
		{
			this.speed = 4;
			this.damage = 2;
			this.uiImage.Source = ImageManager.Instance.GetBulletImage(BulletSize.Small);
			this.uiImage.Width = 10;
			this.uiImage.Height = 10;
			Canvas.SetZIndex(this.uiImage, Layers.BACK);
		}
	}
}
