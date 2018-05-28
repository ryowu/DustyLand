using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovingEngine.MovingObjects
{
	public class LivingElement : MovingElement
	{
		protected double hp;
		protected double atk;
		protected double def;
		public double Hp
		{
			get => hp;
			set
			{
				hp = value;
				if (hp < 0)
					this.canBeRemoved = true;
			}
		}
		public double Atk { get => atk; set => atk = value; }
		public double Def { get => def; set => def = value; }
	}
}
