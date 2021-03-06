﻿using MovingEngine.MovingObjects;
using MovingEngine.MovingObjects.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MovingEngine
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private object lockObj = new object();

		private Player player1 = new Player(MAP_WIDTH, MAP_HEIGHT);
		private List<IMovingElement> movingBullets = new List<IMovingElement>();
		private List<IMovingElement> movingEnemies = new List<IMovingElement>();
		private List<IMovingElement> movingEnemiesBullets = new List<IMovingElement>();
		private List<IMovingElement> movingExplosions = new List<IMovingElement>();
		private List<IMovingElement> players = new List<IMovingElement>();

		private bool gamePause = false;


		private bool fireOn = false;


		private bool isReloading = false;

		private const double MAP_WIDTH = 800;
		private const double MAP_HEIGHT = 900;


		private DispatcherTimer mainTimer = new DispatcherTimer();

		private int timeLine = 0;

		public MainWindow()
		{
			InitializeComponent();



			mainTimer.Interval = new TimeSpan(0, 0, 0, 0, 2000);
			mainTimer.Tick += MainTimer_Tick;
			mainTimer.Start();
			CompositionTarget.Rendering += CompositionTarget_Rendering;

			this.Width = MAP_WIDTH;
			this.Height = MAP_HEIGHT + gridHeader.Height;

			bulletsCount.BulletsCount = player1.Bullets;

			players.Add(player1);
			mainCanvas.Children.Add(player1.UiImage);

			pnlPlayer.Visibility = Visibility.Hidden;
		}

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			timeLine++;
			Random r = new Random(DateTime.Now.GetHashCode());

			IMovingElement s = null;
			if (timeLine < 5)
			{
				//s = new BaseEnemy();
				s = new Boss01();
				//s = new DragonGreen();
				////////////////////////////////
				mainTimer.Stop();


			}
			else if (timeLine < 10)
			{
				s = new Flower();
			}
			else
			{
				s = new DragonGreen();
			}

			//double left = r.Next(-20, (int)(this.Width + 20.0));
			double left = 400;
			s.Pos = new Point(left, -20);
			Canvas.SetLeft(s.UiImage, left);
			Canvas.SetTop(s.UiImage, -20);
			movingEnemies.Add(s);
			mainCanvas.Children.Add(s.UiImage);
		}

		private void CompositionTarget_Rendering(object sender, EventArgs e)
		{
			if (gamePause) return;
			HandlePlayerPos();
			HandlePlayerBoard();
			CalculateMovingObjects();
			CalculateFire();
			RefreshUIElements();
		}

		#region Methods
		private void HandlePlayerBoard()
		{
			if (Keyboard.IsKeyDown(Key.Tab))
			{
				this.mainTimer.Stop();
				gamePause = true;
				RefreshPlayerBoard();
				pnlPlayer.Visibility = Visibility.Visible;
			}
		}

		private void CalculateFire()
		{
			if (player1.FireContinueCount > 0)
				player1.FireContinueCount--;

			if (isReloading)
			{
				if (player1.ReloadTime < player1.MaxReloadTime)
				{
					player1.ReloadTime++;
				}
				else
				{
					player1.ReloadTime = 0;
					isReloading = false;
					bulletsCount.IsReloading = false;
					player1.Bullets = player1.MaxBullets;
					bulletsCount.BulletsCount = player1.Bullets;
				}
			}
			else
			{
				if (player1.FireContinueCount <= 0 && fireOn)
				{
					player1.Bullets--;
					isReloading = (player1.Bullets <= 0);
					bulletsCount.IsReloading = isReloading;

					bulletsCount.BulletsCount = player1.Bullets;
					Fire();
					player1.FireContinueCount = player1.Fire_continue_max;
				}
			}
		}

		private void RefreshUIElements()
		{
			RefreshMovingObjects(movingBullets);
			RefreshMovingObjects(movingEnemies);
			RefreshMovingObjects(movingEnemiesBullets);
			RefreshMovingObjects(movingExplosions);
			RefreshMovingObjects(players);
			RefreshHeaderPanel();
		}

		private void RefreshHeaderPanel()
		{
			lblHp.Width = (player1.Hp / player1.MaxHp) * borderPlayerHp.Width;
			lblCoin.Content = player1.Coin;
			lblScore.Content = player1.Score;
		}

		private void RefreshMovingObjects(List<IMovingElement> items)
		{
			for (int i = items.Count - 1; i >= 0; i--)
			{
				IMovingElement e = items[i];
				if (e.CanBeRemoved)
				{
					mainCanvas.Children.Remove(e.UiImage);
					lock (lockObj)
					{
						items.RemoveAt(i);
					}
				}
				else
				{
					Canvas.SetLeft(e.UiImage, e.Pos.X - e.UiImage.Width / 2);
					Canvas.SetTop(e.UiImage, e.Pos.Y - e.UiImage.Height / 2);
				}
			}
		}

		private void Fire()
		{
			BulletSmall bullet = new BulletSmall();
			bullet.Pos = new Point(player1.Pos.X + 3, player1.Pos.Y);
			//Point p = Mouse.GetPosition(mainCanvas);
			//Vector v = p - player1.Pos;
			//			bullet.Vec = v / (v.Length / bullet.Speed);
			bullet.Vec = new Vector(0, -8);
			Canvas.SetLeft(bullet.UiImage, bullet.Pos.X);
			Canvas.SetTop(bullet.UiImage, bullet.Pos.Y);

			lock (lockObj)
			{
				movingBullets.Add(bullet);
			}
			mainCanvas.Children.Add(bullet.UiImage);
		}

		private void HandlePlayerPos()
		{
			Vector v = new Vector();

			if (Keyboard.IsKeyDown(Key.Up) || Keyboard.IsKeyDown(Key.W))
				v.Y = -player1.Speed;
			if (Keyboard.IsKeyDown(Key.Down) || Keyboard.IsKeyDown(Key.S))
				v.Y = player1.Speed;
			if (Keyboard.IsKeyDown(Key.Left) || Keyboard.IsKeyDown(Key.A))
				v.X = -player1.Speed;
			if (Keyboard.IsKeyDown(Key.Right) || Keyboard.IsKeyDown(Key.D))
				v.X = player1.Speed;

			player1.Vec = v;
		}

		private void CalculateMovingObjects()
		{
			movingBullets.ForEach(e =>
			{
				if (!e.CanBeRemoved)
				{
					movingEnemies.ForEach(enemy =>
					{
						IBaseEnemy b = enemy as IBaseEnemy;
						if (!b.CanBeRemoved && (b.Pos - e.Pos).Length < b.Radius) //calc bullet width
						{
							b.Hp -= player1.Atk;
							e.CanBeRemoved = true;
							if (b.CanBeRemoved)
							{
								//Add explosion
								Explosion exp = new Explosion();
								exp.Pos = b.Pos;
								movingExplosions.Add(exp);
								mainCanvas.Children.Add(exp.UiImage);

								//Get coin and exp
								player1.Exp += b.Exp;
								player1.Coin += b.Coin;
								player1.Score += b.Score;
							}
						}
					});

					e.Move();
					e.CheckBorder(MAP_WIDTH, MAP_HEIGHT);
				}
			});

			movingEnemies.ForEach(e =>
			{
				IBaseEnemy b = e as IBaseEnemy;
				List<EnemyBullet> bullets_e = b.Action(mainCanvas, player1);
				//Add new enemy bullets into the map
				if (bullets_e != null)
				{
					movingEnemiesBullets.AddRange(bullets_e);
				}
			});

			movingEnemiesBullets.ForEach(e =>
			{
				EnemyBullet b = e as EnemyBullet;
				b.Move();
				if (!player1.IsDamaging && (b.Pos - player1.Pos).Length < player1.Radius)
				{
					player1.Hp -= b.Damage;
					player1.IsDamaging = true;
					b.CanBeRemoved = true;
				}
				b.CheckBorder(MAP_WIDTH, MAP_HEIGHT);
			});

			movingExplosions.ForEach(e => e.Move());

			players.ForEach(e => e.Move());
		}

		private void RefreshPlayerBoard()
		{
			lblPlayerHp.Content = player1.Hp.ToString("F");
			lblPlayerExp.Content = string.Format("{0}/{1}", player1.Exp, player1.CurrentLevelExp);
			lblPlayerLevel.Content = player1.Level;
			lblPlayerAtk.Content = player1.Atk.ToString("F");
			lblPlayerDef.Content = player1.Def.ToString("F");
			lblPlayerSpeed.Content = player1.Speed.ToString("F");
		}
		#endregion

		#region Events
		private void mainCanvas_MouseDown(object sender, MouseButtonEventArgs e)
		{
			fireOn = true;
		}

		private void mainCanvas_MouseUp(object sender, MouseButtonEventArgs e)
		{
			fireOn = false;
		}
		#endregion

		private void btnCloseBoard_Click(object sender, RoutedEventArgs e)
		{
			pnlPlayer.Visibility = Visibility.Hidden;
			gamePause = false;
			mainTimer.Start();
		}
	}
}
