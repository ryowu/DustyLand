using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovingEngine
{
	public class Utility
	{
		public static double UpdateLimitedNumber(double newValue, double maxValue)
		{
			if (newValue < 0) return 0;
			if (newValue > maxValue) return maxValue;
			return newValue;
		}
	}
}
