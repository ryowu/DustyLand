using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovingEngine.MovingObjects
{
	public class Layers
	{
		public const int VERY_FRONT = 100;
		public const int FRONT = 75;
		public const int MIDDLE = 50;
		public const int BACK = 25;
		public const int VERY_BACK = 0;
	}

	public enum BulletSize
	{
		Small = 0,
		Middle = 1,
		LeaveSmall = 2,
		Big = 3
	}
}
