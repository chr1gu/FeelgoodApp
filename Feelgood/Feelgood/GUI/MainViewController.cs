using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Feelgood
{
	public class MainViewController : UIViewController
	{
		public MainViewController () : base (null, null)
		{
			View.BackgroundColor = UIColor.FromRGB (50, 50, 50);
		}
	}
}
