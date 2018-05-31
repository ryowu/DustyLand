using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MovingEngine.MovingObjects.Enemies
{
	public class DragonGreen : BaseEnemy
	{
		private int fireDuration = 0;
		private int fireDurationMax = 20;
		public DragonGreen()
		{
			this.speed = 0.8;
			this.atk = 5;
			this.def = 4;
			this.hp = 15;
			this.radiuis = 25;
			this.uiImage.Source = ImageManager.Instance.GetMonsterImage(3);
			this.uiImage.Width = 50;
			this.uiImage.Height = 50;
			Canvas.SetZIndex(this.uiImage, Layers.FRONT);

			this.score = 5;
			this.exp = 4;
			this.coin = 3;
		}
	}
}
