using System;
using System.Drawing;
using MonoTouch.UIKit;

namespace Feelgood
{
	public static class FontHelper
	{
		public static UILabel GetQuoteLabel ()
		{
			var label = new UILabel ();
			label.BackgroundColor = UIColor.Clear;
			label.Font = UIFont.FromName ("Klinic Slab", 30f);
			label.TextColor = UIColor.White;
			return label;
		}
	}
}

