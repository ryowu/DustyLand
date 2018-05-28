using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MovingEngine.MovingObjects
{
	public class Explosion : MovingElement
	{
		private int animeIndex = 0;
		private int animeFrameCount = 16;

		public Explosion()
		{
			this.speed = 0;
			this.uiImage.Source = ImageManager.Instance.GetExplosionImage(animeIndex);
			this.uiImage.Width = 50;
			this.uiImage.Height = 50;
			Canvas.SetZIndex(this.uiImage, Layers.VERY_FRONT);
		}
		public override void Move()
		{
			if (animeIndex < animeFrameCount)
			{
				this.uiImage.Source = ImageManager.Instance.GetExplosionImage(animeIndex);
				animeIndex++;
			}
			else
				this.canBeRemoved = true;
		}
	}
}
