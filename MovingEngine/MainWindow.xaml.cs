using MovingEngine.MovingObjects;
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
		private List<IMovingElement> movingExplosions = new List<IMovingElement>();
		private List<IMovingElement> players = new List<IMovingElement>();

		private bool gamePause = false;


		private bool fireOn = false;


		private bool isReloading = false;

		private const double MAP_WIDTH = 1024;
		private const double MAP_HEIGHT = 700;
		

		public MainWindow()
		{
			InitializeComponent();

			CompositionTarget.Rendering += CompositionTarget_Rendering;

			this.Width = MAP_WIDTH;
			this.Height = MAP_HEIGHT;

			bulletsCount.BulletsCount = player1.Bullets;

			players.Add(player1);
			mainCanvas.Children.Add(player1.UiImage);

			pnlPlayer.Visibility = Visibility.Hidden;
		}

		private void CompositionTarget_Rendering(object sender, EventArgs e)
		{
			if (gamePause) return;
			HandlePlayerPos();
			CalculateMovingObjects();
			CalculateFire();
			RefreshUIElements();
		}

		#region Methods
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
			RefreshMovingObjects(movingExplosions);
			RefreshMovingObjects(players);
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
			Point p = Mouse.GetPosition(mainCanvas);
			Bullet bullet = new Bullet();
			bullet.Pos = new Point(player1.Pos.X, player1.Pos.Y);
			Vector v = p - player1.Pos;
			bullet.Vec = v / (v.Length / bullet.Speed);
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
						if (!b.CanBeRemoved && (b.Pos - e.Pos).Length < (e.Speed + 5)) //calc bullet width
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
				b.MoveTo(player1.Pos);
				if ((b.Pos - player1.Pos).Length < b.Speed)
				{
					if (!player1.IsDamaging)
					{
						player1.Hp -= b.Atk;
						player1.IsDamaging = true;
					}
				}

			});

			movingExplosions.ForEach(e => e.Move());

			players.ForEach(e => e.Move());
		}

		private void RefreshPlayerBoard()
		{
			lblPlayerHp.Content = player1.Hp;
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

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Random r = new Random(DateTime.Now.GetHashCode());
			BaseEnemy s = new BaseEnemy();
			double left = r.Next(-20, (int)(this.Width + 20.0));
			s.Pos = new Point(left, -20);
			Canvas.SetLeft(s.UiImage, left);
			Canvas.SetTop(s.UiImage, -20);
			movingEnemies.Add(s);
			mainCanvas.Children.Add(s.UiImage);
		}

		private void mainCanvas_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Tab)
			{
				gamePause = !gamePause;
				if (gamePause)
				{
					RefreshPlayerBoard();
					pnlPlayer.Visibility = Visibility.Visible;
				}
				else
					pnlPlayer.Visibility = Visibility.Hidden;
			}
		}

		#endregion
	}
}
