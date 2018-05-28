using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MovingEngine
{
	/// <summary>
	/// Interaction logic for BulletsCounter.xaml
	/// </summary>
	public partial class BulletsCounter : UserControl
	{
		private int bulletsCount = 0;
		private bool isReloading = false;

		public BulletsCounter()
		{
			InitializeComponent();
		}

		public int BulletsCount
		{
			get => bulletsCount;
			set
			{
				bulletsCount = value;
				RefreshBulletsCount();
			}
		}

		public bool IsReloading { get => isReloading;
			set
			{
				isReloading = value;
				if (isReloading)
					lblBullets.Content = "RELOADING...";
				else
					RefreshBulletsCount();
			}
		}

		private void RefreshBulletsCount()
		{
			if (!isReloading)
			{
				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < bulletsCount; i++)
				{
					sb.Append("I");
				}
				lblBullets.Content = sb.ToString();
			}
		}
	}
}
