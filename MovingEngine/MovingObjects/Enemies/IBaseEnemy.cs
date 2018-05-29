using System.Windows;

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
		void MoveTo(Point target);
	}
}