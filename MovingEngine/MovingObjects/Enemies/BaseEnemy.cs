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
		protected int score = 0;
		protected int exp = 0;
		protected int coin = 0;

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

			this.score = 1;
			this.exp = 2;
			this.coin = 1;
		}

		public int Score { get => score; set => score = value; }
		public int Exp { get => exp; set => exp = value; }
		public int Coin { get => coin; set => coin = value; }

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
