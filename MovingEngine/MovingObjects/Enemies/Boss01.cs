using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MovingEngine.MovingObjects.Enemies
{
	public class Boss01 : BaseEnemy
	{
		private int fireDuration = 0;
		private int fireDurationMax = 10;

		private Vector v1;
		private Vector v2;
		private Vector v3;
		private Vector v4;
		private Vector v5;
		private Vector v6;
		private Vector v7;
		private Vector v8;

		public Boss01()
		{
			this.speed = 0.3;
			this.atk = 5;
			this.def = 4;
			this.hp = 155;
			this.radiuis = 25;
			this.uiImage.Source = ImageManager.Instance.GetMonsterImage(10);
			this.uiImage.Width = 50;
			this.uiImage.Height = 50;
			
			Canvas.SetZIndex(this.uiImage, Layers.FRONT);

			this.score = 5;
			this.exp = 4;
			this.coin = 3;

			v1 = new Vector(0, 2);
			v2 = new Vector(2, 0);
			v3 = new Vector(0, -2);
			v4 = new Vector(-2, 0);

			v5 = new Vector(0, 2);
			v6 = new Vector(2, 0);
			v7 = new Vector(0, -2);
			v8 = new Vector(-2, 0);
		}

		public override List<EnemyBullet> Action(Canvas mainCanvas, Player player)
		{
			fireDuration--;
			List<EnemyBullet> result = new List<EnemyBullet>();

			if (fireDuration < 0)
			{
				fireDuration = fireDurationMax;
				result.Add(CreateEnemyBullet(mainCanvas, ref v1, 10));
				result.Add(CreateEnemyBullet(mainCanvas, ref v2, 10));
				result.Add(CreateEnemyBullet(mainCanvas, ref v3, 10));
				result.Add(CreateEnemyBullet(mainCanvas, ref v4, 10));

				result.Add(CreateEnemyBullet(mainCanvas, ref v5, -10));
				result.Add(CreateEnemyBullet(mainCanvas, ref v6, -10));
				result.Add(CreateEnemyBullet(mainCanvas, ref v7, -10));
				result.Add(CreateEnemyBullet(mainCanvas, ref v8, -10));
			}

			//MoveTo(new Point(600, 30));
			return result;
		}

		private EnemyBullet CreateEnemyBullet(Canvas mainCanvas, ref Vector baseVector, double angle)
		{
			EnemyBullet eb = new EnemyBullet();
			eb.Pos = this.pos;
			Matrix m = Matrix.Identity;
			m.Rotate(angle);
			eb.Vec = m.Transform(baseVector);
			baseVector = eb.Vec;
			Canvas.SetLeft(eb.UiImage, eb.Pos.X);
			Canvas.SetTop(eb.UiImage, eb.Pos.Y);
			mainCanvas.Children.Add(eb.UiImage);
			return eb;
		}
	}
}
