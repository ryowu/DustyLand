﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MovingEngine.MovingObjects
{
	public class LivingElement : MovingElement
	{
		protected double hp;
		protected double atk;
		protected double def;
		protected double radius;
		protected double maxHp;

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
		public double MaxHp { get => maxHp; set => maxHp = value; }
		public double Radius { get => radius; set => radius = value; }
	}
}
