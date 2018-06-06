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

		//private Vector v1;
		//private Vector v2;
		//private Vector v3;
		//private Vector v4;
		//private Vector v5;
		//private Vector v6;
		//private Vector v7;
		//private Vector v8;

		private List<Point> movingSteps = new List<Point>();
		private int currentMovingStep = 0;
		private int restTime = 30;


		//Fire type
		private int shotgunFireCount = 0;
		private int shotgunFireCountMaxEveryStep = 3;

		private int windFireCount = 0;
		private int windgunFireCountMaxEveryStep = 30;

		public Boss01()
		{
			this.speed = 4.5;
			this.atk = 5;
			this.def = 4;
			this.hp = 155;
			this.radius = 25;
			this.uiImage.Source = ImageManager.Instance.GetMonsterImage(10);
			this.uiImage.Width = 50;
			this.uiImage.Height = 50;

			Canvas.SetZIndex(this.uiImage, Layers.FRONT);

			this.score = 5;
			this.exp = 4;
			this.coin = 3;

			movingSteps.Add(new Point(400, 150));
			movingSteps.Add(new Point(600, 150));
			movingSteps.Add(new Point(400, 150));
			movingSteps.Add(new Point(400, 250));
			movingSteps.Add(new Point(400, 150));
			movingSteps.Add(new Point(200, 150));

			//v1 = new Vector(0, 2);
			//v2 = new Vector(2, 0);
			//v3 = new Vector(0, -2);
			//v4 = new Vector(-2, 0);

			//v5 = new Vector(0, 2);
			//v6 = new Vector(2, 0);
			//v7 = new Vector(0, -2);
			//v8 = new Vector(-2, 0);
		}

		public override List<EnemyBullet> Action(Canvas mainCanvas, Player player)
		{
			if (restTime > 0)
			{
				restTime--;
				return null;
			}

			List<EnemyBullet> result = new List<EnemyBullet>();

			//Get to the next step
			if ((this.pos - movingSteps[currentMovingStep]).Length < 1)
			{


				fireDuration--;
				if (fireDuration < 0)
				{
					fireDuration = fireDurationMax;

					if (currentMovingStep == 3)
					{
						fireDurationMax = 15;
						Vector bv = new Vector(0, 2);
						for (int i = 0; i < 360; i += 6)
						{
							result.Add(CreateRotateEnemyBullet(mainCanvas, bv, (double)i));
						}
						//result.Add(CreateRotateEnemyBullet(mainCanvas, ref v1, 10));
						//result.Add(CreateRotateEnemyBullet(mainCanvas, ref v2, 10));
						//result.Add(CreateRotateEnemyBullet(mainCanvas, ref v3, 10));
						//result.Add(CreateRotateEnemyBullet(mainCanvas, ref v4, 10));

						//result.Add(CreateRotateEnemyBullet(mainCanvas, ref v5, -10));
						//result.Add(CreateRotateEnemyBullet(mainCanvas, ref v6, -10));
						//result.Add(CreateRotateEnemyBullet(mainCanvas, ref v7, -10));
						//result.Add(CreateRotateEnemyBullet(mainCanvas, ref v8, -10));


						windFireCount++;
						if (windFireCount >= windgunFireCountMaxEveryStep)
						{
							currentMovingStep++;
							windFireCount = 0;
							restTime = 60;
							if (currentMovingStep >= movingSteps.Count)
								currentMovingStep = 0;
						}
					}
					else
					{
						fireDurationMax = 10;

						result.AddRange(CreateShotgunBullets(mainCanvas));
						shotgunFireCount++;
						if (shotgunFireCount >= shotgunFireCountMaxEveryStep)
						{
							//Move to next step
							currentMovingStep++;
							shotgunFireCount = 0;
							restTime = 60;
							if (currentMovingStep >= movingSteps.Count)
								currentMovingStep = 0;
						}
					}
				}
			}
			else //Keep moving
			{
				MoveTo(movingSteps[currentMovingStep]);
			}
			return result;
		}

		private EnemyBulletLeaveSmall CreateRotateEnemyBullet(Canvas mainCanvas, Vector baseVector, double angle)
		{
			EnemyBulletLeaveSmall eb = new EnemyBulletLeaveSmall();
			eb.Pos = this.pos;

			Matrix m = Matrix.Identity;
			m.Rotate(angle);
			eb.Vec = m.Transform(baseVector);

			RotateTransform rotateTransform = new RotateTransform(180 + angle);
			eb.UiImage.RenderTransform = rotateTransform;

			baseVector = eb.Vec;
			Canvas.SetLeft(eb.UiImage, eb.Pos.X);
			Canvas.SetTop(eb.UiImage, eb.Pos.Y);
			mainCanvas.Children.Add(eb.UiImage);
			return eb;
		}

		private List<EnemyBullet> CreateShotgunBullets(Canvas mainCanvas)
		{
			List<EnemyBullet> bullets = new List<EnemyBullet>();
			Vector v = new Vector(0, 7);
			int angle = 3;
			bullets.Add(GetRotateBullet(mainCanvas, v, 0));
			bullets.Add(GetRotateBullet(mainCanvas, v, -2 * angle));
			bullets.Add(GetRotateBullet(mainCanvas, v, 2 * angle));
			bullets.Add(GetRotateBullet(mainCanvas, v, -4 * angle));
			bullets.Add(GetRotateBullet(mainCanvas, v, 4 * angle));
			return bullets;
		}

		private EnemyBulletMiddle GetRotateBullet(Canvas mainCanvas, Vector baseVector, int angle)
		{
			EnemyBulletMiddle eb = new EnemyBulletMiddle();
			eb.Pos = this.pos;
			if (angle != 0)
			{
				Matrix m = Matrix.Identity;
				m.Rotate(angle);
				eb.Vec = m.Transform(baseVector);
			}
			else
			{
				eb.Vec = baseVector;
			}
			eb.Speed = 6;
			Canvas.SetLeft(eb.UiImage, eb.Pos.X);
			Canvas.SetTop(eb.UiImage, eb.Pos.Y);
			mainCanvas.Children.Add(eb.UiImage);
			return eb;
		}
	}
}
