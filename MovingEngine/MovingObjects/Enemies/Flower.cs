using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MovingEngine.MovingObjects.Enemies
{
    public class Flower : BaseEnemy
    {
		public Flower()
		{
			this.speed = 0.6;
			this.atk = 2;
			this.def = 3;
			this.hp = 12;
			this.uiImage.Source = ImageManager.Instance.GetMonsterImage(0);
			this.uiImage.Width = 50;
			this.uiImage.Height = 50;
			Canvas.SetZIndex(this.uiImage, Layers.FRONT);

			this.score = 2;
			this.exp = 3;
			this.coin = 2;
		}
    }
}
