using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovingEngine.MovingObjects
{
    public class EnemyBullet : LinerObject
	{
		protected double damage;
		public double Damage { get => damage; set => damage = value; }
	}
}
