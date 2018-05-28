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
	public class Player : LivingElement
	{
		private double width_range = 0;
		private double height_range = 0;

		private int fire_continue_max = 20;
		private int fireContinueCount = 20;
		private int bullets = 0;
		private int maxBullets = 10;
		private int reloadTime = 0;
		private int maxReloadTime = 120;

		private bool isDamaging = false;
		private int damagingResistTime = 70;
		private int damagingResistTimeMax = 70;

		public Player(double width_r, double height_r)
		{
			this.width_range = width_r;
			this.height_range = height_r;
			this.hp = 32;
			this.atk = 5;
			this.def = 3;
			this.speed = 1.5;
			this.pos = new Point(200, 100);
			this.uiImage.Source = new BitmapImage(new Uri("pack://application:,,,/MovingEngine;component/Resources/Player.png"));
			this.uiImage.Width = 30;
			this.uiImage.Height = 30;
			Canvas.SetZIndex(this.uiImage, Layers.VERY_FRONT);


			this.bullets = maxBullets;
		}

		public int Fire_continue_max { get => fire_continue_max; set => fire_continue_max = value; }
		public int FireContinueCount { get => fireContinueCount; set => fireContinueCount = value; }
		public int Bullets { get => bullets; set => bullets = value; }
		public int MaxBullets { get => maxBullets; set => maxBullets = value; }
		public int ReloadTime { get => reloadTime; set => reloadTime = value; }
		public int MaxReloadTime { get => maxReloadTime; set => maxReloadTime = value; }
		public bool IsDamaging { get => isDamaging; set => isDamaging = value; }

		public override void Move()
		{
			if (IsDamaging)
			{
				if (damagingResistTime > 0)
				{
					this.uiImage.Opacity = 0.5;
					damagingResistTime--;
				}
				else
				{
					damagingResistTime = damagingResistTimeMax;
					this.uiImage.Opacity = 1;
					IsDamaging = false;
				}
			}

			Point newp = this.pos + this.vec;
			if (newp.X < 30 || newp.X > width_range - 30 || newp.Y < 30 || newp.Y > height_range - 60)
				return;
			base.Move();
		}
	}
}
