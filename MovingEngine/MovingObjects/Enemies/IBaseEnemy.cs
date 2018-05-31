using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MovingEngine.MovingObjects.Enemies
{
	public interface IBaseEnemy : IMovingElement
	{
		double Atk { get; set; }
		double Def { get; set; }
		double Hp { get; set; }
		int Score { get; set; }
		int Exp { get; set; }
		int Coin { get; set; }

		double Radius { get; set; }
		List<EnemyBullet> Action(Canvas mainCanvas, Player player);
	}
}