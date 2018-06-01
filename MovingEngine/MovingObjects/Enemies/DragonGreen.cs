using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MovingEngine.MovingObjects.Enemies
{
	public class DragonGreen : BaseEnemy
	{
		private int fireDuration = 0;
		private int fireDurationMax = 100;

		public DragonGreen()
		{
			this.speed = 0.7;
			this.atk = 5;
			this.def = 4;
			this.hp = 15;
			this.radius = 25;
			this.uiImage.Source = ImageManager.Instance.GetMonsterImage(3);
			this.uiImage.Width = 50;
			this.uiImage.Height = 50;
			Canvas.SetZIndex(this.uiImage, Layers.FRONT);

			this.score = 5;
			this.exp = 4;
			this.coin = 3;
		}

		public override List<EnemyBullet> Action(Canvas mainCanvas, Player player)
		{
			fireDuration--;
			List<EnemyBullet> result = new List<EnemyBullet>();

			if (fireDuration < 0)
			{
				EnemyBulletSmall eb = new EnemyBulletSmall();
				eb.Pos = this.pos;
				Vector v = player.Pos - this.pos;
				eb.Vec = v / (v.Length / eb.Speed);
				Canvas.SetLeft(eb.UiImage, eb.Pos.X);
				Canvas.SetTop(eb.UiImage, eb.Pos.Y);
				mainCanvas.Children.Add(eb.UiImage);

				fireDuration = fireDurationMax;
				result.Add(eb);
			}

			MoveTo(player.Pos);
			return result;
		}
	}
}
