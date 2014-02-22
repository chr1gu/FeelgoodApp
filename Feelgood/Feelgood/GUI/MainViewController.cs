using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;

namespace Feelgood
{
	public class MainViewController : UIViewController
	{
		public UIScrollView ScrollContainer;

		public MainViewController () : base (null, null)
		{
			View.BackgroundColor = UIColor.FromRGB (50, 50, 50);
			ScrollContainer = new UIScrollView (View.Bounds);
			ScrollContainer.PagingEnabled = true;
			View.Add (ScrollContainer);
			AddQuotes ();
		}

		public void AddQuotes ()
		{
			var quotes = QuoteFactory.GetUsedQuotes ();
			var rect = View.Bounds;
			for (var i = 0; i < quotes.Count; i++) {
				rect.X = View.Bounds.Width * i;
				var view = new QuoteView (rect);
				view.Text = quotes [i].Text;
				ScrollContainer.Add (view);
			}
			var frameSize = new SizeF (View.Bounds.Width * quotes.Count, View.Bounds.Height);
			ScrollContainer.ContentSize = frameSize;
		}

		public override bool PrefersStatusBarHidden ()
		{
			return true;
		}
	}
}
