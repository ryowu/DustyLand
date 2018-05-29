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

		//All towers
		private BitmapImage explosionImage = new BitmapImage();
		private List<CroppedBitmap> explosionBlocks = new List<CroppedBitmap>();

		//Monsters
		private BitmapImage monsterImage1 = new BitmapImage();
		private List<CroppedBitmap> monsterBlocks1 = new List<CroppedBitmap>();

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
	}
}
