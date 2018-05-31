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

		private int fire_continue_max = 10;
		private int fireContinueCount = 10;
		private int bullets = 0;
		private int maxBullets = 10;
		private int reloadTime = 0;
		private int maxReloadTime = 0;

		private bool isDamaging = false;
		private int damagingResistTime = 70;
		private int damagingResistTimeMax = 70;

		private double hpRecover = 0.001;

		private int coin = 0;
		private int exp = 0;
		private int level = 1;
		private int currentLevelExp = 12;

		private int score = 0;

		public Player(double width_r, double height_r)
		{
			this.width_range = width_r;
			this.height_range = height_r;
			this.hp = 332;
			this.maxHp = 32;
			this.atk = 5;
			this.def = 3;
			this.speed = 1.5;
			this.pos = new Point(500, 400);
			this.uiImage.Source = new BitmapImage(new Uri("pack://application:,,,/MovingEngine;component/Resources/spaceship_sprite.png"));
			this.uiImage.Width = 50;
			this.uiImage.Height = 50;
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
		public int Coin { get => coin; set => coin = value; }
		public int Exp
		{
			get => exp;
			set
			{
				exp = value;
				if (exp > CurrentLevelExp)
				{
					this.level++;
					this.CurrentLevelExp = this.level * 12 + this.level * this.level;
				}
			}
		}
		public int Level { get => level; set => level = value; }
		public int Score { get => score; set => score = value; }
		public int CurrentLevelExp { get => currentLevelExp; set => currentLevelExp = value; }

		public override void Move()
		{
			//HP recover
			this.hp = Utility.UpdateLimitedNumber(this.hp + this.hpRecover, this.maxHp);

			//Damage resist
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


			//Limit move into map area
			Point newp = this.pos + this.vec;
			if (newp.X < 30 || newp.X > width_range - 30 || newp.Y < 30 || newp.Y > height_range - 60)
				return;
			base.Move();
		}
	}
}
