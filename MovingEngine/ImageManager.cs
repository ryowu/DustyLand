using MovingEngine.MovingObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MovingEngine
{
	public class ImageManager
	{
		private static ImageManager _instance;

		public static ImageManager Instance
		{
			get
			{
				if (_instance == null) _instance = new ImageManager();
				return ImageManager._instance;
			}
		}

		//Explosion
		private BitmapImage explosionImage = new BitmapImage();
		private List<CroppedBitmap> explosionBlocks = new List<CroppedBitmap>();

		//Monsters
		private BitmapImage monsterImage1 = new BitmapImage();
		private List<CroppedBitmap> monsterBlocks1 = new List<CroppedBitmap>();

		//All bullets
		private BitmapImage allBullets = new BitmapImage();

		private List<CroppedBitmap> enemyBullets = new List<CroppedBitmap>();

		private ImageManager()
		{
			InitializeImageResources();
		}

		public void InitializeImageResources()
		{
			explosionImage.BeginInit();
			explosionImage.UriSource = new Uri("pack://application:,,,/MovingEngine;component/Resources/explosion.png");
			explosionImage.EndInit();
			explosionBlocks = InitializeImageBlocks(explosionImage, 4, 4, 128, 128, 0, 0);

			monsterImage1.BeginInit();
			monsterImage1.UriSource = new Uri("pack://application:,,,/MovingEngine;component/Resources/Monster01.png");
			monsterImage1.EndInit();
			monsterBlocks1 = InitializeImageBlocks(monsterImage1, 12, 8, 32, 32, 0, 0);

			allBullets.BeginInit();
			allBullets.UriSource = new Uri("pack://application:,,,/MovingEngine;component/Resources/allbullets.png");
			allBullets.EndInit();
			enemyBullets.Add(GetBulletImage(new Int32Rect(0, 0, 10, 10)));
			enemyBullets.Add(GetBulletImage(new Int32Rect(208, 90, 25, 25)));
			enemyBullets.Add(GetBulletImage(new Int32Rect(553, 0, 8, 12)));
			enemyBullets.Add(GetBulletImage(new Int32Rect(208, 90, 25, 25)));
			
		}

		/// <summary>
		/// Split image into blocks
		/// </summary>
		/// <param name="img"></param>
		/// <param name="widthBlockCount"></param>
		/// <param name="heightBlockCount"></param>
		/// <returns></returns>
		private List<CroppedBitmap> InitializeImageBlocks(BitmapImage img, int widthBlockCount, int heightBlockCount, int width, int height, int rowAlter, int colAlter)
		{
			List<CroppedBitmap> result = new List<CroppedBitmap>();
			for (int j = 0; j < heightBlockCount; j++)
			{
				for (int i = 0; i < widthBlockCount; i++)
				{
					result.Add(new CroppedBitmap(img, new Int32Rect(i * (width + rowAlter) + rowAlter, j * (height + colAlter) + colAlter, width, height)));
				}
			}
			return result;
		}

		public CroppedBitmap GetExplosionImage(int index)
		{
			return explosionBlocks[index];
		}

		public CroppedBitmap GetMonsterImage(int index)
		{
			return monsterBlocks1[index];
		}

		private CroppedBitmap GetBulletImage(Int32Rect rect)
		{
			return new CroppedBitmap(allBullets, rect);
		}

		public CroppedBitmap GetBulletImage(BulletSize size)
		{
			return enemyBullets[(int)size];
		}
	}
}
